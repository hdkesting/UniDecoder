namespace UniDecoderWpf.Models
{
    public class TextFragment
    {
        public string Fragment { get; }

        public string UnicodeBlock { get; }

        public TextFragment(string fragment, string unicodeBlock)
        {
            Fragment = fragment;
            UnicodeBlock = unicodeBlock;
        }
    }
}
