using Azure;
using Azure.AI.TextAnalytics;
using Azure_AI_Demo.Forms;
using Azure_AI_Demo.Utils;
using System.Diagnostics;
using System.Text;
using static System.Windows.Forms.LinkLabel;

namespace Azure_AI_Demo.User_Controls
{
    public partial class TextAnalysisUserControl : UserControl
    {
        private TextAnalyticsClient? _languageClient;

        private ToolTip? _toolTip;

        public TextAnalysisUserControl()
        {
            InitializeComponent();
            LoadConfiguration();
            InitializeTooltips();

            //var list = new List<string>()
            //{
            //    "One",
            //    "Two",
            //    "Three",
            //    "Four",
            //    "Five",
            //    "Six",
            //    "Seven",
            //    "Eight",
            //    "Nine",
            //    "Ten"
            //};
            //StringBuilder sb = new StringBuilder();
            //foreach (var item in list)
            //{
            //    sb.Append($"{item}\n");
            //}
            //var text = "One\nTwo\nThree\nFour\nFive\nSix\nSeven\nEight\nNine\nTen";
            //linkLabelAnalysisResult.Text = sb.ToString();

            //MessageBox.Show($"{text.Length},{sb.ToString().Length},{linkLabelAnalysisResult.Text.Length}");
            //foreach (char c in text)
            //{
            //    linkLabelAnalysisResult.Text += $"{(int)c} -> {c}\n";
            //}
            //foreach (char c in sb.ToString())
            //{
            //    linkLabelAnalysisResult.Text += $"{(int)c} -> {c}\n";
            //}

            //foreach (var item in list)
            //{
            //    var start = linkLabelAnalysisResult.Text.IndexOf(item);
            //    linkLabelAnalysisResult.Links.Add(start, item.Length, item);
            //}
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
                buttonAnalyzeText.Enabled = true;
                buttonSetKey.BackColor = DefaultBackColor;
            }
            else
            {
                buttonAnalyzeText.Enabled = false;
                buttonSetKey.BackColor = Color.LightGreen;
            }
        }

        private void InitializeTooltips()
        {
            _toolTip = new ToolTip();
            _toolTip.SetToolTip(buttonAnalyzeText, "Analyze the text");
            _toolTip.SetToolTip(buttonSetKey, "Set the key for the language service");
        }

        private void buttonSetKey_Click(object sender, EventArgs e)
        {
            FormKeyInput formKeyInput = new FormKeyInput(FormKeyInput.Service.Language);
            formKeyInput.ShowDialog();
            LoadConfiguration();
        }

        private async void buttonAnalyzeText_Click(object sender, EventArgs e)
        {
            if (_languageClient == null)
            {
                linkLabelAnalysisResult.Text = "Error: Language client not initialized";
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxTextToAnalyze.Text))
            {
                linkLabelAnalysisResult.Text = "Error: Text to detect is empty";
                return;
            }

            // Show activity indicator
            var cancellationTokenSource = new CancellationTokenSource();
            var activityIndicatorTask = Task.Run(() => ActivityIndicator.IndicateActivity(text => ActivityIndicator.UpdateTextSafely(linkLabelAnalysisResult, text), "Analyzing text", cancellationTokenSource.Token));

