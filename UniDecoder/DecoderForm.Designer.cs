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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecoderForm));
            gridCharacters = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            lbBigChar = new System.Windows.Forms.Label();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPageShow = new System.Windows.Forms.TabPage();
            label3 = new System.Windows.Forms.Label();
            tbTextInput = new System.Windows.Forms.TextBox();
            tabPageFind = new System.Windows.Forms.TabPage();
            tbNameInput = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            tabPageBlock = new System.Windows.Forms.TabPage();
            cbBlocks = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            NormalizationGroup = new System.Windows.Forms.GroupBox();
            label5 = new System.Windows.Forms.Label();
            rbNone = new System.Windows.Forms.RadioButton();
            rbFormC = new System.Windows.Forms.RadioButton();
            rbFormD = new System.Windows.Forms.RadioButton();
            VersionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)gridCharacters).BeginInit();
            tabControl1.SuspendLayout();
            tabPageShow.SuspendLayout();
            tabPageFind.SuspendLayout();
            tabPageBlock.SuspendLayout();
            NormalizationGroup.SuspendLayout();
            SuspendLayout();
            // 
            // gridCharacters
            // 
            gridCharacters.AllowUserToAddRows = false;
            gridCharacters.AllowUserToDeleteRows = false;
            gridCharacters.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridCharacters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            gridCharacters.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gridCharacters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            gridCharacters.BackgroundColor = System.Drawing.SystemColors.Window;
            gridCharacters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridCharacters.Location = new System.Drawing.Point(14, 171);
            gridCharacters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gridCharacters.MultiSelect = false;
            gridCharacters.Name = "gridCharacters";
            gridCharacters.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            gridCharacters.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            gridCharacters.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridCharacters.RowsDefaultCellStyle = dataGridViewCellStyle3;
            gridCharacters.Size = new System.Drawing.Size(666, 427);
            gridCharacters.TabIndex = 5;
            gridCharacters.CellDoubleClick += GridCharacters_CellDoubleClick;
            gridCharacters.CellMouseEnter += GridCharacters_CellMouseEnter;
            gridCharacters.CellMouseLeave += GridCharacters_CellMouseLeave;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(44, 601);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(175, 15);
            label1.TabIndex = 8;
            label1.Text = "↑ double click to copy character";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(15, 152);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 15);
            label2.TabIndex = 9;
            label2.Text = "Characters";
            // 
            // lbBigChar
            // 
            lbBigChar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lbBigChar.AutoSize = true;
            lbBigChar.BackColor = System.Drawing.SystemColors.ControlLight;
            lbBigChar.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lbBigChar.Location = new System.Drawing.Point(621, 110);
            lbBigChar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbBigChar.Name = "lbBigChar";
            lbBigChar.Size = new System.Drawing.Size(32, 50);
            lbBigChar.TabIndex = 10;
            lbBigChar.Text = " ";
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPageShow);
            tabControl1.Controls.Add(tabPageFind);
            tabControl1.Controls.Add(tabPageBlock);
            tabControl1.Location = new System.Drawing.Point(14, 14);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(666, 88);
            tabControl1.TabIndex = 11;
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            // 
            // tabPageShow
            // 
            tabPageShow.Controls.Add(label3);
            tabPageShow.Controls.Add(tbTextInput);
            tabPageShow.Location = new System.Drawing.Point(4, 24);
            tabPageShow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPageShow.Name = "tabPageShow";
            tabPageShow.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPageShow.Size = new System.Drawing.Size(658, 60);
            tabPageShow.TabIndex = 0;
            tabPageShow.Text = "Show";
            tabPageShow.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(5, 8);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(153, 15);
            label3.TabIndex = 3;
            label3.Text = "Show characters in this text:";
            // 
            // tbTextInput
            // 
            tbTextInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbTextInput.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tbTextInput.Location = new System.Drawing.Point(4, 27);
            tbTextInput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbTextInput.Name = "tbTextInput";
            tbTextInput.Size = new System.Drawing.Size(646, 22);
            tbTextInput.TabIndex = 2;
            tbTextInput.Text = "1× 🍕 à €1,‒";
            tbTextInput.TextChanged += TbInput_TextChanged;
            // 
            // tabPageFind
            // 
            tabPageFind.Controls.Add(tbNameInput);
            tabPageFind.Controls.Add(label4);
            tabPageFind.Location = new System.Drawing.Point(4, 24);
            tabPageFind.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPageFind.Name = "tabPageFind";
            tabPageFind.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPageFind.Size = new System.Drawing.Size(658, 60);
            tabPageFind.TabIndex = 1;
            tabPageFind.Text = "Find";
            tabPageFind.UseVisualStyleBackColor = true;
            // 
            // tbNameInput
            // 
            tbNameInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbNameInput.Location = new System.Drawing.Point(7, 27);
            tbNameInput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbNameInput.Name = "tbNameInput";
            tbNameInput.Size = new System.Drawing.Size(642, 23);
            tbNameInput.TabIndex = 1;
            tbNameInput.TextChanged += TbNameInput_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 8);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(237, 15);
            label4.TabIndex = 0;
            label4.Text = "Find characters by name using these words:";
            // 
            // tabPageBlock
            // 
            tabPageBlock.Controls.Add(cbBlocks);
            tabPageBlock.Controls.Add(label6);
            tabPageBlock.Location = new System.Drawing.Point(4, 24);
            tabPageBlock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPageBlock.Name = "tabPageBlock";
            tabPageBlock.Size = new System.Drawing.Size(658, 60);
            tabPageBlock.TabIndex = 2;
            tabPageBlock.Text = "Block";
            tabPageBlock.UseVisualStyleBackColor = true;
            // 
            // cbBlocks
            // 
            cbBlocks.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            cbBlocks.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            cbBlocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbBlocks.FormattingEnabled = true;
            cbBlocks.Location = new System.Drawing.Point(6, 23);
            cbBlocks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbBlocks.Name = "cbBlocks";
            cbBlocks.Size = new System.Drawing.Size(326, 23);
            cbBlocks.Sorted = true;
            cbBlocks.TabIndex = 1;
            cbBlocks.SelectedValueChanged += CbBlocks_SelectedValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 5);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(223, 15);
            label6.TabIndex = 0;
            label6.Text = "Show characters from the selected block:";
            // 
            // NormalizationGroup
            // 
            NormalizationGroup.Controls.Add(label5);
            NormalizationGroup.Controls.Add(rbNone);
            NormalizationGroup.Controls.Add(rbFormC);
            NormalizationGroup.Controls.Add(rbFormD);
            NormalizationGroup.Location = new System.Drawing.Point(14, 104);
            NormalizationGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NormalizationGroup.Name = "NormalizationGroup";
            NormalizationGroup.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NormalizationGroup.Size = new System.Drawing.Size(317, 45);
            NormalizationGroup.TabIndex = 14;
            NormalizationGroup.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(7, 18);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(85, 15);
            label5.TabIndex = 17;
            label5.Text = "Normalization:";
            // 
            // rbNone
            // 
            rbNone.AutoSize = true;
            rbNone.Checked = true;
            rbNone.Location = new System.Drawing.Point(99, 16);
            rbNone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbNone.Name = "rbNone";
            rbNone.Size = new System.Drawing.Size(54, 19);
            rbNone.TabIndex = 16;
            rbNone.TabStop = true;
            rbNone.Text = "None";
            rbNone.UseVisualStyleBackColor = true;
            rbNone.CheckedChanged += RbShowChars_CheckedChanged;
            // 
            // rbFormC
            // 
            rbFormC.AutoSize = true;
            rbFormC.Location = new System.Drawing.Point(166, 16);
            rbFormC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbFormC.Name = "rbFormC";
            rbFormC.Size = new System.Drawing.Size(57, 19);
            rbFormC.TabIndex = 15;
            rbFormC.Text = "FomC";
            rbFormC.UseVisualStyleBackColor = true;
            rbFormC.CheckedChanged += RbShowChars_CheckedChanged;
            // 
            // rbFormD
            // 
            rbFormD.AutoSize = true;
            rbFormD.Location = new System.Drawing.Point(233, 16);
            rbFormD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbFormD.Name = "rbFormD";
            rbFormD.Size = new System.Drawing.Size(57, 19);
            rbFormD.TabIndex = 14;
            rbFormD.Text = "FomD";
            rbFormD.UseVisualStyleBackColor = true;
            rbFormD.CheckedChanged += RbShowChars_CheckedChanged;
            // 
            // VersionLabel
            // 
            VersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            VersionLabel.AutoSize = true;
            VersionLabel.Location = new System.Drawing.Point(617, 605);
            VersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new System.Drawing.Size(40, 15);
            VersionLabel.TabIndex = 15;
            VersionLabel.Text = "0.0.0.0";
            // 
            // DecoderForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(694, 630);
            Controls.Add(VersionLabel);
            Controls.Add(NormalizationGroup);
            Controls.Add(tabControl1);
            Controls.Add(lbBigChar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(gridCharacters);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "DecoderForm";
            Text = "Unicode decoder";
            ((System.ComponentModel.ISupportInitialize)gridCharacters).EndInit();
            tabControl1.ResumeLayout(false);
            tabPageShow.ResumeLayout(false);
            tabPageShow.PerformLayout();
            tabPageFind.ResumeLayout(false);
            tabPageFind.PerformLayout();
            tabPageBlock.ResumeLayout(false);
            tabPageBlock.PerformLayout();
            NormalizationGroup.ResumeLayout(false);
            NormalizationGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.GroupBox NormalizationGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbFormC;
        private System.Windows.Forms.RadioButton rbFormD;
        private System.Windows.Forms.TabPage tabPageBlock;
        private System.Windows.Forms.ComboBox cbBlocks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label VersionLabel;
    }
}

