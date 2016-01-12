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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridCharacters = new System.Windows.Forms.DataGridView();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lbBigChar = new System.Windows.Forms.Label();
            this.gridFoundChars = new System.Windows.Forms.DataGridView();
            this.tbNameInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCharacters)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFoundChars)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(669, 400);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.gridCharacters);
            this.tabPage1.Controls.Add(this.tbInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(661, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Display chars";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Characters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Input text";
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
            this.gridCharacters.Location = new System.Drawing.Point(9, 109);
            this.gridCharacters.MultiSelect = false;
            this.gridCharacters.Name = "gridCharacters";
            this.gridCharacters.ReadOnly = true;
            this.gridCharacters.RowHeadersVisible = false;
            this.gridCharacters.Size = new System.Drawing.Size(646, 259);
            this.gridCharacters.TabIndex = 5;
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(6, 41);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(649, 20);
            this.tbInput.TabIndex = 1;
            this.tbInput.Text = "1× 🍕 à €1,‒";
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.lbBigChar);
            this.tabPage2.Controls.Add(this.gridFoundChars);
            this.tabPage2.Controls.Add(this.tbNameInput);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(661, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Find chars";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 355);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "↑ double click to copy character";
            // 
            // lbBigChar
            // 
            this.lbBigChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBigChar.AutoSize = true;
            this.lbBigChar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbBigChar.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBigChar.Location = new System.Drawing.Point(612, 28);
            this.lbBigChar.Name = "lbBigChar";
            this.lbBigChar.Size = new System.Drawing.Size(24, 36);
            this.lbBigChar.TabIndex = 3;
            this.lbBigChar.Text = " ";
            // 
            // gridFoundChars
            // 
            this.gridFoundChars.AllowUserToAddRows = false;
            this.gridFoundChars.AllowUserToDeleteRows = false;
            this.gridFoundChars.AllowUserToResizeRows = false;
            this.gridFoundChars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFoundChars.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridFoundChars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFoundChars.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridFoundChars.Location = new System.Drawing.Point(10, 112);
            this.gridFoundChars.MultiSelect = false;
            this.gridFoundChars.Name = "gridFoundChars";
            this.gridFoundChars.ReadOnly = true;
            this.gridFoundChars.RowHeadersVisible = false;
            this.gridFoundChars.ShowEditingIcon = false;
            this.gridFoundChars.Size = new System.Drawing.Size(645, 240);
            this.gridFoundChars.TabIndex = 2;
            this.gridFoundChars.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFoundChars_CellDoubleClick);
            this.gridFoundChars.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFoundChars_CellMouseEnter);
            this.gridFoundChars.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFoundChars_CellMouseLeave);
            // 
            // tbNameInput
            // 
            this.tbNameInput.Location = new System.Drawing.Point(7, 45);
            this.tbNameInput.Name = "tbNameInput";
            this.tbNameInput.Size = new System.Drawing.Size(360, 20);
            this.tbNameInput.TabIndex = 1;
            this.tbNameInput.TextChanged += new System.EventHandler(this.tbNameInput_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Character name";
            // 
            // DecoderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 424);
            this.Controls.Add(this.tabControl1);
            this.Name = "DecoderForm";
            this.Text = "Unicode decoder";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCharacters)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFoundChars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.DataGridView gridCharacters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gridFoundChars;
        private System.Windows.Forms.TextBox tbNameInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbBigChar;
        private System.Windows.Forms.Label label4;
    }
}

