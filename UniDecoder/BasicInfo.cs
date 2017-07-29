using System;

namespace UniDecoder
{
    /// <summary>
    /// Basic information about a unicode codepoint, for display in the grid.
    /// </summary>
    public class BasicInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicInfo"/> class.
        /// </summary>
        /// <param name="fullInfo">The full information.</param>
        public BasicInfo(System.Unicode.UnicodeCharInfo fullInfo)
        {
            Name = fullInfo.Name.ToTitleCase();
            Block = fullInfo.Block;
            Codepoint = fullInfo.CodePoint;
            if (fullInfo.Category != System.Globalization.UnicodeCategory.Surrogate)
            {
                Character = Char.ConvertFromUtf32(fullInfo.CodePoint);
            }
            else
            {
                Character = "�";
            }
        }

        /// <summary>
        /// Gets the character itself.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        public string Character { get; }

        /// <summary>
        /// Gets the name of the character.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the block where the character appears.
        /// </summary>
        /// <value>
        /// The block.
        /// </value>
        public string Block { get; }

        /// <summary>
        /// Gets the codepoint of the character.
        /// </summary>
        /// <value>
        /// The integer codepoint.
        /// </value>
        public int Codepoint { get; }

        /// <summary>
        /// Gets the codepoint of the character in hexadecimal.
        /// </summary>
        /// <value>
        /// The hexadecimal codepoint .
        /// </value>
        public string CodepointHex => Codepoint.ToString("X4");
    }
}
