namespace Azure_AI_Demo.Forms
{
    partial class FormKeyInput
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelEndpoint = new Label();
            textBoxEndpoint = new TextBox();
            textBoxKey = new TextBox();
            labelKey = new Label();
            buttonAccept = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelEndpoint
            // 
            labelEndpoint.AutoSize = true;
            labelEndpoint.Location = new Point(12, 9);
            labelEndpoint.Name = "labelEndpoint";
            labelEndpoint.Size = new Size(55, 15);
            labelEndpoint.TabIndex = 0;
            labelEndpoint.Text = "Endpoint";
            // 
            // textBoxEndpoint
            // 
            textBoxEndpoint.Location = new Point(73, 6);
            textBoxEndpoint.Name = "textBoxEndpoint";
            textBoxEndpoint.Size = new Size(499, 23);
            textBoxEndpoint.TabIndex = 1;
            // 
            // textBoxKey
            // 
            textBoxKey.Location = new Point(73, 35);
            textBoxKey.Name = "textBoxKey";
            textBoxKey.Size = new Size(499, 23);
            textBoxKey.TabIndex = 3;
            // 
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.Location = new Point(41, 38);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(26, 15);
            labelKey.TabIndex = 2;
            labelKey.Text = "Key";
            // 
            // buttonAccept
            // 
            buttonAccept.Location = new Point(150, 76);
            buttonAccept.Name = "buttonAccept";
            buttonAccept.Size = new Size(75, 23);
            buttonAccept.TabIndex = 4;
            buttonAccept.Text = "Accept";
            buttonAccept.UseVisualStyleBackColor = true;
            buttonAccept.Click += buttonAccept_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(360, 76);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // FormKeyInput
            // 
            AcceptButton = buttonAccept;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(584, 111);
            Controls.Add(buttonCancel);
            Controls.Add(buttonAccept);
            Controls.Add(textBoxKey);
            Controls.Add(labelKey);
            Controls.Add(textBoxEndpoint);
            Controls.Add(labelEndpoint);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormKeyInput";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelEndpoint;
        private TextBox textBoxEndpoint;
        private TextBox textBoxKey;
        private Label labelKey;
        private Button buttonAccept;
        private Button buttonCancel;
    }
}