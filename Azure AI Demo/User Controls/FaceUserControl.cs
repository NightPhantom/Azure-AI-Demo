using Azure;
using Azure.AI.Vision.Face;
using Azure_AI_Demo.Forms;
using Azure_AI_Demo.Utils;
using System.Numerics;
using System.Text;

namespace Azure_AI_Demo
{
    public partial class FaceUserControl : UserControl
    {
        private FaceClient? _faceClient;
        private Dictionary<int, FaceRectangle>? _detectedFaces;
        private List<FaceLandmarks>? _detectedLandmarks;

        private ToolTip? _toolTip;

        public FaceUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
            InitializeTooltips();
        }

        private void LoadConfiguration()
        {
            var visionFaceEndpoint = Program.Configuration["AIVisionFaceServiceEndpoint"];
            var visionFaceKey = Program.Configuration["AIVisionFaceServiceKey"];
            if (!string.IsNullOrEmpty(visionFaceEndpoint) && !string.IsNullOrEmpty(visionFaceKey))
            {
                var computerVisionUri = new Uri(visionFaceEndpoint);
                var computerVisionCredentials = new AzureKeyCredential(visionFaceKey);
                _faceClient = new FaceClient(computerVisionUri, computerVisionCredentials);
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
            _toolTip.SetToolTip(buttonSetKey, "Set the key for the face service");
        }

        private void buttonSetKey_Click(object sender, EventArgs e)
        {
            FormKeyInput formKeyInput = new FormKeyInput(FormKeyInput.Service.Face);
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
            if (_faceClient == null)
            {
                labelFaceDetectionResult.Text = "Error: Face client not initialized";
                return;
            }
            if (pictureBoxImageToAnalyze.Image == null)
            {
                labelFaceDetectionResult.Text = "Error: Image to analyze is empty";
                return;
            }

            // Clear previous results
            ClearPreviousAnalysisResults();

            // Show activity indicator
            var cancellationTokenSource = new CancellationTokenSource();
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateTextSafely(labelFaceDetectionResult, text), "Analyzing image", cancellationTokenSource.Token));

            // Detect faces
            var imageData = GetBinaryDataFromImage(pictureBoxImageToAnalyze.Image);
            var faceAttributesForDetection03 = new FaceAttributeType[]
            {
                FaceAttributeType.Blur,
                FaceAttributeType.Exposure,
                FaceAttributeType.Glasses,
                FaceAttributeType.HeadPose,
                FaceAttributeType.Mask,
                FaceAttributeType.Occlusion
            };
            var faceDetectionResult = await _faceClient.DetectAsync(imageData, FaceDetectionModel.Detection03, FaceRecognitionModel.Recognition04, false, faceAttributesForDetection03, true);

            // Stop Activity Indicator
            cancellationTokenSource.Cancel();
            await activityIndicatorTask;

