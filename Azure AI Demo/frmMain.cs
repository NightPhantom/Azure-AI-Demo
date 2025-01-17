
using Azure;
using Azure.AI.ContentSafety;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Windows.Forms;

namespace Azure_AI_Demo
{
    public partial class frmMain : Form
    {
        private TextAnalyticsClient? _languageClient;
        private ContentSafetyClient? _contentSafetyClient;
        private PromptShieldClient? _promptShieldClient;

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
            else
            {
                buttonDetectLanguage.Enabled = false;
            }

            // Azure AI Content Safety
            var contentSafetyEndpoint = Program.Configuration["AIContentSafetyEndpoint"];
            var contentSafetyKey = Program.Configuration["AIContentSafetyKey"];
            if (!string.IsNullOrEmpty(contentSafetyEndpoint) && !string.IsNullOrEmpty(contentSafetyKey))
            {
                var contentSafetyUri = new Uri(languageEndpoint);
                var contentSafetyCredentials = new AzureKeyCredential(languageKey);
                _contentSafetyClient = new ContentSafetyClient(contentSafetyUri, contentSafetyCredentials);
            }

            // Azure AI Prompt Shield (uses same endpoint and key as Content Safety)
            if (!string.IsNullOrEmpty(contentSafetyEndpoint) && !string.IsNullOrEmpty(contentSafetyKey))
            {
                var promptShieldUri = new Uri(contentSafetyEndpoint);
                _promptShieldClient = new PromptShieldClient(promptShieldUri, contentSafetyKey);
            }
            else
            {
                buttonTestPrompt.Enabled = false;
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

        private async void buttonTestPrompt_Click(object sender, EventArgs e)
        {
            if (_promptShieldClient == null)
            {
                labelPromptShieldResult.Text = "Error: Prompt shield client not initialized";
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxPromptShield.Text))
            {
                labelPromptShieldResult.Text = "Error: Prompt is empty";
                return;
            }

            // Show activity indicator
            var cancellationTokenSource = new CancellationTokenSource();
            var activityIndicatorTask = Task.Run(() => IndicateActivity(text => UpdateLabelSafely(labelPromptShieldResult, text), "Checking prompt for jailbreak attempt", cancellationTokenSource.Token));

            // Check prompt for jailbreak attempt
            PromptShieldResponse result = await _promptShieldClient.AnalyzePromptAsync(textBoxPromptShield.Text);

            // Stop activity indicator
            cancellationTokenSource.Cancel();
            await activityIndicatorTask;

            // Display prompt analysys result
            labelPromptShieldResult.Text = $"Jailbreak attempt: {(result.UserPromptAnalysis.AttackDetected ? "Yes" : "No")}";
        }
    }
}
