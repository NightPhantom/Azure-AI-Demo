
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms;

namespace Azure_AI_Demo
{
    public partial class frmMain : Form
    {
        private TextAnalyticsClient? _languageClient;

        public frmMain()
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
        }

        private async Task IndicateActivity(Action<string> updateLabel, string message, CancellationToken ct)
        {
            var activityIndicator = "-";
            while (!ct.IsCancellationRequested)
            {
                updateLabel($"{message} {activityIndicator}");
                switch (activityIndicator)
                {
                    case "-":
                        activityIndicator = "\\";
                        break;
                    case "\\":
                        activityIndicator = "|";
                        break;
                    case "|":
                        activityIndicator = "/";
                        break;
                    case "/":
                        activityIndicator = "-";
                        break;
                    default:
                        break;
                }
                Thread.Sleep(100);
            }

            updateLabel(string.Empty);
            await Task.CompletedTask;
        }

        private void UpdateLabelSafely(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => label.Text = text));
            }
            else
            {
                label.Text = text;
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
            var activityIndicatorTask = Task.Run(() => IndicateActivity(text => UpdateLabelSafely(labelDetectedLanguage, text), "Detecting language", cancellationTokenSource.Token));

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