            try
            {
                // Detect language
                DetectedLanguage detectedLanguage = await _languageClient.DetectLanguageAsync(textBoxTextToAnalyze.Text);

                // Detect sentiment
                DocumentSentiment documentSentiment = await _languageClient.AnalyzeSentimentAsync(textBoxTextToAnalyze.Text);

                // Detect key phrases
                KeyPhraseCollection keyPhrases = await _languageClient.ExtractKeyPhrasesAsync(textBoxTextToAnalyze.Text);

                // Detect entities
                CategorizedEntityCollection entities = await _languageClient.RecognizeEntitiesAsync(textBoxTextToAnalyze.Text);

                // Detect linked entities
                LinkedEntityCollection linkedEntities = await _languageClient.RecognizeLinkedEntitiesAsync(textBoxTextToAnalyze.Text);

                // Detect personally identifiable information
                PiiEntityCollection personallyIdentifiableInformation = await _languageClient.RecognizePiiEntitiesAsync(textBoxTextToAnalyze.Text);

                // Stop activity indicator
                cancellationTokenSource.Cancel();
                await activityIndicatorTask;

                // Display results of text analysis
                StringBuilder analysisResult = new StringBuilder();
                analysisResult.AppendResponse(detectedLanguage)
                    .AppendResponse(documentSentiment)
                    .AppendResponse(keyPhrases)
                    .AppendResponse(entities)
                    .AppendResponse(linkedEntities, out List<Link> links)
                    .AppendResponse(personallyIdentifiableInformation);
                linkLabelAnalysisResult.Invoke((MethodInvoker)(() => linkLabelAnalysisResult.Text = analysisResult.ToString()));

                // Display links
                linkLabelAnalysisResult.Links.Clear();
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        linkLabelAnalysisResult.Links.Add(link);
                    }
                }
            }
            catch (Exception ex)
            {
                linkLabelAnalysisResult.Invoke((MethodInvoker)(() => linkLabelAnalysisResult.Text = $"Error: {ex.Message}"));
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }

        private void linkLabelAnalysisResult_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link?.LinkData == null)
            {
                return;
            }

            var url = e.Link.LinkData.ToString();
            var processStartInfo = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(processStartInfo);
        }
    }

    internal static class TextAnalysisExtensions
    {
        internal static StringBuilder AppendResponse(this StringBuilder sb, DetectedLanguage detectedLanguage)
        {
            sb.Append($"Language: {detectedLanguage.Name}\n");
            sb.Append($"    ISO: {detectedLanguage.Iso6391Name}\n");
            sb.Append($"    Confidence: {detectedLanguage.ConfidenceScore * 100:F2}\n");
            return sb;
        }

        internal static StringBuilder AppendResponse(this StringBuilder sb, DocumentSentiment documentSentiment)
        {
            sb.Append($"Sentiment: {documentSentiment.Sentiment}\n");
            sb.Append($"    Positive: {documentSentiment.ConfidenceScores.Positive * 100:F2}\n");
            sb.Append($"    Neutral: {documentSentiment.ConfidenceScores.Neutral * 100:F2}\n");
            sb.Append($"    Negative: {documentSentiment.ConfidenceScores.Negative * 100:F2}\n");
            return sb;
        }

        internal static StringBuilder AppendResponse(this StringBuilder sb, KeyPhraseCollection phrases)
        {
            if (phrases.Count > 0)
            {
                sb.Append("Key Phrases:\n");
            }

            foreach (var phrase in phrases)
            {
                sb.Append($"    {phrase}\n");
            }
            return sb;
        }

        internal static StringBuilder AppendResponse(this StringBuilder sb, CategorizedEntityCollection entities)
        {
            if (entities.Count > 0)
            {
                sb.Append("Entities:\n");
            }

            foreach (var entity in entities)
            {
                if (entity.ConfidenceScore > 0.5)
                {
                    sb.Append($"    {entity.Text} ({entity.Category}) ({entity.ConfidenceScore * 100:F2})\n");
                }
            }
            return sb;
        }

        internal static StringBuilder AppendResponse(this StringBuilder sb, LinkedEntityCollection linkEntities, out List<Link> links)
        {
            if (linkEntities.Count > 0)
            {
                sb.Append("Links:\n");
            }

            links = new List<Link>();

            foreach (var linkEntity in linkEntities)
            {
                sb.Append($"    {linkEntity.Name}: ");
                links.Add(new Link(sb.Length, linkEntity.Url.OriginalString.Length, linkEntity.Url.OriginalString));
                sb.Append($"{linkEntity.Url}\n");
            }
            return sb;
        }

        internal static StringBuilder AppendResponse(this StringBuilder sb, PiiEntityCollection personallyIdentifiableInformation)
        {
            if (personallyIdentifiableInformation.Count > 0)
            {
                sb.Append("Personally Identifiable Information:\n");
            }

            foreach (var pii in personallyIdentifiableInformation)
            {
                if (pii.ConfidenceScore > 0.5)
                {
                    sb.Append($"    {pii.Text} ({pii.Category} {pii.SubCategory}) ({pii.ConfidenceScore * 100:F2})\n");
                }
            }
            return sb;
        }
    }
}
