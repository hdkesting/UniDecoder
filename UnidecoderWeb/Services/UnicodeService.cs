// <copyright file="UnicodeService.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace UnidecoderWeb.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Unicode;
    using UniDecoderWeb.Models;

    /// <summary>
    /// The Unicode Service.
    /// </summary>
    public class UnicodeService
    {
        private const int MaxCodepoint = 0x10FFFF;

        /// <summary>
        /// Shows the characters in the source string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A list of characters.</returns>
        public List<BasicInfo> ShowCharactersInString(string source)
        {
            var items = source.AsPermissiveCodePointEnumerable()
                .Select(cp => new BasicInfo(UnicodeInfo.GetCharInfo(cp))).ToList();

            return items;
        }

        /// <summary>
        /// Finds the characters by their name.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A list of characters.</returns>
        public List<BasicInfo> FindCharactersByName(string source)
        {
            List<BasicInfo> list;
            if (string.IsNullOrWhiteSpace(source))
            {
                list = new List<BasicInfo>();
                return list;
            }
            else
            {
                // select the first {limit} existing characters whose name matches
                int limit = (source.Length > 3) ? 100 : 25;
                list = Enumerable.Range(0x0000, MaxCodepoint)
                    .Where(this.CodepointExists)
                    .Select(UnicodeInfo.GetCharInfo)
                    .Where(x => NameMatches(source, x.Name))
                    .Select(info => new BasicInfo(info))
                    .Take(limit)
                    .ToList();
            }

            // try and interpret as integer (decimal)
            if (int.TryParse(source, out int code) && this.CodepointExists(code))
            {
                try
                {
                    var i = UnicodeInfo.GetCharInfo(code);
                    list.Insert(0, new BasicInfo(i));
                }
                catch
                {
                    // just ignore problematic codepoints
                }
            }

            // try and interpret as integer (hex)
            if (int.TryParse(
                        source,
                        System.Globalization.NumberStyles.HexNumber,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out code)
                    && this.CodepointExists(code))
            {
                try
                {
                    var i = UnicodeInfo.GetCharInfo(code);
                    list.Insert(0, new BasicInfo(i));
                }
                catch
                {
                    // just ignore problematic codepoints
                }
            }

            return list;
        }

        /// <summary>
        /// Gets the characters.
        /// </summary>
        /// <param name="list">The list of codepoints.</param>
        /// <returns>A list of characters.</returns>
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

        /// <summary>
        /// Gets all unicode block names.
        /// </summary>
        /// <returns>A list of block names.</returns>
        public List<string> GetUnicodeBlockNames()
        {
            var list = Enumerable.Range(0x0000, MaxCodepoint)
                                 .Where(this.CodepointExists)
                                 .Select(UnicodeInfo.GetCharInfo)
                                 .Select(info => info.Block)
                                 .Distinct()
                                 .ToList();

            return list;
        }

        /// <summary>
        /// Gets the characters by block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <returns>A list of characters.</returns>
        public List<BasicInfo> GetCharactersByBlock(string block)
        {
            var list = Enumerable.Range(0x0000, MaxCodepoint)
                                 .Where(this.CodepointExists)
                                 .Select(UnicodeInfo.GetCharInfo)
                                 .Where(x => x.Block == block)
                                 .Select(info => new BasicInfo(info))
                                 .ToList();

            return list;
        }

        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <returns>A list of characters.</returns>
        public List<BasicInfo> GetAllCharacters()
        {
            var list = Enumerable.Range(0x0000, MaxCodepoint)
                                 .Where(this.CodepointExists)
                                 .Select(UnicodeInfo.GetCharInfo)
                                 .Select(info => new BasicInfo(info))
                                 .ToList();
            return list;
        }

        /// <summary>
        /// Tries to find out whether this codepoint is a valid character.
        /// </summary>
        /// <param name="codepoint">The codepoint.</param>
        /// <returns><c>true</c> when it is a valid/known character; otherwise <c>false</c>.</returns>
        public bool CodepointExists(int codepoint)
        {
            var cat = UnicodeInfo.GetCategory(codepoint);
            return cat != System.Globalization.UnicodeCategory.OtherNotAssigned;
            //// unfortunately I don't see a more direct/less brittle way
        }

        /// <summary>
        /// Gets the unicode version supported by the library.
        /// </summary>
        /// <returns>The currently supported unicode version.</returns>
        public Version GetUnicodeVersion()
        {
            return UnicodeInfo.UnicodeVersion;
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
