using System;

namespace UniDecoder
{
    public static class Extensions
    {
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
    }
}
