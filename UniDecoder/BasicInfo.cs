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

        public string Character { get; }

        public string Name { get; }

        public string Block { get; }

        public int Codepoint { get; }

        public string CodepointHex => Codepoint.ToString("X4");
    }
}
