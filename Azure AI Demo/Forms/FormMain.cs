using Azure_AI_Demo.User_Controls;

namespace Azure_AI_Demo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            tabPageTextAnalysis.Controls.Add(new TextAnalysisUserControl { Dock = DockStyle.Fill });
            tabPagePromptShield.Controls.Add(new PromptShieldUserControl { Dock = DockStyle.Fill });
            tabPageImageAnalysis.Controls.Add(new ImageAnalysisUserControl { Dock = DockStyle.Fill });
            tabPageSmartCrop.Controls.Add(new ImageSmartCropUserControl { Dock = DockStyle.Fill });
            tabPageFaceAnalysis.Controls.Add(new FaceUserControl { Dock = DockStyle.Fill });
            tabPageOCR.Controls.Add(new OCRUserControl { Dock = DockStyle.Fill });
        }
    }
}
