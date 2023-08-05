// <copyright file="BasicInfo.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Maui.Models;

using System.Collections.Generic;

/// <summary>
/// Gets basic information about the system.
/// </summary>
public class BasicInfo
{
    /// <summary>
    /// Gets or sets the list of Unicode blocks.
    /// </summary>
    /// <value>
    /// The blocks.
    /// </value>
    public Dictionary<int, string> Blocks { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of Unicode categories.
    /// </summary>
    /// <value>
    /// The categories.
    /// </value>
    public Dictionary<int, string> Categories { get; set; } = null!;

    /// <summary>
    /// Gets or sets the character count.
    /// </summary>
    /// <value>
    /// The character count.
    /// </value>
    public int CharCount { get; set; }

    /// <summary>
    /// Gets or sets the unicode version.
    /// </summary>
    /// <value>
    /// The unicode version.
    /// </value>
    public string UnicodeVersion { get; set; } = "";
}
