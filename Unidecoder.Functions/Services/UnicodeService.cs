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
        /// <returns>A list of <see cref="CategoryInfo"/>.</returns>
        public Dictionary<int, string> GetAllCategories()
        {
            return Enum.GetValues(typeof(System.Globalization.UnicodeCategory))
                .Cast<System.Globalization.UnicodeCategory>()
                .ToDictionary(c => (int)c, c => c.ToString().ToSeparateWords());
        }
    }
}
