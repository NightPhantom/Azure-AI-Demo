using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Azure_AI_Demo.Forms;
using Azure_AI_Demo.Utils;

namespace Azure_AI_Demo.User_Controls
{
    public partial class ImageSmartCropUserControl : UserControl
    {
        private ImageAnalysisClient? _imageClient;

        private ToolTip? _toolTip;

        private string? _errorMessage;

        public ImageSmartCropUserControl()
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
            _toolTip.SetToolTip(buttonAnalyzeImage, "Analyze the image");
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
                    pictureBoxOriginalImage.Image = Image.FromFile(filePath);
                }
            }
        }

        private async void buttonAnalyzeImage_Click(object sender, EventArgs e)
        {
            if (_imageClient == null)
            {
                _errorMessage = "Error: Image client not initialized";
                pictureBoxResultImage.Invalidate();
                return;
            }
            if (pictureBoxOriginalImage.Image == null)
            {
                _errorMessage = "Error: Image to analyze is empty";
                pictureBoxResultImage.Invalidate();
                return;
            }

            // Clear previous results
            ClearPreviousAnalysisResults();

            // Show activity indicator
            var cancellationTokenSource = new CancellationTokenSource();
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateTextSafely(labelImageAnalysisResult, text), "Analyzing image", cancellationTokenSource.Token));

            // Analyze image
            var imageData = GetBinaryDataFromImage(pictureBoxOriginalImage.Image);
            var visualFeatures = VisualFeatures.SmartCrops;
            var options = new ImageAnalysisOptions
            {
                SmartCropsAspectRatios = new float[] { 0.75F, 0.9F, 1.0F, 1.33F, 1.5F, 1.8F }
            };
            var imageAnalysisResult = await _imageClient.AnalyzeAsync(imageData, visualFeatures, options);

            // Stop Activity Indicator
            cancellationTokenSource.Cancel();
            await activityIndicatorTask;
            ActivityIndicator.UpdateTextSafely(labelImageAnalysisResult, string.Empty);

            // Display cropped image
            var cropResults = imageAnalysisResult.Value.SmartCrops.Values.ToDictionary(crop => crop.AspectRatio, crop => crop.BoundingBox);
            LoadAndShowCropResults(cropResults);
        }

        private void pictureBoxThumbnail_Paint(object sender, PaintEventArgs e)
        {
            if (_errorMessage != null)
            {
                var font = new Font("Arial", 8);
                var brush = new SolidBrush(Color.Blue);
                e.Graphics.DrawString(_errorMessage, font, brush, 0, 0);
            }
        }

        private void comboBoxCrops_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCrops.SelectedItem is KeyValuePair<string, ImageBoundingBox> selectedCrop)
            {
                var croppedImage = CropImage(pictureBoxOriginalImage.Image, selectedCrop.Value);
                pictureBoxResultImage.Image = croppedImage;
            }
        }

        private static BinaryData GetBinaryDataFromImage(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return new BinaryData(memoryStream.ToArray());
            }
        }

        private void ClearPreviousAnalysisResults()
        {
            comboBoxCrops.Items.Clear();
            pictureBoxResultImage.Image = null;
            pictureBoxResultImage.Invalidate();
        }

        private void LoadAndShowCropResults(Dictionary<float, ImageBoundingBox> cropResults)
        {
            comboBoxCrops.Items.Clear();
            foreach (var crop in cropResults)
            {
                comboBoxCrops.Items.Add(new KeyValuePair<string, ImageBoundingBox>($"Aspect ratio: {crop.Key}", crop.Value));
            }
            comboBoxCrops.DisplayMember = "Key";
            comboBoxCrops.ValueMember = "Value";
            comboBoxCrops.SelectedIndex = 0;
        }

        private Image CropImage(Image original, ImageBoundingBox boundingBox)
        {
            var cropped = new Bitmap(boundingBox.Width, boundingBox.Height);

            using (var graphics = Graphics.FromImage(cropped))
            {
                graphics.DrawImage(
                    original,
                    new Rectangle(0, 0, boundingBox.Width, boundingBox.Height),
                    new Rectangle(boundingBox.X, boundingBox.Y, boundingBox.Width, boundingBox.Height),
                    GraphicsUnit.Pixel);
            }

            return cropped;
        }
    }
}
