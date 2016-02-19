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
            tbInput.Focus();
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            var text = tbInput.Text;

            List<BasicInfo> list;
            if (rbShowChars.Checked)
            {
                list = ShowCharactersInString(text);
            }
            else
            {
                list = FindCharacters(text);
            }

            gridCharacters.DataSource = list;
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
                int limit = (source.Length > 3) ? 100 : 25;
                list = Enumerable.Range(0x0000, 0x10FFFF)
                    .Where(cp => UnicodeInfo.GetCategory(cp) != System.Globalization.UnicodeCategory.OtherNotAssigned)
                    .Select(cp => UnicodeInfo.GetCharInfo(cp))
                    .Where(x => NameMatches(source, x.Name))
                    .Select(info => new BasicInfo(info))
                    .Take(limit)
                    .ToList();
            }

            // try and interpret as integer (decimal or hex)
            int code;
            if (Int32.TryParse(source, out code) && CodepointExists(code))
            {
                try
                {
                    var i = UnicodeInfo.GetCharInfo(code);
                    list.Insert(0, new BasicInfo(i));
                }
                catch { }
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
                catch { }
            }

            return list;
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

        private void gridCharacters_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;
                lbBigChar.Text = grid[0, e.RowIndex].Value.ToString();
            }
        }

        private void gridCharacters_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            lbBigChar.Text = String.Empty;
        }

        private void gridCharacters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;
                var character = grid[e.ColumnIndex, e.RowIndex].Value.ToString();
                Clipboard.SetText(character);
            }
        }

        private void rbShowChars_CheckedChanged(object sender, EventArgs e)
        {
            tbInput_TextChanged(sender, e);
            tbInput.SelectAll();
            tbInput.Focus();
        }
    }
}
