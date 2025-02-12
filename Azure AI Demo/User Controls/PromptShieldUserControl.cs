using Azure;
using Azure.AI.ContentSafety;
using Azure_AI_Demo.Forms;
using Azure_AI_Demo.Utils;

namespace Azure_AI_Demo
{
    public partial class PromptShieldUserControl : UserControl
    {
        private PromptShieldClient? _promptShieldClient;

        private ToolTip? _toolTip;

        public PromptShieldUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
            InitializeTooltips();
        }

        private void LoadConfiguration()
        {
            var contentSafetyEndpoint = Program.Configuration["AIContentSafetyEndpoint"];
            var contentSafetyKey = Program.Configuration["AIContentSafetyKey"];

            if (!string.IsNullOrEmpty(contentSafetyEndpoint) && !string.IsNullOrEmpty(contentSafetyKey))
            {
                var promptShieldUri = new Uri(contentSafetyEndpoint);
                _promptShieldClient = new PromptShieldClient(promptShieldUri, contentSafetyKey);
                buttonTestPrompt.Enabled = true;
                buttonSetKey.BackColor = DefaultBackColor;
            }
            else
            {
                buttonTestPrompt.Enabled = false;
                buttonSetKey.BackColor = Color.LightGreen;
            }
        }

        private void InitializeTooltips()
        {
            _toolTip = new ToolTip();
            _toolTip.SetToolTip(buttonTestPrompt, "Test the prompt for jailbreak attempt");
            _toolTip.SetToolTip(buttonSetKey, "Set the key for the content safety service");
        }

        private void buttonSetKey_Click(object sender, EventArgs e)
        {
            FormKeyInput formKeyInput = new FormKeyInput(FormKeyInput.Service.ContentSafety);
            formKeyInput.ShowDialog();
            LoadConfiguration();
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
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateTextSafely(labelPromptShieldResult, text), "Checking prompt for jailbreak attempt", cancellationTokenSource.Token));

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
