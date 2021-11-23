using System.Globalization;

using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Pages
{
    public partial class Index
    {
        string unicodeVersion = "?";
        string characterCount = "";
        CodepointInfo sample = null !;

        protected override void OnInitialized()
        {
            unicodeVersion = myservice.GetUnicodeVersion().ToString();
            var count = myservice.GetTotalCharacterCount();
            var ci = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            ci.NumberFormat.NumberGroupSeparator = " "; // "thin space"
            characterCount = count.ToString("#,#", ci);
            sample = myservice.ListCharacters("a").Single();
        }
    }
}