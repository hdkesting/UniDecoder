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
            this.gridCharacters = new System.Windows.Forms.DataGridView();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.rbShowChars = new System.Windows.Forms.RadioButton();
            this.rbFindChars = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbBigChar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridCharacters)).BeginInit();
            this.SuspendLayout();
            // 
            // gridCharacters
            // 
            this.gridCharacters.AllowUserToAddRows = false;
            this.gridCharacters.AllowUserToDeleteRows = false;
            this.gridCharacters.AllowUserToResizeRows = false;
            this.gridCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCharacters.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridCharacters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCharacters.Location = new System.Drawing.Point(12, 135);
            this.gridCharacters.MultiSelect = false;
            this.gridCharacters.Name = "gridCharacters";
            this.gridCharacters.ReadOnly = true;
            this.gridCharacters.RowHeadersVisible = false;
            this.gridCharacters.Size = new System.Drawing.Size(571, 343);
            this.gridCharacters.TabIndex = 5;
            this.gridCharacters.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCharacters_CellDoubleClick);
            this.gridCharacters.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCharacters_CellMouseEnter);
            this.gridCharacters.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCharacters_CellMouseLeave);
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(12, 58);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(571, 20);
            this.tbInput.TabIndex = 1;
            this.tbInput.Text = "1× 🍕 à €1,‒";
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // rbShowChars
            // 
            this.rbShowChars.AutoSize = true;
            this.rbShowChars.Checked = true;
            this.rbShowChars.Location = new System.Drawing.Point(13, 35);
            this.rbShowChars.Name = "rbShowChars";
            this.rbShowChars.Size = new System.Drawing.Size(136, 17);
            this.rbShowChars.TabIndex = 6;
            this.rbShowChars.TabStop = true;
            this.rbShowChars.Text = "Show characters in text";
            this.rbShowChars.UseVisualStyleBackColor = true;
            this.rbShowChars.CheckedChanged += new System.EventHandler(this.rbShowChars_CheckedChanged);
            // 
            // rbFindChars
            // 
            this.rbFindChars.AutoSize = true;
            this.rbFindChars.Location = new System.Drawing.Point(177, 35);
            this.rbFindChars.Name = "rbFindChars";
            this.rbFindChars.Size = new System.Drawing.Size(141, 17);
            this.rbFindChars.TabIndex = 7;
            this.rbFindChars.TabStop = true;
            this.rbFindChars.Text = "Find characters by name";
            this.rbFindChars.UseVisualStyleBackColor = true;
            this.rbFindChars.CheckedChanged += new System.EventHandler(this.rbShowChars_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 481);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "↑ double click to copy character";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Characters";
            // 
            // lbBigChar
            // 
            this.lbBigChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBigChar.AutoSize = true;
            this.lbBigChar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lbBigChar.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBigChar.Location = new System.Drawing.Point(532, 88);
            this.lbBigChar.Name = "lbBigChar";
            this.lbBigChar.Size = new System.Drawing.Size(30, 44);
            this.lbBigChar.TabIndex = 10;
            this.lbBigChar.Text = " ";
            // 
            // DecoderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 506);
            this.Controls.Add(this.lbBigChar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbFindChars);
            this.Controls.Add(this.rbShowChars);
            this.Controls.Add(this.gridCharacters);
            this.Controls.Add(this.tbInput);
            this.Name = "DecoderForm";
            this.Text = "Unicode decoder";
            ((System.ComponentModel.ISupportInitialize)(this.gridCharacters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.DataGridView gridCharacters;
        private System.Windows.Forms.RadioButton rbShowChars;
        private System.Windows.Forms.RadioButton rbFindChars;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbBigChar;
    }
}

