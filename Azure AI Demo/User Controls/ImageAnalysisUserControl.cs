﻿using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Azure_AI_Demo.Forms;
using Azure_AI_Demo.Utils;
using System.Numerics;
using System.Text;

namespace Azure_AI_Demo.User_Controls
{
    public partial class ImageAnalysisUserControl : UserControl
    {
        private ImageAnalysisClient? _imageClient;
        private List<DetectedPerson>? _detectedPeople;
        private List<DetectedObject>? _detectedObjects;

        private ToolTip? _toolTip;

        private static readonly float _confidenceThreshold = 0.5f;

        public ImageAnalysisUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
            InitializeTooltips();
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
                buttonAnalyzeImage.Enabled = true;
                buttonSetKey.BackColor = DefaultBackColor;
            }
            else
            {
                buttonAnalyzeImage.Enabled = false;
                buttonSetKey.BackColor = Color.LightGreen;
            }
        }

        private void InitializeTooltips()
        {
            _toolTip = new ToolTip();
            _toolTip.SetToolTip(buttonLoadImage, "Load an image to analyze");
            _toolTip.SetToolTip(buttonAnalyzeImage, "Analyze the loaded image");
            _toolTip.SetToolTip(buttonSetKey, "Set the key for the computer vision service");
        }

        private void buttonSetKey_Click(object sender, EventArgs e)
        {
            FormKeyInput formKeyInput = new FormKeyInput(FormKeyInput.Service.ComputerVision);
            formKeyInput.ShowDialog();
            LoadConfiguration();
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

        private async void buttonAnalyzeImage_Click(object sender, EventArgs e)
        {
            if (_imageClient == null)
            {
                labelImageAnalysisResult.Text = "Error: Image client not initialized";
                return;
            }
            if (pictureBoxImageToAnalyze.Image == null)
            {
                labelImageAnalysisResult.Text = "Error: Image to analyze is empty";
                return;
            }

            // Clear previous results
            ClearPreviousAnalysisResults();

            // Show activity indicator
            var cancellationTokenSource = new CancellationTokenSource();
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateTextSafely(labelImageAnalysisResult, text), "Analyzing image", cancellationTokenSource.Token));

            // Analyze image
            var imageData = GetBinaryDataFromImage(pictureBoxImageToAnalyze.Image);
            var visualFeatures = VisualFeatures.Caption
                | VisualFeatures.DenseCaptions
                | VisualFeatures.Tags
                | VisualFeatures.Objects
                | VisualFeatures.People;
            var imageAnalysisResult = await _imageClient.AnalyzeAsync(imageData, visualFeatures);

            // Stop Activity Indicator
            cancellationTokenSource.Cancel();
            await activityIndicatorTask;

            // Display image analysis result
            labelImageAnalysisResult.Text = GetAnalysisResultString(imageAnalysisResult.Value);
            SetBoundingBoxes(imageAnalysisResult.Value);
        }

        private void ClearPreviousAnalysisResults()
        {
            labelImageAnalysisResult.Text = string.Empty;
            _detectedPeople = null;
            _detectedObjects = null;
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

            if (result.Metadata != null)
            {
                sb.AppendLine($"Image width: {result.Metadata.Width}, height: {result.Metadata.Height}");
            }

            if (result.Caption != null)
            {
                sb.AppendLine($"Caption: {result.Caption.Text} ({result.Caption.Confidence * 100:F2}%)");
            }

            if (result.DenseCaptions != null)
            {
                sb.AppendLine("Dense Captions:");
                foreach (var denseCaption in result.DenseCaptions.Values)
                {
                    if (denseCaption.Confidence > _confidenceThreshold)
                    {
                        sb.AppendLine($"    {denseCaption.Text} ({denseCaption.Confidence * 100:F2}%)");
                    }
                }
            }

            if (result.Tags != null)
            {
                sb.AppendLine("Tags:");
                foreach (var tag in result.Tags.Values)
                {
                    if (tag.Confidence > _confidenceThreshold)
                    {
                        sb.AppendLine($"    {tag.Name} ({tag.Confidence * 100:F2}%)");
                    }
                }
            }

            if (result.Objects != null && result.Objects.Values.Count > 0)
            {
                sb.AppendLine("Objects:");
                foreach (var obj in result.Objects.Values)
                {
                    foreach (var objectTags in obj.Tags)
                    {
                        if (objectTags.Confidence > _confidenceThreshold)
                        {
                            sb.AppendLine($"    {objectTags.Name} ({objectTags.Confidence * 100:F2}%)");
                        }
                    }
                }
            }

            return sb.ToString();
        }

        private void SetBoundingBoxes(ImageAnalysisResult result)
        {
            if (result.People != null)
            {
                _detectedPeople = result.People.Values.ToList();
            }

            if (result.Objects != null)
            {
                _detectedObjects = result.Objects.Values.ToList();
            }
            pictureBoxImageToAnalyze.Invalidate();
        }

        private void pictureBoxImageToAnalyze_Paint(object sender, PaintEventArgs e)
        {
            CalculateImageRatiosAndOffsets(pictureBoxImageToAnalyze, out Vector2 offset, out Vector2 ratio);

            if (_detectedPeople != null)
            {
                foreach (var person in _detectedPeople)
                {
                    if (person.Confidence > _confidenceThreshold)
                    {
                        // Draw bounding box
                        var boundingBox = person.BoundingBox;
                        var x = boundingBox.X * ratio.X + offset.X;
                        var y = boundingBox.Y * ratio.Y + offset.Y;
                        var width = boundingBox.Width * ratio.X;
                        var height = boundingBox.Height * ratio.Y;
                        e.Graphics.DrawRectangle(Pens.Red, x, y, width, height);

                        // Draw the confidence score
                        string confidenceScore = $"{person.Confidence * 100:F2}%";
                        var font = new Font("Arial", 8);
                        var brush = new SolidBrush(Color.Red);
                        e.Graphics.DrawString(confidenceScore, font, brush, x, y);
                    }
                }
            }

            if (_detectedObjects != null)
            {
                foreach (var obj in _detectedObjects)
                {
                    if (obj.Tags.Any(tag => tag.Confidence > _confidenceThreshold))
                    {
                        // Draw bounding box
                        var boundingBox = obj.BoundingBox;
                        var x = boundingBox.X * ratio.X + offset.X;
                        var y = boundingBox.Y * ratio.Y + offset.Y;
                        var width = boundingBox.Width * ratio.X;
                        var height = boundingBox.Height * ratio.Y;
                        e.Graphics.DrawRectangle(Pens.Blue, x, y, width, height);

                        // Draw the tags and confidence scores
                        foreach (var tag in obj.Tags)
                        {
                            string tagString = $"{tag.Name} ({tag.Confidence * 100:F2}%)";
                            var font = new Font("Arial", 8);
                            var brush = new SolidBrush(Color.Blue);
                            e.Graphics.DrawString(tagString, font, brush, x, y);
                        }
                    }
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
    }
}
