using System;
using System.Text.RegularExpressions;

namespace UniDecoderWpf.Support
{
    public static class Extensions
    {
        /// <summary>
        /// Converts the (upper case) input to Title Case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The converted string.</returns>
        public static string ToTitleCase(this string input)
        {
            var ca = (input ?? String.Empty).ToCharArray();
            bool start = true;
            for (int i = 0; i < ca.Length; i++)
            {
                if (Char.IsWhiteSpace(ca[i]))
                {
                    start = true;
                }
                else if (start)
                {
                    start = false;
                }
                else if (Char.IsLetter(ca[i]))
                {
                    ca[i] = Char.ToLowerInvariant(ca[i]);
                }
            }

            return new string(ca);
        }

        public static string ToSeparateWords(this string input)
        {
            return string.IsNullOrEmpty(input)
                        ? string.Empty
                        : Regex.Replace(input, @"(?<=[a-z0-9])([A-Z])|(?<=[A-Z])([A-Z])(?=[a-z])", " $1$2"); // insert space before capital letter
        }
    }
}
