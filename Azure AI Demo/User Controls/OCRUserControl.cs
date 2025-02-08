using Azure.AI.Vision.ImageAnalysis;
using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using Azure_AI_Demo.Utils;

namespace Azure_AI_Demo
{
    public partial class OCRUserControl : UserControl
    {
        private ImageAnalysisClient? _imageClient;
        private List<DetectedTextLine>? _detectedTextLines;

        private static readonly float _confidenceThreshold = 0.5f;

        public OCRUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            var computerVisionEndpoint = Program.Configuration["AIComputerVisionServiceEndpoint"];
            var computerVisionKey = Program.Configuration["AIComputerVisionServiceKey"];
            if (!string.IsNullOrEmpty(computerVisionEndpoint) && !string.IsNullOrEmpty(computerVisionKey))
            {
                var computerVisionUri = new Uri(computerVisionEndpoint);
                var computerVisionCredentials = new AzureKeyCredential(computerVisionKey);
                _imageClient = new ImageAnalysisClient(computerVisionUri, computerVisionCredentials);
            }
            else
            {
                buttonReadImage.Enabled = false;
            }
        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ClearPreviousAnalysisResults();
                    var filePath = openFileDialog.FileName;
                    pictureBoxImageToAnalyze.Image = Image.FromFile(filePath);
                }
            }
        }

        private async void buttonReadImage_Click(object sender, EventArgs e)
        {
            if (_imageClient == null)
            {
                textBoxReadResult.Text = "Error: Image client not initialized";
                return;
            }
            if (pictureBoxImageToAnalyze.Image == null)
            {
                textBoxReadResult.Text = "Error: Image to analyze is empty";
                return;
            }

            // Clear previous results
            ClearPreviousAnalysisResults();

            // Show activity indicator
            var cancellationTokenSource = new CancellationTokenSource();
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateTextSafely(textBoxReadResult, text), "Analyzing image", cancellationTokenSource.Token));

            // Analyze image
            var imageData = GetBinaryDataFromImage(pictureBoxImageToAnalyze.Image);
            var imageAnalysisResult = await _imageClient.AnalyzeAsync(imageData, VisualFeatures.Read);

            // Stop Activity Indicator
            cancellationTokenSource.Cancel();
            await activityIndicatorTask;

            // Display image analysis result
            textBoxReadResult.Text = GetAnalysisResultString(imageAnalysisResult.Value);
            SetBoundingBoxes(imageAnalysisResult.Value);
        }

        private void pictureBoxImageToAnalyze_Paint(object sender, PaintEventArgs e)
        {
            CalculateImageRatiosAndOffsets(pictureBoxImageToAnalyze, out Vector2 offset, out Vector2 ratio);

            if (_detectedTextLines != null)
            {
                foreach (var line in _detectedTextLines)
                {
                    // Draw bounding polygon
                    var boundingPolygon = line.BoundingPolygon;
                    PointF[] polygonPoints = {
                        new PointF(boundingPolygon[0].X * ratio.X + offset.X, boundingPolygon[0].Y * ratio.Y + offset.Y),
                        new PointF(boundingPolygon[1].X * ratio.X + offset.X, boundingPolygon[1].Y * ratio.Y + offset.Y),
                        new PointF(boundingPolygon[2].X * ratio.X + offset.X, boundingPolygon[2].Y * ratio.Y + offset.Y),
                        new PointF(boundingPolygon[3].X * ratio.X + offset.X, boundingPolygon[3].Y * ratio.Y + offset.Y)
                    };
                    e.Graphics.DrawPolygon(new Pen(Color.Orange, 3), polygonPoints);
                }
            }
        }

        private static void CalculateImageRatiosAndOffsets(PictureBox pictureBox, out Vector2 offset, out Vector2 ratio)
        {
            int displayedImageWidth, displayedImageHeight;

            var originalImage = pictureBox.Image;
            var imageAspectRatio = (float)originalImage.Width / originalImage.Height;
            var pictureBoxAspectRatio = (float)pictureBox.Width / pictureBox.Height;

            if (imageAspectRatio > pictureBoxAspectRatio)
            {
                // Gaps at top and bottom or picture box
                displayedImageWidth = pictureBox.Width;
                displayedImageHeight = (int)(pictureBox.Width / imageAspectRatio);
                offset.X = 0;
                offset.Y = (pictureBox.Height - displayedImageHeight) / 2;
            }
            else
            {
                // Gaps at left and right of picture box
                displayedImageWidth = (int)(pictureBox.Height * imageAspectRatio);
                displayedImageHeight = pictureBox.Height;
                offset.X = (pictureBox.Width - displayedImageWidth) / 2;
                offset.Y = 0;
            }

            ratio.X = (float)displayedImageWidth / originalImage.Width;
            ratio.Y = (float)displayedImageHeight / originalImage.Height;
        }

        private void ClearPreviousAnalysisResults()
        {
            textBoxReadResult.Text = string.Empty;
            _detectedTextLines = null;
            pictureBoxImageToAnalyze.Invalidate();
        }

        private static BinaryData GetBinaryDataFromImage(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return new BinaryData(memoryStream.ToArray());
            }
        }

        private static string GetAnalysisResultString(ImageAnalysisResult result)
        {
            StringBuilder sb = new StringBuilder();

            if (result.Read != null)
            {
                if (result.Read.Blocks.Count == 0)
                {
                    return "No text detected";
                }
                foreach (var block in result.Read.Blocks)
                {
                    foreach (var line in block.Lines)
                    {
                        if (line.Words.All(word => word.Confidence >= _confidenceThreshold))
                        {
                            sb.AppendLine(line.Text);
                        }
                    }    
                }    
            }
            
            return sb.ToString();
        }

        private void SetBoundingBoxes(ImageAnalysisResult result)
        {
            _detectedTextLines = result.Read?.Blocks.SelectMany(block => block.Lines).Where(line => line.Words.All(word => word.Confidence >= _confidenceThreshold)).ToList();
            pictureBoxImageToAnalyze.Invalidate();
        }
    }
}
