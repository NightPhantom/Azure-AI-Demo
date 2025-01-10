
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;

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
            var languageUri = new Uri(languageEndpoint);
            var languageCredentials = new AzureKeyCredential(languageKey);
            _languageClient = new TextAnalyticsClient(languageUri, languageCredentials);
        }
    }
}
