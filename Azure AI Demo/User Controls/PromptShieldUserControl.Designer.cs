namespace Azure_AI_Demo
{
    partial class PromptShieldUserControl
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
            labelPromptShieldResult = new Label();
            buttonTestPrompt = new Button();
            textBoxPromptShield = new TextBox();
            buttonSetKey = new Button();
            SuspendLayout();
            // 
            // labelPromptShieldResult
            // 
            labelPromptShieldResult.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelPromptShieldResult.AutoSize = true;
            labelPromptShieldResult.Location = new Point(249, 29);
            labelPromptShieldResult.MaximumSize = new Size(100, 0);
            labelPromptShieldResult.Name = "labelPromptShieldResult";
            labelPromptShieldResult.Size = new Size(22, 15);
            labelPromptShieldResult.TabIndex = 6;
            labelPromptShieldResult.Text = "     ";
            // 
            // buttonTestPrompt
            // 
            buttonTestPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonTestPrompt.Image = Properties.Resources.Play;
            buttonTestPrompt.Location = new Point(249, 3);
            buttonTestPrompt.Name = "buttonTestPrompt";
            buttonTestPrompt.Size = new Size(24, 24);
            buttonTestPrompt.TabIndex = 5;
            buttonTestPrompt.UseVisualStyleBackColor = true;
            buttonTestPrompt.Click += buttonTestPrompt_Click;
            // 
            // textBoxPromptShield
            // 
            textBoxPromptShield.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxPromptShield.Location = new Point(3, 3);
            textBoxPromptShield.Multiline = true;
            textBoxPromptShield.Name = "textBoxPromptShield";
            textBoxPromptShield.PlaceholderText = "Enter prompt here";
            textBoxPromptShield.Size = new Size(240, 177);
            textBoxPromptShield.TabIndex = 4;
            // 
            // buttonSetKey
            // 
            buttonSetKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSetKey.Image = Properties.Resources.EditKey;
            buttonSetKey.Location = new Point(323, 3);
            buttonSetKey.Name = "buttonSetKey";
            buttonSetKey.Size = new Size(24, 24);
            buttonSetKey.TabIndex = 7;
            buttonSetKey.UseVisualStyleBackColor = true;
            buttonSetKey.Click += buttonSetKey_Click;
            // 
            // PromptShieldUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonSetKey);
            Controls.Add(labelPromptShieldResult);
            Controls.Add(buttonTestPrompt);
            Controls.Add(textBoxPromptShield);
            Name = "PromptShieldUserControl";
            Size = new Size(350, 183);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPromptShieldResult;
        private Button buttonTestPrompt;
        private TextBox textBoxPromptShield;
        private Button buttonSetKey;
    }
}
