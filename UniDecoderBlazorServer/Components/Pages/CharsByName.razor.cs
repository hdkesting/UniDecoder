using System.Globalization;

using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Components.Shared;
using Microsoft.AspNetCore.Components.Forms;

namespace UniDecoderBlazorServer.Components.Pages
{
    public partial class CharsByName
    {
        private bool loading, queued;

        private ElementReference textInput = default!;

        [Parameter]
        public int? IntParam { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [CascadingParameter]
        public CascadingAppState AppState { get; set; } = null!;

        public string? SearchText
        {
            get => AppState.NameSearchText;
            set => AppState.NameSearchText = value;
        }

        public List<CodepointInfo>? Characters { get; set; }

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

            await PerformSearch(SearchText);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // "autofocus" doesn't work in Blazor
            await textInput.FocusAsync();
        }

        private async Task OnValueChange(string? value)
        {
            SearchText = value;
            await PerformSearch(SearchText);
        }

        private async Task PerformSearch(string? text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (loading)
                {
                    queued = true;
                    return;
                }

                do
                {
                    await Task.Yield();

                    loading = true;
                    queued = false;
                    // System.Diagnostics.Debug.WriteLine(SearchText);
                    // try and parse the search value as an integer or hex value.
                    // text = SearchText ?? string.Empty;
                    int? codepoint = ParseAsDecimal(text) ?? ParseAsHex(text);
                    if (codepoint.HasValue)
                    {
                        Characters = myservice.FindAroundValue(codepoint.Value);
                    }
                    else
                    {
                        // regular search
                        Characters = myservice.FindByName(text, capped: false);
                    }

                    loading = false;
                }
                while (queued);
            }
            else
            {
                Characters = [];
            }

            await InvokeAsync(StateHasChanged);
        }

        private static int? ParseAsDecimal(string value)
        {
            return int.TryParse(value, out int res) ? res : default(int?);
        }

        private static int? ParseAsHex(string value)
        {
            // ignore some usual prefixes
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase) || value.StartsWith("U+", StringComparison.OrdinalIgnoreCase))
            {
                value = value[2..];
            }

            return int.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int code) ? code : default(int?);
        }
    }
}