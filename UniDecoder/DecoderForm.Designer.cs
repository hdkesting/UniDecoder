namespace UniDecoder
{
    partial class DecoderForm
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCharacters = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(39, 64);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(599, 20);
            this.tbInput.TabIndex = 0;
            this.tbInput.Text = "1× 🍕 à €1,‒";
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input text";
            // 
            // lbCharacters
            // 
            this.lbCharacters.FormattingEnabled = true;
            this.lbCharacters.Location = new System.Drawing.Point(42, 130);
            this.lbCharacters.Name = "lbCharacters";
            this.lbCharacters.Size = new System.Drawing.Size(361, 212);
            this.lbCharacters.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Characters";
            // 
            // DecoderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 424);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbCharacters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbInput);
            this.Name = "DecoderForm";
            this.Text = "Unicode decoder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbCharacters;
        private System.Windows.Forms.Label label2;
    }
}

