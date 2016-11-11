﻿namespace UniDecoder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecoderForm));
            this.gridCharacters = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbBigChar = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageShow = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTextInput = new System.Windows.Forms.TextBox();
            this.tabPageFind = new System.Windows.Forms.TabPage();
            this.tbNameInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbFormC = new System.Windows.Forms.RadioButton();
            this.rbFormD = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridCharacters)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageShow.SuspendLayout();
            this.tabPageFind.SuspendLayout();
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
            this.gridCharacters.Location = new System.Drawing.Point(12, 148);
            this.gridCharacters.MultiSelect = false;
            this.gridCharacters.Name = "gridCharacters";
            this.gridCharacters.ReadOnly = true;
            this.gridCharacters.RowHeadersVisible = false;
            this.gridCharacters.Size = new System.Drawing.Size(571, 370);
            this.gridCharacters.TabIndex = 5;
            this.gridCharacters.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCharacters_CellDoubleClick);
            this.gridCharacters.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCharacters_CellMouseEnter);
            this.gridCharacters.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCharacters_CellMouseLeave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 521);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "↑ double click to copy character";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 132);
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
            this.lbBigChar.Location = new System.Drawing.Point(532, 101);
            this.lbBigChar.Name = "lbBigChar";
            this.lbBigChar.Size = new System.Drawing.Size(30, 44);
            this.lbBigChar.TabIndex = 10;
            this.lbBigChar.Text = " ";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageShow);
            this.tabControl1.Controls.Add(this.tabPageFind);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(571, 76);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageShow
            // 
            this.tabPageShow.Controls.Add(this.label3);
            this.tabPageShow.Controls.Add(this.tbTextInput);
            this.tabPageShow.Location = new System.Drawing.Point(4, 22);
            this.tabPageShow.Name = "tabPageShow";
            this.tabPageShow.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageShow.Size = new System.Drawing.Size(563, 50);
            this.tabPageShow.TabIndex = 0;
            this.tabPageShow.Text = "Show";
            this.tabPageShow.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Show characters in this text:";
            // 
            // tbTextInput
            // 
            this.tbTextInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTextInput.Location = new System.Drawing.Point(3, 23);
            this.tbTextInput.Name = "tbTextInput";
            this.tbTextInput.Size = new System.Drawing.Size(554, 20);
            this.tbTextInput.TabIndex = 2;
            this.tbTextInput.Text = "1× 🍕 à €1,‒";
            this.tbTextInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // tabPageFind
            // 
            this.tabPageFind.Controls.Add(this.tbNameInput);
            this.tabPageFind.Controls.Add(this.label4);
            this.tabPageFind.Location = new System.Drawing.Point(4, 22);
            this.tabPageFind.Name = "tabPageFind";
            this.tabPageFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFind.Size = new System.Drawing.Size(563, 50);
            this.tabPageFind.TabIndex = 1;
            this.tabPageFind.Text = "Find";
            this.tabPageFind.UseVisualStyleBackColor = true;
            // 
            // tbNameInput
            // 
            this.tbNameInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNameInput.Location = new System.Drawing.Point(6, 23);
            this.tbNameInput.Name = "tbNameInput";
            this.tbNameInput.Size = new System.Drawing.Size(551, 20);
            this.tbNameInput.TabIndex = 1;
            this.tbNameInput.TextChanged += new System.EventHandler(this.tbNameInput_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Find characters by name using these words:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Normalization:";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(96, 95);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 13;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNormalization_CheckedChanged);
            // 
            // rbFormC
            // 
            this.rbFormC.AutoSize = true;
            this.rbFormC.Location = new System.Drawing.Point(153, 94);
            this.rbFormC.Name = "rbFormC";
            this.rbFormC.Size = new System.Drawing.Size(52, 17);
            this.rbFormC.TabIndex = 13;
            this.rbFormC.Text = "FomC";
            this.rbFormC.UseVisualStyleBackColor = true;
            this.rbFormC.CheckedChanged += new System.EventHandler(this.rbNormalization_CheckedChanged);
            // 
            // rbFormD
            // 
            this.rbFormD.AutoSize = true;
            this.rbFormD.Location = new System.Drawing.Point(211, 95);
            this.rbFormD.Name = "rbFormD";
            this.rbFormD.Size = new System.Drawing.Size(53, 17);
            this.rbFormD.TabIndex = 13;
            this.rbFormD.Text = "FomD";
            this.rbFormD.UseVisualStyleBackColor = true;
            this.rbFormD.CheckedChanged += new System.EventHandler(this.rbNormalization_CheckedChanged);
            // 
            // DecoderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 546);
            this.Controls.Add(this.rbFormD);
            this.Controls.Add(this.rbFormC);
            this.Controls.Add(this.rbNone);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbBigChar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridCharacters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DecoderForm";
            this.Text = "Unicode decoder";
            ((System.ComponentModel.ISupportInitialize)(this.gridCharacters)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageShow.ResumeLayout(false);
            this.tabPageShow.PerformLayout();
            this.tabPageFind.ResumeLayout(false);
            this.tabPageFind.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView gridCharacters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbBigChar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageShow;
        private System.Windows.Forms.TextBox tbTextInput;
        private System.Windows.Forms.TabPage tabPageFind;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNameInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbFormC;
        private System.Windows.Forms.RadioButton rbFormD;
    }
}

