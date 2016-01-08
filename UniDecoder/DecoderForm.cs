using System;
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

            lbCharacters.Items.Clear();
            for (var i = 0; i < text.Length; i += Char.IsSurrogatePair(text, i) ? 2 : 1)
            {
                var codepoint = Char.ConvertToUtf32(text, i);
                var info = UnicodeInfo.GetName(codepoint);
                
                lbCharacters.Items.Add($"{TitleCaseString(info)} ({codepoint}/U+{codepoint:X4})");
            }
        }

        private string TitleCaseString(string input)
        {
            var ca = input.ToCharArray();
            bool start = true;
            for (int i=0; i<ca.Length; i++)
            {
                if (Char.IsWhiteSpace(ca[i]))
                {
                    start = true;
                }
                else if (start)
                {
                    start = false;
                }
                else if (Char.IsLetter(ca[i]))
                {
                    ca[i] = Char.ToLowerInvariant(ca[i]);
                }
            }

            return new string(ca);
        }
    }
}
