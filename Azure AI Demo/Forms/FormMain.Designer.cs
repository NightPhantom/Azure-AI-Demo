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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            tabPageOCR = new TabPage();
            tabPageFaceAnalysis = new TabPage();
            tabPageSmartCrop = new TabPage();
            tabPageImageAnalysis = new TabPage();
            tabPagePromptShield = new TabPage();
            tabPageTextAnalysis = new TabPage();
            tabControlFeatures = new TabControl();
            tabControlFeatures.SuspendLayout();
            SuspendLayout();
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
            // tabPageTextAnalysis
            // 
            tabPageTextAnalysis.Location = new Point(4, 24);
            tabPageTextAnalysis.Name = "tabPageTextAnalysis";
            tabPageTextAnalysis.Padding = new Padding(3);
            tabPageTextAnalysis.Size = new Size(776, 533);
            tabPageTextAnalysis.TabIndex = 6;
            tabPageTextAnalysis.Text = "Text Analysis";
            tabPageTextAnalysis.UseVisualStyleBackColor = true;
            // 
            // tabControlFeatures
            // 
            tabControlFeatures.Controls.Add(tabPageTextAnalysis);
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
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(tabControlFeatures);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            Text = "Azure AI Demo";
            tabControlFeatures.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tabPageOCR;
        private TabPage tabPageFaceAnalysis;
        private TabPage tabPageSmartCrop;
        private TabPage tabPageImageAnalysis;
        private TabPage tabPagePromptShield;
        private TabPage tabPageTextAnalysis;
        private TabControl tabControlFeatures;
    }
}
