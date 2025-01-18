namespace Azure_AI_Demo
{
    partial class ImageAnalysisUserControl
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
            pictureBoxImageToAnalyze = new PictureBox();
            buttonLoadImage = new Button();
            buttonAnalyzeImage = new Button();
            labelImageAnalysisResult = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageToAnalyze).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxImageToAnalyze
            // 
            pictureBoxImageToAnalyze.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxImageToAnalyze.Image = Properties.Resources.Palácio_do_Congresso_Nacional__52780142794_;
            pictureBoxImageToAnalyze.Location = new Point(0, 0);
            pictureBoxImageToAnalyze.Name = "pictureBoxImageToAnalyze";
            pictureBoxImageToAnalyze.Size = new Size(500, 600);
            pictureBoxImageToAnalyze.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImageToAnalyze.TabIndex = 0;
            pictureBoxImageToAnalyze.TabStop = false;
            pictureBoxImageToAnalyze.Paint += pictureBoxImageToAnalyze_Paint;
            // 
            // buttonLoadImage
            // 
            buttonLoadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoadImage.Location = new Point(506, 3);
            buttonLoadImage.Name = "buttonLoadImage";
            buttonLoadImage.Size = new Size(90, 23);
            buttonLoadImage.TabIndex = 1;
            buttonLoadImage.Text = "Load Image";
            buttonLoadImage.UseVisualStyleBackColor = true;
            buttonLoadImage.Click += buttonLoadImage_Click;
            // 
            // buttonAnalyzeImage
            // 
            buttonAnalyzeImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAnalyzeImage.Location = new Point(506, 32);
            buttonAnalyzeImage.Name = "buttonAnalyzeImage";
            buttonAnalyzeImage.Size = new Size(90, 23);
            buttonAnalyzeImage.TabIndex = 2;
            buttonAnalyzeImage.Text = "Analyze";
            buttonAnalyzeImage.UseVisualStyleBackColor = true;
            buttonAnalyzeImage.Click += buttonAnalyzeImage_Click;
            // 
            // labelImageAnalysisResult
            // 
            labelImageAnalysisResult.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelImageAnalysisResult.AutoSize = true;
            labelImageAnalysisResult.Location = new Point(506, 58);
            labelImageAnalysisResult.MaximumSize = new Size(290, 0);
            labelImageAnalysisResult.Name = "labelImageAnalysisResult";
            labelImageAnalysisResult.Size = new Size(22, 15);
            labelImageAnalysisResult.TabIndex = 3;
            labelImageAnalysisResult.Text = "     ";
            // 
            // ImageAnalysisUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelImageAnalysisResult);
            Controls.Add(buttonAnalyzeImage);
            Controls.Add(buttonLoadImage);
            Controls.Add(pictureBoxImageToAnalyze);
            Name = "ImageAnalysisUserControl";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageToAnalyze).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxImageToAnalyze;
        private Button buttonLoadImage;
        private Button buttonAnalyzeImage;
        private Label labelImageAnalysisResult;
    }
}
