using System;

namespace UniDecoder
{
    public class BasicInfo
    {
        public BasicInfo(System.Unicode.UnicodeCharInfo fullInfo)
        {
            Name = fullInfo.Name.ToTitleCase();
            Block = fullInfo.Block;
            Codepoint = fullInfo.CodePoint;
            Character = Char.ConvertFromUtf32(fullInfo.CodePoint);
        }

        public string Character { get; private set; }

        public string Name { get; private set; }

        public string Block { get; private set; }

        public int Codepoint { get; private set; }

        public string CodepointHex { get { return Codepoint.ToString("X4"); } }
    }
}
