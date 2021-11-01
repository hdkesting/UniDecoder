using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Pages
{
    public partial class Index
    {
        string unicodeVersion = "?";
        int characterCount;
        CodepointInfo sample = null !;

        protected override void OnInitialized()
        {
            unicodeVersion = myservice.GetUnicodeVersion().ToString();
            characterCount = myservice.GetTotalCharacterCount();
            sample = myservice.ListCharacters("a").Single();
        }
    }
}