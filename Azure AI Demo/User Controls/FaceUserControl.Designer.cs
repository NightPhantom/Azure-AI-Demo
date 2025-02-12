namespace Azure_AI_Demo
{
    partial class FaceUserControl
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
            labelFaceDetectionResult = new Label();
            buttonAnalyzeImage = new Button();
            buttonLoadImage = new Button();
            pictureBoxImageToAnalyze = new PictureBox();
            panelResult = new Panel();
            buttonSetKey = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageToAnalyze).BeginInit();
            panelResult.SuspendLayout();
            SuspendLayout();
            // 
            // labelFaceDetectionResult
            // 
            labelFaceDetectionResult.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelFaceDetectionResult.AutoSize = true;
            labelFaceDetectionResult.Location = new Point(15, 0);
            labelFaceDetectionResult.MaximumSize = new Size(175, 0);
            labelFaceDetectionResult.Name = "labelFaceDetectionResult";
            labelFaceDetectionResult.Size = new Size(22, 15);
            labelFaceDetectionResult.TabIndex = 7;
            labelFaceDetectionResult.Text = "     ";
            // 
            // buttonAnalyzeImage
            // 
            buttonAnalyzeImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAnalyzeImage.Image = Properties.Resources.Play;
            buttonAnalyzeImage.Location = new Point(606, 3);
            buttonAnalyzeImage.Name = "buttonAnalyzeImage";
            buttonAnalyzeImage.Size = new Size(24, 24);
            buttonAnalyzeImage.TabIndex = 6;
            buttonAnalyzeImage.UseVisualStyleBackColor = true;
            buttonAnalyzeImage.Click += buttonAnalyzeImage_Click;
            // 
            // buttonLoadImage
            // 
            buttonLoadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoadImage.Image = Properties.Resources.OpenFile;
            buttonLoadImage.Location = new Point(636, 3);
            buttonLoadImage.Name = "buttonLoadImage";
            buttonLoadImage.Size = new Size(24, 24);
            buttonLoadImage.TabIndex = 5;
            buttonLoadImage.UseVisualStyleBackColor = true;
            buttonLoadImage.Click += buttonLoadImage_Click;
            // 
            // pictureBoxImageToAnalyze
            // 
            pictureBoxImageToAnalyze.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxImageToAnalyze.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxImageToAnalyze.Image = Properties.Resources.face_sample;
            pictureBoxImageToAnalyze.Location = new Point(0, 0);
            pictureBoxImageToAnalyze.Name = "pictureBoxImageToAnalyze";
            pictureBoxImageToAnalyze.Size = new Size(600, 600);
            pictureBoxImageToAnalyze.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImageToAnalyze.TabIndex = 4;
            pictureBoxImageToAnalyze.TabStop = false;
            pictureBoxImageToAnalyze.Paint += pictureBoxImageToAnalyze_Paint;
            // 
            // panelResult
            // 
            panelResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelResult.AutoScroll = true;
            panelResult.Controls.Add(labelFaceDetectionResult);
            panelResult.Location = new Point(606, 33);
            panelResult.Name = "panelResult";
            panelResult.Size = new Size(191, 564);
            panelResult.TabIndex = 8;
            // 
            // buttonSetKey
            // 
            buttonSetKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSetKey.Image = Properties.Resources.EditKey;
            buttonSetKey.Location = new Point(773, 3);
            buttonSetKey.Name = "buttonSetKey";
            buttonSetKey.Size = new Size(24, 24);
            buttonSetKey.TabIndex = 9;
            buttonSetKey.UseVisualStyleBackColor = true;
            buttonSetKey.Click += buttonSetKey_Click;
            // 
            // FaceUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonSetKey);
            Controls.Add(panelResult);
            Controls.Add(buttonAnalyzeImage);
            Controls.Add(pictureBoxImageToAnalyze);
            Controls.Add(buttonLoadImage);
            Name = "FaceUserControl";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageToAnalyze).EndInit();
            panelResult.ResumeLayout(false);
            panelResult.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelFaceDetectionResult;
        private Button buttonAnalyzeImage;
        private Button buttonLoadImage;
        private PictureBox pictureBoxImageToAnalyze;
        private Panel panelResult;
        private Button buttonSetKey;
    }
}
