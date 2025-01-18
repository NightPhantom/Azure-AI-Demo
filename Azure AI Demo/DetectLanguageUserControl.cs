using Azure.AI.TextAnalytics;
using Azure;

namespace Azure_AI_Demo
{
    public partial class DetectLanguageUserControl : UserControl
    {
        private TextAnalyticsClient? _languageClient;

        public DetectLanguageUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            // Azure AI Language service
            var languageEndpoint = Program.Configuration["AILanguageServiceEndpoint"];
            var languageKey = Program.Configuration["AILanguageServiceKey"];
            if (!string.IsNullOrEmpty(languageEndpoint) && !string.IsNullOrEmpty(languageKey))
            {
                var languageUri = new Uri(languageEndpoint);
                var languageCredentials = new AzureKeyCredential(languageKey);
                _languageClient = new TextAnalyticsClient(languageUri, languageCredentials);
            }
            else
            {
                buttonDetectLanguage.Enabled = false;
            }
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
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateLabelSafely(labelDetectedLanguage, text), "Detecting language", cancellationTokenSource.Token));

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
