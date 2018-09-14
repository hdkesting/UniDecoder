// <copyright file="CategoryInfo.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions.Model
{
    using Unidecoder.Functions.Support;

    /// <summary>
    /// Information about unicode category.
    /// </summary>
    public class CategoryInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryInfo"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        public CategoryInfo(System.Globalization.UnicodeCategory category)
        {
            this.Id = (int)category;
            this.Name = category.ToString().ToSeparateWords();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
    }
}
