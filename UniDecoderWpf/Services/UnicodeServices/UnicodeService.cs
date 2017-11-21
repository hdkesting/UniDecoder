using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Unicode;
using UniDecoderWpf.Models;

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

        public List<BasicInfo> FindCharacters(string source)
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
