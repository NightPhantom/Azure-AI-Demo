using Azure;
using Azure.AI.ContentSafety;

namespace Azure_AI_Demo
{
    public partial class PromptShieldUserControl : UserControl
    {
        private PromptShieldClient? _promptShieldClient;

        public PromptShieldUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            var contentSafetyEndpoint = Program.Configuration["AIContentSafetyEndpoint"];
            var contentSafetyKey = Program.Configuration["AIContentSafetyKey"];

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
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateLabelSafely(labelPromptShieldResult, text), "Checking prompt for jailbreak attempt", cancellationTokenSource.Token));

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
