
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
        public frmMain()
        {
            InitializeComponent();
            tabPageDetectLanguage.Controls.Add(new DetectLanguageUserControl { Dock = DockStyle.Fill });
            tabPagePromptShield.Controls.Add(new PromptShieldUserControl { Dock = DockStyle.Fill });
            tabPageImageAnalysis.Controls.Add(new ImageAnalysisUserControl { Dock = DockStyle.Fill });
            tabPageSmartCrop.Controls.Add(new ImageSmartCropUserControl { Dock = DockStyle.Fill });
            tabPageFaceAnalysis.Controls.Add(new FaceUserControl { Dock = DockStyle.Fill });
            tabPageOCR.Controls.Add(new OCRUserControl { Dock = DockStyle.Fill });
        }
    }
}
