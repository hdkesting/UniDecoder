// <copyright file="Extensions.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace UniDecoderBlazorServer.Support;

using System.Text.RegularExpressions;

/// <summary>
/// Various (string) extensions.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Converts the (upper case) input to Title Case.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The converted string.</returns>
    public static string ToTitleCase(this string input)
    {
        var ca = (input ?? string.Empty).ToCharArray();
        bool start = true;
        for (int i = 0; i < ca.Length; i++)
        {
            if (char.IsWhiteSpace(ca[i]))
            {
                start = true;
            }
            else if (start)
            {
                start = false;
            }
            else if (char.IsLetter(ca[i]))
            {
                ca[i] = char.ToLowerInvariant(ca[i]);
            }
        }

        return new string(ca);
    }

    /// <summary>
    /// Converts the PascalCased string into the separate words.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>A string.</returns>
    public static string ToSeparateWords(this string input)
    {
        return string.IsNullOrEmpty(input)
                    ? string.Empty
                    : Regex.Replace(input, @"(?<=[a-z0-9])([A-Z])|(?<=[A-Z])([A-Z])(?=[a-z])", " $1$2"); // insert space before capital letter
    }
}
