using System;
using System.Unicode;
using Newtonsoft.Json;
using UniDecoderWeb.Support;

namespace UniDecoderWeb.Models
{
    /// <summary>
    /// Basic information about a unicode codepoint, for display in the grid.
    /// </summary>
    public class BasicInfo : IEquatable<BasicInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicInfo"/> class.
        /// </summary>
        /// <param name="fullInfo">The full information.</param>
        public BasicInfo(UnicodeCharInfo fullInfo)
        {
            Name = fullInfo.Name.ToTitleCase();
            Block = fullInfo.Block;
            Codepoint = fullInfo.CodePoint;

            Character = UnicodeInfo.GetDisplayText(fullInfo.CodePoint);

            CategoryId = (int)fullInfo.Category;
            Category = fullInfo.Category.ToString().ToSeparateWords();
        }

        /// <summary>
        /// Gets the character itself.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        [JsonProperty("character")]
        public string Character { get; }

        /// <summary>
        /// Gets the name of the character.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Gets the block where the character appears.
        /// </summary>
        /// <value>
        /// The block.
        /// </value>
        [JsonProperty("blockName")]
        public string Block { get; }

        /// <summary>
        /// Gets the codepoint of the character.
        /// </summary>
        /// <value>
        /// The integer codepoint.
        /// </value>
        [JsonProperty("codepoint")]
        public int Codepoint { get; }

        /// <summary>
        /// Gets the codepoint of the character in hexadecimal.
        /// </summary>
        /// <value>
        /// The hexadecimal codepoint .
        /// </value>
        [JsonProperty("codepointHex")]
        public string CodepointHex => Codepoint.ToString("X4");

        /// <summary>
        /// Gets the Unicode category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [JsonProperty("category")]
        public string Category { get; }

        public int CategoryId { get; }

        public bool Equals(BasicInfo other)
        {
            return other?.Codepoint == this.Codepoint;
        }

        public override string ToString() => Name;

        public override int GetHashCode() => this.Codepoint;

        public override bool Equals(object obj)
        {
            var bi = obj as BasicInfo;
            return bi == null ? false : this.Equals(bi);
        }
    }
}
