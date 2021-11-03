using System.Web;

using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Shared;

namespace UniDecoderBlazorServer.Pages
{
    public partial class CharsInText
    {
        [CascadingParameter]
        public CascadingAppState AppState { get; set; } = null!;

        [Parameter]
        public string? TextParam { get; set; }

        public string? SearchText
        {
            get => AppState.TextSplitText;
            set
            {
                AppState.TextSplitText = value;
                PerformSearch();
            }
        }

        public List<CodepointInfo>? Characters { get; set; }

        protected override void OnParametersSet()
        {
            if (!string.IsNullOrWhiteSpace(TextParam))
            {
                SearchText = HttpUtility.HtmlDecode(Uri.UnescapeDataString(TextParam));
            }
            else
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                Characters = myservice.ListCharacters(SearchText);
            }
            else
            {
                Characters = new List<CodepointInfo>();
            }
        }
    }
}