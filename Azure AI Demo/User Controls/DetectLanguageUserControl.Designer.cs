namespace Azure_AI_Demo.User_Controls
{
    partial class DetectLanguageUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelDetectedLanguage = new Label();
            buttonDetectLanguage = new Button();
            textBoxTextToDetect = new TextBox();
            buttonSetKey = new Button();
            SuspendLayout();
            // 
            // labelDetectedLanguage
            // 
            labelDetectedLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelDetectedLanguage.AutoSize = true;
            labelDetectedLanguage.Location = new Point(249, 30);
            labelDetectedLanguage.MaximumSize = new Size(100, 0);
            labelDetectedLanguage.Name = "labelDetectedLanguage";
            labelDetectedLanguage.Size = new Size(22, 15);
            labelDetectedLanguage.TabIndex = 2;
            labelDetectedLanguage.Text = "     ";
            // 
            // buttonDetectLanguage
            // 
            buttonDetectLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDetectLanguage.Image = Properties.Resources.Play;
            buttonDetectLanguage.Location = new Point(249, 3);
            buttonDetectLanguage.Name = "buttonDetectLanguage";
            buttonDetectLanguage.Size = new Size(24, 24);
            buttonDetectLanguage.TabIndex = 1;
            buttonDetectLanguage.UseVisualStyleBackColor = true;
            buttonDetectLanguage.Click += buttonDetectLanguage_Click;
            // 
            // textBoxTextToDetect
            // 
            textBoxTextToDetect.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxTextToDetect.Location = new Point(3, 3);
            textBoxTextToDetect.Multiline = true;
            textBoxTextToDetect.Name = "textBoxTextToDetect";
            textBoxTextToDetect.PlaceholderText = "Enter text here";
            textBoxTextToDetect.Size = new Size(240, 177);
            textBoxTextToDetect.TabIndex = 0;
            // 
            // buttonSetKey
            // 
            buttonSetKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSetKey.Image = Properties.Resources.EditKey;
            buttonSetKey.Location = new Point(323, 3);
            buttonSetKey.Name = "buttonSetKey";
            buttonSetKey.Size = new Size(24, 24);
            buttonSetKey.TabIndex = 3;
            buttonSetKey.UseVisualStyleBackColor = true;
            buttonSetKey.Click += buttonSetKey_Click;
            // 
            // DetectLanguageUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonSetKey);
            Controls.Add(labelDetectedLanguage);
            Controls.Add(buttonDetectLanguage);
            Controls.Add(textBoxTextToDetect);
            Name = "DetectLanguageUserControl";
            Size = new Size(350, 183);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelDetectedLanguage;
        private Button buttonDetectLanguage;
        private TextBox textBoxTextToDetect;
        private Button buttonSetKey;
    }
}
