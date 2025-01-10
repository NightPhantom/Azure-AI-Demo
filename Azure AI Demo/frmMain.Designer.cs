namespace Azure_AI_Demo
{
    partial class frmMain
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
            textBoxTextToDetect = new TextBox();
            groupBoxDetectLanguage = new GroupBox();
            labelDetectedLanguage = new Label();
            buttonDetectLanguage = new Button();
            groupBoxDetectLanguage.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxTextToDetect
            // 
            textBoxTextToDetect.Location = new Point(6, 22);
            textBoxTextToDetect.Multiline = true;
            textBoxTextToDetect.Name = "textBoxTextToDetect";
            textBoxTextToDetect.Size = new Size(202, 72);
            textBoxTextToDetect.TabIndex = 0;
            // 
            // groupBoxDetectLanguage
            // 
            groupBoxDetectLanguage.Controls.Add(labelDetectedLanguage);
            groupBoxDetectLanguage.Controls.Add(buttonDetectLanguage);
            groupBoxDetectLanguage.Controls.Add(textBoxTextToDetect);
            groupBoxDetectLanguage.Location = new Point(12, 12);
            groupBoxDetectLanguage.Name = "groupBoxDetectLanguage";
            groupBoxDetectLanguage.Size = new Size(323, 101);
            groupBoxDetectLanguage.TabIndex = 1;
            groupBoxDetectLanguage.TabStop = false;
            groupBoxDetectLanguage.Text = "Detect Language";
            // 
            // labelDetectedLanguage
            // 
            labelDetectedLanguage.AutoSize = true;
            labelDetectedLanguage.Location = new Point(214, 48);
            labelDetectedLanguage.MaximumSize = new Size(105, 0);
            labelDetectedLanguage.Name = "labelDetectedLanguage";
            labelDetectedLanguage.Size = new Size(22, 15);
            labelDetectedLanguage.TabIndex = 2;
            labelDetectedLanguage.Text = "     ";
            // 
            // buttonDetectLanguage
            // 
            buttonDetectLanguage.Location = new Point(214, 22);
            buttonDetectLanguage.Name = "buttonDetectLanguage";
            buttonDetectLanguage.Size = new Size(75, 23);
            buttonDetectLanguage.TabIndex = 1;
            buttonDetectLanguage.Text = "Detect";
            buttonDetectLanguage.UseVisualStyleBackColor = true;
            buttonDetectLanguage.Click += buttonDetectLanguage_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBoxDetectLanguage);
            Name = "frmMain";
            Text = "Azure AI Demo";
            groupBoxDetectLanguage.ResumeLayout(false);
            groupBoxDetectLanguage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxTextToDetect;
        private GroupBox groupBoxDetectLanguage;
        private Label labelDetectedLanguage;
        private Button buttonDetectLanguage;
    }
}
