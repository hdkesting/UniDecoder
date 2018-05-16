// <copyright file="BasicInfo.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace UniDecoderWeb.Models
{
    using System;
    using System.Unicode;
    using Newtonsoft.Json;
    using UniDecoderWeb.Support;

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
            this.Name = fullInfo.Name.ToTitleCase();
            this.Block = fullInfo.Block;
            this.Codepoint = fullInfo.CodePoint;

            this.Character = UnicodeInfo.GetDisplayText(fullInfo.CodePoint);

            this.CategoryId = (int)fullInfo.Category;
            this.Category = fullInfo.Category.ToString().ToSeparateWords();
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
        public string CodepointHex => this.Codepoint.ToString("X4");

        /// <summary>
        /// Gets the Unicode category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [JsonProperty("category")]
        public string Category { get; }

        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(BasicInfo other)
        {
            return other?.Codepoint == this.Codepoint;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.Name;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => this.Codepoint;

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is BasicInfo bi && this.Equals(bi);
        }
    }
}
