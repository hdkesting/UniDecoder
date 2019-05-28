// <copyright file="UnicodeService.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Unicode;
    using Unidecoder.Functions.Model;
    using Unidecoder.Functions.Support;

    /// <summary>
    /// Service for unicode character related information.
    /// </summary>
    public class UnicodeService
    {
        private const int MaxResultCount = 80;
        private const int LowestPossibleCodepoint = 0x0000;
        private const int HighestPossibleCodepoint = 0x10FFFF;

        /// <summary>
        /// Lists the characters in the supplied <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
        public List<CodepointInfo> ListCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new List<CodepointInfo>();
            }

            var result = input.AsPermissiveCodePointEnumerable().Select(cp => new CodepointInfo(UnicodeInfo.GetCharInfo(cp))).ToList();
            return result;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A dictionary combining the id and the name.</returns>
        public Dictionary<int, string> GetAllCategories()
        {
            return Enum.GetValues(typeof(System.Globalization.UnicodeCategory))
                .Cast<System.Globalization.UnicodeCategory>()
                .Where(uc => uc != System.Globalization.UnicodeCategory.PrivateUse)
                .Where(uc => uc != System.Globalization.UnicodeCategory.Surrogate)
                .ToDictionary(c => (int)c, c => c.ToString().ToSeparateWords());
        }

        /// <summary>
        /// Gets all blocks.
        /// </summary>
        /// <returns>A dictionary combining the internal id and the name.</returns>
        public Dictionary<int, string> GetAllBlocks()
        {
            return UnicodeInfo.GetBlocks()
                .Select((b, i) => new { Block = b, Index = i })
                .ToDictionary(b => b.Index, b => b.Block.Name);
        }

        /// <summary>
        /// Gets the count of all known characters.
        /// </summary>
        /// <returns>The count.</returns>
        public int GetTotalCharacterCount()
        {
            return Enumerable.Range(LowestPossibleCodepoint, HighestPossibleCodepoint - LowestPossibleCodepoint)
                .Count(this.CodepointExists);
        }

        /// <summary>
        /// Gets the supported unicode version.
        /// </summary>
        /// <returns>The version.</returns>
        public Version GetUnicodeVersion()
        {
            return UnicodeInfo.UnicodeVersion;
        }

        /// <summary>
        /// Finds the characters by their name.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
        public List<CodepointInfo> FindByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<CodepointInfo>();
            }

            searchText = searchText.Trim();

            var list = Enumerable.Range(LowestPossibleCodepoint, HighestPossibleCodepoint - LowestPossibleCodepoint)
                .Where(this.CodepointExists)
                .Select(UnicodeInfo.GetCharInfo)
                .Where(x => this.NameMatches(searchText, x.Name))
                .Select(info => new CodepointInfo(info))
                .Take(MaxResultCount)
                .ToList();

            return list;
        }

        /// <summary>
        /// Finds characters the around the supplied codepoint value.
        /// </summary>
        /// <param name="codepoint">The codepoint.</param>
        /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
        public List<CodepointInfo> FindAroundValue(int codepoint)
        {
            const int halfRange = 8;

            var list = Enumerable.Range(codepoint - halfRange, halfRange * 2)
                .Where(n => n >= LowestPossibleCodepoint)
                .Where(n => n <= HighestPossibleCodepoint)
                .Where(this.CodepointExists)
                .Select(UnicodeInfo.GetCharInfo)
                .Select(it => new CodepointInfo(it))
                .ToList();

            return list;
        }

        /// <summary>
        /// Gets all the characters of a named block.
        /// </summary>
        /// <param name="blockName">Name of the block.</param>
        /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
        public List<CodepointInfo> GetCharactersOfBlock(string blockName)
        {
            var block = UnicodeInfo.GetBlocks().FirstOrDefault(b => b.Name.Equals(blockName, StringComparison.OrdinalIgnoreCase));

            if (block.Name is null)
            {
                return new List<CodepointInfo>();
            }

            var list = block.CodePointRange
                .Select(UnicodeInfo.GetCharInfo)
                .Select(it => new CodepointInfo(it))
                .ToList();

            return list;
        }

        /// <summary>
        /// Gets all the characters in the specified category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns>A list of <see cref="CodepointInfo"/>.</returns>
        public List<CodepointInfo> GetCharactersOfCategory(string categoryName)
        {
            categoryName = categoryName.Replace(" ", string.Empty); // remove all spaces

            if (Enum.TryParse(categoryName, true, out System.Globalization.UnicodeCategory cat))
            {
                var list = Enumerable.Range(LowestPossibleCodepoint, HighestPossibleCodepoint - LowestPossibleCodepoint)
                    .Where(this.CodepointExists)
                    .Where(cp => UnicodeInfo.GetCategory(cp) == cat)
                    .Select(UnicodeInfo.GetCharInfo)
                    .Select(it => new CodepointInfo(it))
                    .Take(200)
                    .ToList();

                return list;
            }

            return new List<CodepointInfo>();
        }

        private bool CodepointExists(int codepoint)
        {
            var cat = UnicodeInfo.GetCategory(codepoint);
            return cat != System.Globalization.UnicodeCategory.OtherNotAssigned;
            //// unfortunately I don't see a more direct/less brittle way
        }

        private bool NameMatches(string match, string source)
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
