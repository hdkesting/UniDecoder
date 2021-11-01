using System.Globalization;

using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Pages
{
    public partial class CharsByName
    {
        const string sessionkey = "charsearch";
        private string? _searchtext;

        [Parameter]
        public int? IntParam { get; set; }

        [Parameter]
        public string? Name { get; set; }

        public string? SearchText
        {
            get => _searchtext;
            set
            {
                _searchtext = value;
                PerformSearch();
            }
        }

        public List<CodepointInfo>? Characters { get; set; }

        public bool ResultsIsCapped { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (IntParam.HasValue)
            {
                SearchText = IntParam.Value.ToString(CultureInfo.InvariantCulture);
            }
            else if (!string.IsNullOrWhiteSpace(Name))
            {
                SearchText = Name;
            }
            else
            {
                var storedResult = await sessionStore.GetAsync<string>(sessionkey);
                SearchText = storedResult.Success ? storedResult.Value : string.Empty;
            }
        }

        private void PerformSearch()
        {
            // System.Diagnostics.Debug.WriteLine(SearchText);
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                // try and parse the search value as an integer or hex value.
                int? codepoint = ParseAsDecimal(SearchText) ?? ParseAsHex(SearchText);
                if (codepoint.HasValue)
                {
                    Characters = myservice.FindAroundValue(codepoint.Value);
                    ResultsIsCapped = false;
                }
                else
                {
                    // regular search
                    Characters = myservice.FindByName(SearchText, capped: false);
                    ResultsIsCapped = Characters.Count >= myservice.MaxResults;
                }

                Task.Run(async () => await sessionStore.SetAsync(sessionkey, SearchText));
            }
            else
            {
                Characters = new List<CodepointInfo>();
                ResultsIsCapped = false;
                Task.Run(async () => await sessionStore.DeleteAsync(sessionkey));
            }
        }

        private static int? ParseAsDecimal(string value)
        {
            return int.TryParse(value, out int res) ? res : default(int? );
        }

        private static int? ParseAsHex(string value)
        {
            // ignore some usual prefixes
            if (value.StartsWith("0x", System.StringComparison.OrdinalIgnoreCase) || value.StartsWith("U+", System.StringComparison.OrdinalIgnoreCase))
            {
                value = value.Substring(2);
            }

            return int.TryParse(value, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out int code) ? code : default(int? );
        }
    }
}