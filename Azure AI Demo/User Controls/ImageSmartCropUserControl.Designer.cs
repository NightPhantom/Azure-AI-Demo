﻿
namespace Azure_AI_Demo.User_Controls
{
    partial class ImageSmartCropUserControl
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
            pictureBoxOriginalImage = new PictureBox();
            buttonAnalyzeImage = new Button();
            buttonLoadImage = new Button();
            pictureBoxResultImage = new PictureBox();
            comboBoxCrops = new ComboBox();
            labelImageAnalysisResult = new Label();
            buttonSetKey = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginalImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxResultImage).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxOriginalImage
            // 
            pictureBoxOriginalImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxOriginalImage.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxOriginalImage.Image = Properties.Resources.image_crop_sample;
            pictureBoxOriginalImage.Location = new Point(0, 0);
            pictureBoxOriginalImage.Name = "pictureBoxOriginalImage";
            pictureBoxOriginalImage.Size = new Size(400, 600);
            pictureBoxOriginalImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOriginalImage.TabIndex = 0;
            pictureBoxOriginalImage.TabStop = false;
            // 
            // buttonAnalyzeImage
            // 
            buttonAnalyzeImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAnalyzeImage.Image = Properties.Resources.Play;
            buttonAnalyzeImage.Location = new Point(406, 3);
            buttonAnalyzeImage.Name = "buttonAnalyzeImage";
            buttonAnalyzeImage.Size = new Size(24, 24);
            buttonAnalyzeImage.TabIndex = 4;
            buttonAnalyzeImage.UseVisualStyleBackColor = true;
            buttonAnalyzeImage.Click += buttonAnalyzeImage_Click;
            // 
            // buttonLoadImage
            // 
            buttonLoadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoadImage.Image = Properties.Resources.OpenFile;
            buttonLoadImage.Location = new Point(436, 3);
            buttonLoadImage.Name = "buttonLoadImage";
            buttonLoadImage.Size = new Size(24, 24);
            buttonLoadImage.TabIndex = 3;
            buttonLoadImage.UseVisualStyleBackColor = true;
            buttonLoadImage.Click += buttonLoadImage_Click;
            // 
            // pictureBoxResultImage
            // 
            pictureBoxResultImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBoxResultImage.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxResultImage.Location = new Point(400, 150);
            pictureBoxResultImage.Name = "pictureBoxResultImage";
            pictureBoxResultImage.Size = new Size(400, 300);
            pictureBoxResultImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxResultImage.TabIndex = 7;
            pictureBoxResultImage.TabStop = false;
            pictureBoxResultImage.Paint += pictureBoxThumbnail_Paint;
            // 
            // comboBoxCrops
            // 
            comboBoxCrops.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxCrops.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCrops.FormattingEnabled = true;
            comboBoxCrops.Location = new Point(500, 121);
            comboBoxCrops.Name = "comboBoxCrops";
            comboBoxCrops.Size = new Size(200, 23);
            comboBoxCrops.TabIndex = 8;
            comboBoxCrops.SelectedIndexChanged += comboBoxCrops_SelectedIndexChanged;
            // 
            // labelImageAnalysisResult
            // 
            labelImageAnalysisResult.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelImageAnalysisResult.AutoSize = true;
            labelImageAnalysisResult.Location = new Point(406, 58);
            labelImageAnalysisResult.MaximumSize = new Size(390, 0);
            labelImageAnalysisResult.Name = "labelImageAnalysisResult";
            labelImageAnalysisResult.Size = new Size(22, 15);
            labelImageAnalysisResult.TabIndex = 9;
            labelImageAnalysisResult.Text = "     ";
            // 
            // buttonSetKey
            // 
            buttonSetKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSetKey.Image = Properties.Resources.EditKey;
            buttonSetKey.Location = new Point(773, 3);
            buttonSetKey.Name = "buttonSetKey";
            buttonSetKey.Size = new Size(24, 24);
            buttonSetKey.TabIndex = 10;
            buttonSetKey.UseVisualStyleBackColor = true;
            buttonSetKey.Click += buttonSetKey_Click;
            // 
            // ImageSmartCropUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonSetKey);
            Controls.Add(labelImageAnalysisResult);
            Controls.Add(comboBoxCrops);
            Controls.Add(pictureBoxResultImage);
            Controls.Add(buttonAnalyzeImage);
            Controls.Add(buttonLoadImage);
            Controls.Add(pictureBoxOriginalImage);
            Name = "ImageSmartCropUserControl";
            Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginalImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxResultImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxOriginalImage;
        private Button buttonAnalyzeImage;
        private Button buttonLoadImage;
        private PictureBox pictureBoxResultImage;
        private ComboBox comboBoxCrops;
        private Label labelImageAnalysisResult;
        private Button buttonSetKey;
    }
}
