using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniDecoder
{
    public class BasicInfo
    {
        public BasicInfo()
        {

        }

        public BasicInfo(System.Unicode.UnicodeCharInfo fullInfo)
        {
            Name = fullInfo.Name.ToTitleCase();
            Block = fullInfo.Block;
            Codepoint = fullInfo.CodePoint;
            Character = Char.ConvertFromUtf32(fullInfo.CodePoint);
        }

        public string Character { get; set; }

        public string Name { get; set; }

        public string Block { get; set; }

        public int Codepoint { get; set; }

        public string CodepointHex { get { return Codepoint.ToString("X4"); } }
    }
}
