using System.Globalization;

using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Shared;

namespace UniDecoderBlazorServer.Pages
{
    public partial class CharsByName
    {
        [Parameter]
        public int? IntParam { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [CascadingParameter]
        public CascadingAppState AppState { get; set; } = null!;

        public string? SearchText
        {
            get => AppState.NameSearchText;
            set
            {
                AppState.NameSearchText = value;
                PerformSearch();
            }
        }

        public List<CodepointInfo>? Characters { get; set; }

        public bool ResultsIsCapped { get; set; }

        protected override void OnParametersSet()
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
                PerformSearch();
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
            }
            else
            {
                Characters = new List<CodepointInfo>();
                ResultsIsCapped = false;
            }
        }

        private static int? ParseAsDecimal(string value)
        {
            return int.TryParse(value, out int res) ? res : default(int? );
        }

        private static int? ParseAsHex(string value)
        {
            // ignore some usual prefixes
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase) || value.StartsWith("U+", StringComparison.OrdinalIgnoreCase))
            {
                value = value[2..];
            }

            return int.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int code) ? code : default(int? );
        }
    }
}