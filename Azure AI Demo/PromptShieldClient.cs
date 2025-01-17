
using Azure;
using Azure.AI.ContentSafety;
using Azure.Core;
using Azure.Core.Pipeline;
using System.Text.Json;
using static System.Windows.Forms.Design.AxImporter;

namespace Azure_AI_Demo
{
    public class PromptShieldClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly Uri _endpoint;
        private readonly string _key;
        private readonly string _apiVersion;

        public PromptShieldClient(Uri endpoint, string key, string apiVersion = "2024-09-01")
        {
            _endpoint = endpoint;
            _key = key;
            _apiVersion = "2024-09-01";
        }

        public async Task<PromptShieldResponse> AnalyzePromptAsync(string text, CancellationToken cancellationToken = default)
        {
            // Set up the API request
            Uri url = new Uri(_endpoint, $"/contentsafety/text:shieldPrompt?api-version={_apiVersion}");
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var promptShieldRequest = new PromptShieldRequest(text, new[] { text });

            string payload = JsonSerializer.Serialize(promptShieldRequest, promptShieldRequest.GetType());

            using var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync(url, content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync(cancellationToken);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var promptShieldResponse = JsonSerializer.Deserialize<PromptShieldResponse>(result, options);
                    return promptShieldResponse;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }

    public class PromptShieldRequest
    {
        public string UserPrompt { get; set; }
        public string[] Documents { get; set; }

        public PromptShieldRequest(string userPrompt, string[] documents)
        {
            UserPrompt = userPrompt ?? throw new ArgumentNullException(nameof(userPrompt));
            Documents = documents ?? throw new ArgumentNullException(nameof(documents));
            if (documents.Length == 0)
            {
                throw new ArgumentException("At least one document must be provided.", nameof(documents));
            }
        }
    }

    public class PromptShieldResponse
    {
        public required UserPromptAnalysis UserPromptAnalysis { get; set; }
        public required List<DocumentAnalysis> DocumentsAnalysis { get; set; }
    }

    public class UserPromptAnalysis
    {
        public bool AttackDetected { get; set; }
    }

    public class DocumentAnalysis
    {
        public bool AttackDetected { get; set; }
    }
}
