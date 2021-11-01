using System.Web;

using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Pages
{
    public partial class CharsInText
    {
        const string sessionkey = "textsearch";
        private string? _searchText;

        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                PerformSearch();
            }
        }

        [Parameter]
        public string? TextParam { get; set; }

        public List<CodepointInfo>? Characters { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrWhiteSpace(TextParam))
            {
                SearchText = HttpUtility.HtmlDecode(Uri.UnescapeDataString(TextParam));
            }
            else
            {
                var storedResult = await sessionStore.GetAsync<string>(sessionkey);
                SearchText = storedResult.Success ? storedResult.Value : string.Empty;
            }
        }

        private void PerformSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                Characters = myservice.ListCharacters(SearchText);
                Task.Run(async () => await sessionStore.SetAsync(sessionkey, SearchText));
            }
            else
            {
                Characters = new List<CodepointInfo>();
                Task.Run(async () => await sessionStore.DeleteAsync(sessionkey));
            }
        }
    }
}