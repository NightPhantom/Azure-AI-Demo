namespace Azure_AI_Demo.User_Controls
{
    partial class TextAnalysisUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextAnalysisUserControl));
            textBoxTextToAnalyze = new TextBox();
            buttonAnalyzeText = new Button();
            buttonSetKey = new Button();
            labelAnalysisResult = new Label();
            splitContainer = new SplitContainer();
            panelResults = new Panel();
            linkLabelAnalysisResult = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            panelResults.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxTextToAnalyze
            // 
            textBoxTextToAnalyze.Dock = DockStyle.Fill;
            textBoxTextToAnalyze.Location = new Point(0, 0);
            textBoxTextToAnalyze.Multiline = true;
            textBoxTextToAnalyze.Name = "textBoxTextToAnalyze";
            textBoxTextToAnalyze.PlaceholderText = "Enter text here";
            textBoxTextToAnalyze.Size = new Size(400, 600);
            textBoxTextToAnalyze.TabIndex = 1;
            textBoxTextToAnalyze.Text = resources.GetString("textBoxTextToAnalyze.Text");
            // 
            // buttonAnalyzeText
            // 
            buttonAnalyzeText.Image = Properties.Resources.Play;
            buttonAnalyzeText.Location = new Point(3, 3);
            buttonAnalyzeText.Name = "buttonAnalyzeText";
            buttonAnalyzeText.Size = new Size(24, 24);
            buttonAnalyzeText.TabIndex = 2;
            buttonAnalyzeText.UseVisualStyleBackColor = true;
            buttonAnalyzeText.Click += buttonAnalyzeText_Click;
            // 
            // buttonSetKey
            // 
            buttonSetKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSetKey.Image = Properties.Resources.EditKey;
            buttonSetKey.Location = new Point(369, 3);
            buttonSetKey.Name = "buttonSetKey";
            buttonSetKey.Size = new Size(24, 24);
            buttonSetKey.TabIndex = 4;
            buttonSetKey.UseVisualStyleBackColor = true;
            buttonSetKey.Click += buttonSetKey_Click;
            // 
            // labelAnalysisResult
            // 
            labelAnalysisResult.AutoSize = true;
            labelAnalysisResult.Location = new Point(61, 66);
            labelAnalysisResult.Name = "labelAnalysisResult";
            labelAnalysisResult.Size = new Size(22, 15);
            labelAnalysisResult.TabIndex = 5;
            labelAnalysisResult.Text = "     ";
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(textBoxTextToAnalyze);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(panelResults);
            splitContainer.Panel2.Controls.Add(buttonAnalyzeText);
            splitContainer.Panel2.Controls.Add(buttonSetKey);
            splitContainer.Size = new Size(800, 600);
            splitContainer.SplitterDistance = 400;
            splitContainer.TabIndex = 6;
            // 
            // panelResults
            // 
            panelResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelResults.AutoScroll = true;
            panelResults.Controls.Add(linkLabelAnalysisResult);
            panelResults.Controls.Add(labelAnalysisResult);
            panelResults.Location = new Point(3, 33);
            panelResults.Name = "panelResults";
            panelResults.Size = new Size(390, 564);
            panelResults.TabIndex = 6;
            // 
            // linkLabelAnalysisResult
            // 
            linkLabelAnalysisResult.AutoSize = true;
            linkLabelAnalysisResult.LinkArea = new LinkArea(0, 0);
            linkLabelAnalysisResult.Location = new Point(3, 0);
            linkLabelAnalysisResult.Name = "linkLabelAnalysisResult";
            linkLabelAnalysisResult.Size = new Size(22, 15);
            linkLabelAnalysisResult.TabIndex = 7;
            linkLabelAnalysisResult.Text = "     ";
            linkLabelAnalysisResult.LinkClicked += linkLabelAnalysisResult_LinkClicked;
            // 
            // TextAnalysisUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer);
            Name = "TextAnalysisUserControl";
            Size = new Size(800, 600);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            panelResults.ResumeLayout(false);
            panelResults.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxTextToAnalyze;
        private Button buttonAnalyzeText;
        private Button buttonSetKey;
        private Label labelAnalysisResult;
        private SplitContainer splitContainer;
        private Panel panelResults;
        private LinkLabel linkLabelAnalysisResult;
    }
}
