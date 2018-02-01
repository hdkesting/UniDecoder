using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Unicode;
using UniDecoderWpf.Models;
using Windows.UI.Xaml.Documents;

namespace UniDecoderWpf.Services.UnicodeServices
{
    public class UnicodeService
    {
        public List<BasicInfo> ShowCharactersInString(string source)
        {
            var items = source.AsPermissiveCodePointEnumerable()
                .Select(cp => new BasicInfo(UnicodeInfo.GetCharInfo(cp))).ToList();

            return items;
        }

        public List<BasicInfo> FindCharactersByName(string source)
        {
            List<BasicInfo> list;
            if (String.IsNullOrWhiteSpace(source))
            {
                list = new List<BasicInfo>();
                return list;
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

            // try and interpret as integer (decimal)
            if (Int32.TryParse(source, out int code) && CodepointExists(code))
            {
                try
                {
                    var i = UnicodeInfo.GetCharInfo(code);
                    list.Insert(0, new BasicInfo(i));
                }
                catch { }
            }

            // try and interpret as integer (hex)
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

        internal IEnumerable<TextRange> SplitInRanges(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Enumerable.Empty<TextRange>();
            }

            bool isLatin(string block)
            {
                return block.Contains("Latin");
            }

            var output = new List<TextRange>();

            var list = this.ShowCharactersInString(text);
            var inLatin = isLatin(list[0].Block);

            TextRange range = new TextRange { StartIndex = 0 };

            int charpos = -1;
            for (int i = 0; i < list.Count; i++)
            {
                charpos++;
                if (isLatin(list[i].Block) != inLatin)
                {
                    if (!inLatin)
                    {
                        range.Length = charpos - range.StartIndex;
                        output.Add(range);
                    }

                    range = new TextRange { StartIndex = charpos };

                    inLatin = isLatin(list[i].Block);
                }
                if (char.IsSurrogate(text[charpos]))
                {
                    // TextRange uses character positions and surrogates take two chars to represent one codepoint
                    charpos++;
                }
            }

            if (!inLatin)
            {
                range.Length = text.Length - range.StartIndex;
                output.Add(range);
            }

            return output;
        }

        internal List<TextFragment> SplitInBlocks(string text)
        {
            var list = ShowCharactersInString(text);

            var output = new List<TextFragment>();
            string currentBlock = null;
            string currentFragment = "";
            foreach (var token in list)
            {
                if (token.Block != currentBlock)
                {
                    if (!String.IsNullOrEmpty(currentFragment))
                    {
                        output.Add(new TextFragment(currentFragment, currentBlock));
                    }

                    currentBlock = token.Block;
                    currentFragment = token.Character;
                }
                else
                {
                    currentFragment += token.Character;
                }
            }

            if (!String.IsNullOrEmpty(currentFragment))
            {
                output.Add(new TextFragment(currentFragment, currentBlock));
            }

            return output;
        }

        public List<BasicInfo> GetCharacters(List<int> list)
        {
            if (list == null || !list.Any())
            {
                return new List<BasicInfo>();
            }

            return list.Select(cp => UnicodeInfo.GetCharInfo(cp))
                    .Select(ci => new BasicInfo(ci))
                    .ToList();
        }

        public List<string> GetUnicodeBlockNames()
        {
            var list = Enumerable.Range(0x0000, 0x10FFFF)
                                 .Where(CodepointExists)
                                 .Select(UnicodeInfo.GetCharInfo)
                                 .Select(info => info.Block)
                                 .Distinct()
                                 .ToList();

            return list;
        }

        public List<BasicInfo> GetCharactersByBlock(string block)
        {
            var list = Enumerable.Range(0x0000, 0x10FFFF)
                                 .Where(CodepointExists)
                                 .Select(UnicodeInfo.GetCharInfo)
                                 .Where(x => x.Block == block)
                                 .Select(info => new BasicInfo(info))
                                 .ToList();

            return list;
        }

        public bool CodepointExists(int codepoint)
        {
            var cat = UnicodeInfo.GetCategory(codepoint);
            return cat != System.Globalization.UnicodeCategory.OtherNotAssigned;
            // unfortunately I don't see a more direct/less brittle way
        }

        private static bool NameMatches(string match, string source)
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

    }
}
