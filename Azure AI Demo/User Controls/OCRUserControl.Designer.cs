namespace Azure_AI_Demo
{
    partial class OCRUserControl
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
            buttonReadImage = new Button();
            buttonLoadImage = new Button();
            pictureBoxImageToAnalyze = new PictureBox();
            textBoxReadResult = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageToAnalyze).BeginInit();
            SuspendLayout();
            // 
            // buttonReadImage
            // 
            buttonReadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonReadImage.Location = new Point(506, 32);
            buttonReadImage.Name = "buttonReadImage";
            buttonReadImage.Size = new Size(90, 23);
            buttonReadImage.TabIndex = 6;
            buttonReadImage.Text = "Read";
            buttonReadImage.UseVisualStyleBackColor = true;
            buttonReadImage.Click += buttonReadImage_Click;
            // 
            // buttonLoadImage
            // 
            buttonLoadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoadImage.Location = new Point(506, 3);
            buttonLoadImage.Name = "buttonLoadImage";
            buttonLoadImage.Size = new Size(90, 23);
            buttonLoadImage.TabIndex = 5;
            buttonLoadImage.Text = "Load Image";
            buttonLoadImage.UseVisualStyleBackColor = true;
            buttonLoadImage.Click += buttonLoadImage_Click;
            // 
            // pictureBoxImageToAnalyze
            // 
            pictureBoxImageToAnalyze.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxImageToAnalyze.Image = Properties.Resources.conversao_a_direita_antes_de_semaforo_na_avenida_brasil_florindo_cibin_2048x1154;
            pictureBoxImageToAnalyze.Location = new Point(0, 0);
            pictureBoxImageToAnalyze.Name = "pictureBoxImageToAnalyze";
            pictureBoxImageToAnalyze.Size = new Size(500, 600);
            pictureBoxImageToAnalyze.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImageToAnalyze.TabIndex = 4;
            pictureBoxImageToAnalyze.TabStop = false;
            pictureBoxImageToAnalyze.Paint += pictureBoxImageToAnalyze_Paint;
            // 
            // textBoxReadResult
            // 
            textBoxReadResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            textBoxReadResult.Location = new Point(506, 61);
            textBoxReadResult.Multiline = true;
            textBoxReadResult.Name = "textBoxReadResult";
            textBoxReadResult.ReadOnly = true;
            textBoxReadResult.Size = new Size(291, 536);
            textBoxReadResult.TabIndex = 8;
            // 
            // OCRUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBoxReadResult);
            Controls.Add(buttonReadImage);
            Controls.Add(buttonLoadImage);
            Controls.Add(pictureBoxImageToAnalyze);
            Name = "OCRUserControl";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageToAnalyze).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonReadImage;
        private Button buttonLoadImage;
        private PictureBox pictureBoxImageToAnalyze;
        private TextBox textBoxReadResult;
    }
}
