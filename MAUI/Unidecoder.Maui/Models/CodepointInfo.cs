// <copyright file="CodepointInfo.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Maui.Models;

using System.Unicode;

using Unidecoder.Maui.Support;

/// <summary>
/// Information about a single unicode codepoint.
/// </summary>
public class CodepointInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodepointInfo"/> class.
    /// </summary>
    /// <param name="fullInfo">The full information.</param>
    public CodepointInfo(UnicodeCharInfo fullInfo)
    {
        Name = fullInfo.Name.ToTitleCase();
        Block = fullInfo.Block;
        Codepoint = fullInfo.CodePoint;
        Character = UnicodeInfo.GetDisplayText(fullInfo.CodePoint);
        CategoryId = (int)fullInfo.Category;
        CategoryName = fullInfo.Category.ToString().ToSeparateWords();
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

    /// <summary>
    /// Gets the Unicode category identifier.
    /// </summary>
    /// <value>
    /// The category.
    /// </value>
    public int CategoryId { get; }

    /// <summary>
    /// Gets the name of the Unicode category.
    /// </summary>
    public string CategoryName { get; }
}
