namespace Azure_AI_Demo
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControlFeatures = new TabControl();
            tabPageDetectLanguage = new TabPage();
            tabPagePromptShield = new TabPage();
            tabPageImageAnalysis = new TabPage();
            tabPageSmartCrop = new TabPage();
            tabPageFaceAnalysis = new TabPage();
            tabPageOCR = new TabPage();
            tabControlFeatures.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlFeatures
            // 
            tabControlFeatures.Controls.Add(tabPageDetectLanguage);
            tabControlFeatures.Controls.Add(tabPagePromptShield);
            tabControlFeatures.Controls.Add(tabPageImageAnalysis);
            tabControlFeatures.Controls.Add(tabPageSmartCrop);
            tabControlFeatures.Controls.Add(tabPageFaceAnalysis);
            tabControlFeatures.Controls.Add(tabPageOCR);
            tabControlFeatures.Dock = DockStyle.Fill;
            tabControlFeatures.Location = new Point(0, 0);
            tabControlFeatures.Name = "tabControlFeatures";
            tabControlFeatures.SelectedIndex = 0;
            tabControlFeatures.Size = new Size(784, 561);
            tabControlFeatures.TabIndex = 3;
            // 
            // tabPageDetectLanguage
            // 
            tabPageDetectLanguage.Location = new Point(4, 24);
            tabPageDetectLanguage.Name = "tabPageDetectLanguage";
            tabPageDetectLanguage.Padding = new Padding(3);
            tabPageDetectLanguage.Size = new Size(776, 533);
            tabPageDetectLanguage.TabIndex = 0;
            tabPageDetectLanguage.Text = "Detect Language";
            tabPageDetectLanguage.UseVisualStyleBackColor = true;
            // 
            // tabPagePromptShield
            // 
            tabPagePromptShield.Location = new Point(4, 24);
            tabPagePromptShield.Name = "tabPagePromptShield";
            tabPagePromptShield.Padding = new Padding(3);
            tabPagePromptShield.Size = new Size(776, 533);
            tabPagePromptShield.TabIndex = 1;
            tabPagePromptShield.Text = "Prompt Shield";
            tabPagePromptShield.UseVisualStyleBackColor = true;
            // 
            // tabPageImageAnalysis
            // 
            tabPageImageAnalysis.Location = new Point(4, 24);
            tabPageImageAnalysis.Name = "tabPageImageAnalysis";
            tabPageImageAnalysis.Padding = new Padding(3);
            tabPageImageAnalysis.Size = new Size(776, 533);
            tabPageImageAnalysis.TabIndex = 2;
            tabPageImageAnalysis.Text = "Image Analysis";
            tabPageImageAnalysis.UseVisualStyleBackColor = true;
            // 
            // tabPageSmartCrop
            // 
            tabPageSmartCrop.Location = new Point(4, 24);
            tabPageSmartCrop.Name = "tabPageSmartCrop";
            tabPageSmartCrop.Padding = new Padding(3);
            tabPageSmartCrop.Size = new Size(776, 533);
            tabPageSmartCrop.TabIndex = 3;
            tabPageSmartCrop.Text = "Smart Crop";
            tabPageSmartCrop.UseVisualStyleBackColor = true;
            // 
            // tabPageFaceAnalysis
            // 
            tabPageFaceAnalysis.Location = new Point(4, 24);
            tabPageFaceAnalysis.Name = "tabPageFaceAnalysis";
            tabPageFaceAnalysis.Padding = new Padding(3);
            tabPageFaceAnalysis.Size = new Size(776, 533);
            tabPageFaceAnalysis.TabIndex = 4;
            tabPageFaceAnalysis.Text = "Face Analysis";
            tabPageFaceAnalysis.UseVisualStyleBackColor = true;
            // 
            // tabPageOCR
            // 
            tabPageOCR.Location = new Point(4, 24);
            tabPageOCR.Name = "tabPageOCR";
            tabPageOCR.Padding = new Padding(3);
            tabPageOCR.Size = new Size(776, 533);
            tabPageOCR.TabIndex = 5;
            tabPageOCR.Text = "OCR";
            tabPageOCR.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(tabControlFeatures);
            Name = "frmMain";
            Text = "Azure AI Demo";
            tabControlFeatures.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControlFeatures;
        private TabPage tabPageDetectLanguage;
        private TabPage tabPagePromptShield;
        private TabPage tabPageImageAnalysis;
        private TabPage tabPageSmartCrop;
        private TabPage tabPageFaceAnalysis;
        private TabPage tabPageOCR;
    }
}
