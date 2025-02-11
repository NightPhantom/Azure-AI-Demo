using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace Azure_AI_Demo.Forms
{
    public partial class FormKeyInput : Form
    {
        public enum Service
        {
            Language,
            ContentSafety,
            ComputerVision,
            Face,
        }

        Service _service;

        public FormKeyInput(Service service)
        {
            _service = service;
            InitializeComponent();
            ConfigureUI();
        }

        private void ConfigureUI()
        {
            switch (_service)
            {
                case Service.Language:
                    this.Text = "Set Language Service Key";
                    textBoxEndpoint.Text = Program.Configuration["AILanguageServiceEndpoint"];
                    textBoxKey.Text = Program.Configuration["AILanguageServiceKey"];
                    break;
                case Service.ContentSafety:
                    this.Text = "Set Content Safety Service Key";
                    textBoxEndpoint.Text = Program.Configuration["AIContentSafetyEndpoint"];
                    textBoxKey.Text = Program.Configuration["AIContentSafetyKey"];
                    break;
                case Service.ComputerVision:
                    this.Text = "Set Computer Vision Service Key";
                    textBoxEndpoint.Text = Program.Configuration["AIComputerVisionServiceEndpoint"];
                    textBoxKey.Text = Program.Configuration["AIComputerVisionServiceKey"];
                    break;
                case Service.Face:
                    this.Text = "Set Face Service Key";
                    textBoxEndpoint.Text = Program.Configuration["AIVisionFaceServiceEndpoint"];
                    textBoxKey.Text = Program.Configuration["AIVisionFaceServiceKey"];
                    break;
                default:
                    break;
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            var secretsId = Assembly.GetExecutingAssembly().GetCustomAttribute<UserSecretsIdAttribute>()?.UserSecretsId;
            var secretsPath = PathHelper.GetSecretsPathFromSecretsId(secretsId);
            var secretsDirectory = Path.GetDirectoryName(secretsPath);

            switch (_service)
            {
                case Service.Language:
                    Program.Configuration["AILanguageServiceEndpoint"] = textBoxEndpoint.Text;
                    Program.Configuration["AILanguageServiceKey"] = textBoxKey.Text;
                    break;
                case Service.ContentSafety:
                    Program.Configuration["AIContentSafetyEndpoint"] = textBoxEndpoint.Text;
                    Program.Configuration["AIContentSafetyKey"] = textBoxKey.Text;
                    break;
                case Service.ComputerVision:
                    Program.Configuration["AIComputerVisionServiceEndpoint"] = textBoxEndpoint.Text;
                    Program.Configuration["AIComputerVisionServiceKey"] = textBoxKey.Text;
                    break;
                case Service.Face:
                    Program.Configuration["AIVisionFaceServiceEndpoint"] = textBoxEndpoint.Text;
                    Program.Configuration["AIVisionFaceServiceKey"] = textBoxKey.Text;
                    break;
                default:
                    break;
            }

            var secrets = new
            {
                AILanguageServiceEndpoint = Program.Configuration["AILanguageServiceEndpoint"],
                AILanguageServiceKey = Program.Configuration["AILanguageServiceKey"],
                AIContentSafetyEndpoint = Program.Configuration["AIContentSafetyEndpoint"],
                AIContentSafetyKey = Program.Configuration["AIContentSafetyKey"],
                AIComputerVisionServiceEndpoint = Program.Configuration["AIComputerVisionServiceEndpoint"],
                AIComputerVisionServiceKey = Program.Configuration["AIComputerVisionServiceKey"],
                AIVisionFaceServiceEndpoint = Program.Configuration["AIVisionFaceServiceEndpoint"],
                AIVisionFaceServiceKey = Program.Configuration["AIVisionFaceServiceKey"],
            };

            var secretsJson = JsonSerializer.Serialize(secrets, options: new JsonSerializerOptions { WriteIndented = true });

            if (!Directory.Exists(secretsDirectory))
            {
                Directory.CreateDirectory(secretsDirectory);
            }
            File.WriteAllText(secretsPath, secretsJson);

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