            // Display face detection result
            labelFaceDetectionResult.Text = ProcessDetectionResults(faceDetectionResult.Value);
        }

        private void pictureBoxImageToAnalyze_Paint(object sender, PaintEventArgs e)
        {
            CalculateImageRatiosAndOffsets(pictureBoxImageToAnalyze, out Vector2 offset, out Vector2 ratio);

            if (_detectedFaces != null)
            {
                foreach (var face in _detectedFaces)
                {
                    // Draw bounding box
                    var faceRect = face.Value;
                    var x = faceRect.Left * ratio.X + offset.X;
                    var y = faceRect.Top * ratio.Y + offset.Y;
                    var width = faceRect.Width * ratio.X;
                    var height = faceRect.Height * ratio.Y;
                    e.Graphics.DrawRectangle(Pens.Red, x, y, width, height);

                    // Draw face number
                    var faceNumber = face.Key;
                    var font = new Font("Arial", 14);
                    var brush = new SolidBrush(Color.Red);
                    e.Graphics.DrawString($"{faceNumber}", font, brush, x, y);
                }
            }

            if (_detectedLandmarks != null)
            {
                foreach (var faceLandmarks in _detectedLandmarks)
                {
                    // Draw face landmarks
                    DrawFaceLandmarks(e.Graphics, faceLandmarks, offset, ratio);
                }
            }
        }

        private static void DrawFaceLandmarks(Graphics g, FaceLandmarks landmarks, Vector2 offset, Vector2 ratio)
        {
            // Connect landmarks with lines
            var linePen = new Pen(Color.Green, 1);

            // Eyebrows
            DrawLine(g, landmarks.EyebrowLeftInner, landmarks.EyebrowLeftOuter, linePen, offset, ratio);
            DrawLine(g, landmarks.EyebrowRightInner, landmarks.EyebrowRightOuter, linePen, offset, ratio);

            // Left eye
            DrawLine(g, landmarks.EyeLeftBottom, landmarks.EyeLeftInner, linePen, offset, ratio);
            DrawLine(g, landmarks.EyeLeftInner, landmarks.EyeLeftTop, linePen, offset, ratio);
            DrawLine(g, landmarks.EyeLeftTop, landmarks.EyeLeftOuter, linePen, offset, ratio);
            DrawLine(g, landmarks.EyeLeftOuter, landmarks.EyeLeftBottom, linePen, offset, ratio);

            // Right eye
            DrawLine(g, landmarks.EyeRightBottom, landmarks.EyeRightInner, linePen, offset, ratio);
            DrawLine(g, landmarks.EyeRightInner, landmarks.EyeRightTop, linePen, offset, ratio);
            DrawLine(g, landmarks.EyeRightTop, landmarks.EyeRightOuter, linePen, offset, ratio);
            DrawLine(g, landmarks.EyeRightOuter, landmarks.EyeRightBottom, linePen, offset, ratio);

            // Upper lip
            DrawLine(g, landmarks.MouthLeft, landmarks.UpperLipTop, linePen, offset, ratio);
            DrawLine(g, landmarks.UpperLipTop, landmarks.MouthRight, linePen, offset, ratio);
            DrawLine(g, landmarks.MouthRight, landmarks.UpperLipBottom, linePen, offset, ratio);
            DrawLine(g, landmarks.UpperLipBottom, landmarks.MouthLeft, linePen, offset, ratio);

            // Lower lip
            DrawLine(g, landmarks.MouthLeft, landmarks.UnderLipBottom, linePen, offset, ratio);
            DrawLine(g, landmarks.UnderLipBottom, landmarks.MouthRight, linePen, offset, ratio);
            DrawLine(g, landmarks.MouthRight, landmarks.UnderLipTop, linePen, offset, ratio);
            DrawLine(g, landmarks.UnderLipTop, landmarks.MouthLeft, linePen, offset, ratio);

            // Nose
            DrawLine(g, landmarks.NoseRootLeft, landmarks.NoseLeftAlarTop, linePen, offset, ratio);
            DrawLine(g, landmarks.NoseLeftAlarTop, landmarks.NoseLeftAlarOutTip, linePen, offset, ratio);
            DrawLine(g, landmarks.NoseLeftAlarOutTip, landmarks.NoseTip, linePen, offset, ratio);
            DrawLine(g, landmarks.NoseTip, landmarks.NoseRightAlarOutTip, linePen, offset, ratio);
            DrawLine(g, landmarks.NoseRightAlarOutTip, landmarks.NoseRightAlarTop, linePen, offset, ratio);
            DrawLine(g, landmarks.NoseRightAlarTop, landmarks.NoseRootRight, linePen, offset, ratio);

            // Draw landmarks
            var landmarkPen = new Pen(Color.Blue, 2);

            DrawLandmark(g, landmarks.EyebrowLeftInner, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyebrowLeftOuter, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyebrowRightInner, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyebrowRightOuter, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeLeftBottom, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeLeftInner, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeLeftOuter, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeLeftTop, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeRightBottom, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeRightInner, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeRightOuter, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.EyeRightTop, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.MouthLeft, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.MouthRight, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.NoseLeftAlarOutTip, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.NoseLeftAlarTop, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.NoseRightAlarOutTip, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.NoseRightAlarTop, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.NoseRootLeft, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.NoseRootRight, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.NoseTip, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.PupilLeft, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.PupilRight, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.UnderLipBottom, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.UnderLipTop, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.UpperLipBottom, landmarkPen, offset, ratio);
            DrawLandmark(g, landmarks.UpperLipTop, landmarkPen, offset, ratio);
        }

        private static void DrawLandmark(Graphics g, LandmarkCoordinate coordinate, Pen pen, Vector2 offset, Vector2 ratio)
        {
            var x = (coordinate.X * ratio.X + offset.X) - pen.Width / 2;
            var y = (coordinate.Y * ratio.Y + offset.Y) - pen.Width / 2;
            g.DrawEllipse(pen, x, y, pen.Width, pen.Width);
        }

        private static void DrawLine(Graphics g, LandmarkCoordinate start, LandmarkCoordinate end, Pen pen, Vector2 offset, Vector2 ratio)
        {
            var x1 = start.X * ratio.X + offset.X;
            var y1 = start.Y * ratio.Y + offset.Y;
            var x2 = end.X * ratio.X + offset.X;
            var y2 = end.Y * ratio.Y + offset.Y;
            g.DrawLine(pen, x1, y1, x2, y2);
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
            labelFaceDetectionResult.Text = string.Empty;
            _detectedFaces = null;
            _detectedLandmarks = null;
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

        private string ProcessDetectionResults(IReadOnlyList<FaceDetectionResult> results)
        {
            StringBuilder sb = new StringBuilder();

            var faceCount = 1;
            _detectedFaces = new Dictionary<int, FaceRectangle>();
            _detectedLandmarks = new List<FaceLandmarks>();

            foreach (var result in results)
            {
                sb.AppendLine($"Face {faceCount}");

                if (result.FaceAttributes?.Blur != null)
                {
                    sb.AppendLine($"    Blur: {result.FaceAttributes.Blur.Value:F3}");
                    sb.AppendLine($"        {result.FaceAttributes.Blur.BlurLevel}");
                }

                if (result.FaceAttributes?.Exposure != null)
                {
                    sb.AppendLine($"    Exposure: {result.FaceAttributes.Exposure.Value:F3}");
                    sb.AppendLine($"        {result.FaceAttributes.Exposure.ExposureLevel}");
                }

                if (result.FaceAttributes?.Glasses.HasValue == true)
                {
                    sb.AppendLine($"    Glasses: {result.FaceAttributes.Glasses.Value}");
                }

                if (result.FaceAttributes?.Mask != null)
                {
                    sb.AppendLine($"    Mask: {result.FaceAttributes.Mask.Type}");
                }

                if (result.FaceAttributes?.Occlusion != null)
                {
                    sb.AppendLine("    Occlusion:");
                    sb.AppendLine($"        Eye: {(result.FaceAttributes.Occlusion.EyeOccluded ? "Yes" : "No")}");
                    sb.AppendLine($"        Forehead: {(result.FaceAttributes.Occlusion.ForeheadOccluded ? "Yes" : "No")}");
                    sb.AppendLine($"        Mouth: {(result.FaceAttributes.Occlusion.MouthOccluded ? "Yes" : "No")}");
                }

                _detectedFaces.Add(faceCount, result.FaceRectangle);
                _detectedLandmarks.Add(result.FaceLandmarks);

                faceCount++;
            }

            pictureBoxImageToAnalyze.Invalidate();

            return sb.ToString();
        }
    }
}
