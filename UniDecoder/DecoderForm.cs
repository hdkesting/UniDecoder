using System;
using System.Collections.Generic;
using System.Linq;
using System.Unicode;
using System.Windows.Forms;

namespace UniDecoder
{
    public partial class DecoderForm : Form
    {
        public DecoderForm()
        {
            InitializeComponent();

            tbInput_TextChanged(null, EventArgs.Empty);
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            var text = tbInput.Text;

            var items = new List<BasicInfo>();
            for (var i = 0; i < text.Length; i += Char.IsSurrogatePair(text, i) ? 2 : 1)
            {
                var codepoint = Char.ConvertToUtf32(text, i);
                var info = new BasicInfo(UnicodeInfo.GetCharInfo(codepoint));

                items.Add(info);
            }

            gridCharacters.DataSource = items;
        }

        private void tbNameInput_TextChanged(object sender, EventArgs e)
        {
            var partial = tbNameInput.Text;
            List<BasicInfo> list;

            if (String.IsNullOrWhiteSpace(partial))
            {
                list = new List<BasicInfo>();
            }
            else
            {
                int limit = (partial.Length > 3) ? 100 : 25;
                list = Enumerable.Range(1, 200000)
                    .Where(cp => UnicodeInfo.GetCategory(cp) != System.Globalization.UnicodeCategory.OtherNotAssigned)
                    .Select(cp => UnicodeInfo.GetCharInfo(cp))
                    .Where(x => NameMatches(partial, x.Name))
                    .Select(info => new BasicInfo(info))
                    .Take(limit)
                    .ToList();
            }

            // try and interpret as integer
            int code;
            if (Int32.TryParse(partial, out code) && CodepointExists(code))
            {
                var i = UnicodeInfo.GetCharInfo(code);
                list.Insert(0, new BasicInfo(i));
            }

            if (Int32.TryParse(partial, 
                                System.Globalization.NumberStyles.HexNumber, 
                                System.Globalization.CultureInfo.InvariantCulture, 
                                out code)
                        && CodepointExists(code))
            {
                var i = UnicodeInfo.GetCharInfo(code);
                list.Insert(0, new BasicInfo(i));
            }

            gridFoundChars.DataSource = list;
        }

        private bool CodepointExists(int codepoint)
        {
            var cat = UnicodeInfo.GetCategory(codepoint);
            return cat != System.Globalization.UnicodeCategory.OtherNotAssigned;
        }

        private bool NameMatches(string match, string source)
        {
            if (source == null)
                return false;

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

        private void gridFoundChars_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;
                lbBigChar.Text = grid[0, e.RowIndex].Value.ToString();
            }
        }

        private void gridFoundChars_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            lbBigChar.Text = String.Empty;
        }

        private void gridFoundChars_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;
                var character = grid[e.ColumnIndex, e.RowIndex].Value.ToString();
                Clipboard.SetText(character);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tc = (TabControl)sender;

            if (tc.SelectedIndex == 0)
            {
                tbInput.SelectAll();
                tbInput.Focus();
            }
            else if (tc.SelectedIndex == 1)
            {
                tbNameInput.SelectAll();
                tbNameInput.Focus();
            }
        }
    }
}
