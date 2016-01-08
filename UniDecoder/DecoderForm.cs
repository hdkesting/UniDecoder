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
            var list = Enumerable.Range(32, 200000)
                .Where(CodepointExists)
                .Select(cp => UnicodeInfo.GetCharInfo(cp))
                .Where(x => NameMatches(partial, x.Name))
                .Select(info => new BasicInfo(info))
                .Take(15)
                .ToList();

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
    }
}
