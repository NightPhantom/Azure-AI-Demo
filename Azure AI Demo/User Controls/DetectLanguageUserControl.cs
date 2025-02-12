using Azure.AI.TextAnalytics;
using Azure;
using Azure_AI_Demo.Utils;
using Azure_AI_Demo.Forms;

namespace Azure_AI_Demo.User_Controls
{
    public partial class DetectLanguageUserControl : UserControl
    {
        private TextAnalyticsClient? _languageClient;

        private ToolTip? _toolTip;

        public DetectLanguageUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
            InitializeTooltips();
        }

        private void LoadConfiguration()
        {
            var languageEndpoint = Program.Configuration["AILanguageServiceEndpoint"];
            var languageKey = Program.Configuration["AILanguageServiceKey"];
            if (!string.IsNullOrEmpty(languageEndpoint) && !string.IsNullOrEmpty(languageKey))
            {
                var languageUri = new Uri(languageEndpoint);
                var languageCredentials = new AzureKeyCredential(languageKey);
                _languageClient = new TextAnalyticsClient(languageUri, languageCredentials);
                buttonDetectLanguage.Enabled = true;
                buttonSetKey.BackColor = DefaultBackColor;
            }
            else
            {
                buttonDetectLanguage.Enabled = false;
                buttonSetKey.BackColor = Color.LightGreen;
            }
        }

        private void InitializeTooltips()
        {
            _toolTip = new ToolTip();
            _toolTip.SetToolTip(buttonDetectLanguage, "Detect the language of the text");
            _toolTip.SetToolTip(buttonSetKey, "Set the key for the language service");
        }

        private void buttonSetKey_Click(object sender, EventArgs e)
        {
            FormKeyInput formKeyInput = new FormKeyInput(FormKeyInput.Service.Language);
            formKeyInput.ShowDialog();
            LoadConfiguration();
        }

        private async void buttonDetectLanguage_Click(object sender, EventArgs e)
        {
            if (_languageClient == null)
            {
                labelDetectedLanguage.Text = "Error: Language client not initialized";
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxTextToDetect.Text))
            {
                labelDetectedLanguage.Text = "Error: Text to detect is empty";
                return;
            }

            // Show activity indicator
            var cancellationTokenSource = new CancellationTokenSource();
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateTextSafely(labelDetectedLanguage, text), "Detecting language", cancellationTokenSource.Token));

            // Detect language
            DetectedLanguage detectedLanguage = await _languageClient.DetectLanguageAsync(textBoxTextToDetect.Text);

            // Stop activity indicator
            cancellationTokenSource.Cancel();
            await activityIndicatorTask;

            // Display detected language information
            labelDetectedLanguage.Text = $"Name: {detectedLanguage.Name}\nISO: {detectedLanguage.Iso6391Name}\nConfidence: {detectedLanguage.ConfidenceScore}";
        }
    }
}
