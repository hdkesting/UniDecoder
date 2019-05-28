using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Unicode;
using System.Windows.Forms;

namespace UniDecoder
{
    public partial class DecoderForm : Form
    {
        public DecoderForm()
        {
            InitializeComponent();

            this.FillBlockList();

            this.VersionLabel.Text = UnicodeInfo.UnicodeVersion.ToString();

            TbInput_TextChanged(null, EventArgs.Empty);
            this.tbTextInput.Focus();
        }

        private void FillBlockList()
        {
            var list = Enumerable.Range(0x0000, 0x10FFFF)
                   .Where(CodepointExists)
                   .Select(UnicodeInfo.GetCharInfo)
                   .Select(info => info.Block)
                   .Distinct()
                   .ToArray();

            this.cbBlocks.Items.Clear();
            this.cbBlocks.Items.AddRange(list);
        }

        private void TbInput_TextChanged(object sender, EventArgs e)
        {
            var text = this.tbTextInput.Text;
            if (this.rbFormC.Checked)
            {
                text = text.Normalize(NormalizationForm.FormC);
            }
            else if (this.rbFormD.Checked)
            {
                text = text.Normalize(NormalizationForm.FormD);
            }

            var list = ShowCharactersInString(text);
            this.gridCharacters.DataSource = list;
        }

        private void TbNameInput_TextChanged(object sender, EventArgs e)
        {
            var text = this.tbNameInput.Text;
            var list = FindCharacters(text);
            this.gridCharacters.DataSource = list;
        }

        private List<BasicInfo> ShowCharactersInString(string source)
        {
            var items = source.AsPermissiveCodePointEnumerable().Select(cp => new BasicInfo(UnicodeInfo.GetCharInfo(cp))).ToList();

            return items;
        }

        private List<BasicInfo> FindCharacters(string source)
        {
            List<BasicInfo> list;
            if (String.IsNullOrWhiteSpace(source))
            {
                list = new List<BasicInfo>();
            }
            else
            {
                // select the first {limit} existing characters whose name matches
                int limit = (source.Length > 3) ? 100 : 25;
                list = Enumerable.Range(0x0000, 0x10FFFF)
                    .Where(CodepointExists)
                    .Select(UnicodeInfo.GetCharInfo)
                    .Where(x => NameMatches(source, x.Name))
                    .Select(info => new BasicInfo(info))
                    .Take(limit)
                    .ToList();
            }

            // try and interpret as integer (decimal or hex)
            if (Int32.TryParse(source, out int code) && CodepointExists(code))
            {
                try
                {
                    var i = UnicodeInfo.GetCharInfo(code);
                    list.Insert(0, new BasicInfo(i));
                }
                catch
                {
                    // just ignore problematic codepoints
                }
            }

            if (Int32.TryParse(source,
                                System.Globalization.NumberStyles.HexNumber,
                                System.Globalization.CultureInfo.InvariantCulture,
                                out code)
                        && CodepointExists(code))
            {
                try
                {
                    var i = UnicodeInfo.GetCharInfo(code);
                    list.Insert(0, new BasicInfo(i));
                }
                catch
                {
                    // just ignore problematic codepoints
                }
            }

            return list;
        }

        private bool CodepointExists(int codepoint)
        {
            var cat = UnicodeInfo.GetCategory(codepoint);
            return cat != System.Globalization.UnicodeCategory.OtherNotAssigned;
            // unfortunately I don't see a more direct/less brittle way
        }

        private bool NameMatches(string match, string source)
        {
            if (source == null)
            {
                return false;
            }

            var searchwords = match.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var sourcewords = source.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in searchwords)
            {
                if (sourcewords.All(w => !w.StartsWith(word, StringComparison.OrdinalIgnoreCase)))
                {
                    return false; // no match for this search word
                }
            }

            return true;
        }

        private void GridCharacters_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;
                this.lbBigChar.Text = grid[0, e.RowIndex].Value.ToString();
            }
        }

        private void GridCharacters_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.lbBigChar.Text = String.Empty;
        }

        private void GridCharacters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;
                var character = grid[e.ColumnIndex, e.RowIndex].Value.ToString();
                Clipboard.SetText(character);
            }
        }

        private void RbShowChars_CheckedChanged(object sender, EventArgs e)
        {
            TbInput_TextChanged(sender, e);
            this.tbTextInput.SelectAll();
            this.tbTextInput.Focus();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabs = (TabControl)sender;
            switch (tabs.SelectedIndex)
            {
                case 0:
                    this.NormalizationGroup.Visible = true;
                    TbInput_TextChanged(sender, e);
                    this.tbTextInput.Focus();
                    this.tbTextInput.SelectAll();
                    break;

                case 1:
                    this.NormalizationGroup.Visible = false;
                    TbNameInput_TextChanged(sender, e);
                    this.tbNameInput.Focus();
                    this.tbNameInput.SelectAll();
                    break;

                case 2:
                    this.NormalizationGroup.Visible = false;
                    if (this.cbBlocks.SelectedItem == null)
                    {
                        this.cbBlocks.SelectedItem = "Basic Latin";
                    }
                    else
                    {
                        this.CbBlocks_SelectedValueChanged(null, null);
                    }
                    break;
            }
        }

        private void RbNormalization_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                TbInput_TextChanged(sender, e);
            }
        }

        private void CbBlocks_SelectedValueChanged(object sender, EventArgs e)
        {
            var block = (string)this.cbBlocks.SelectedItem;

            var list = Enumerable.Range(0x0000, 0x10FFFF)
                        .Where(CodepointExists)
                        .Select(UnicodeInfo.GetCharInfo)
                        .Where(x => x.Block == block)
                        .Select(info => new BasicInfo(info))
                        .ToList();

            this.gridCharacters.DataSource = list;
        }
    }
}
