using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Components.Shared;

namespace UniDecoderBlazorServer.Components.Pages
{
    public partial class ShowBlock
    {
        ElementReference dropdownElement;
        // private string? _blockName;

        [CascadingParameter]
        public CascadingAppState AppState { get; set; } = null!;

        [Parameter]
        public string? BlockName {  get; set; }
        //{
        //    get => _blockName;
        //    set
        //    {
        //        _blockName = value;
        //        PerformSearch();
        //    }
        //}

        private List<CodepointInfo>? Characters { get; set; }

        private List<string>? FilteredBlocks { get; set; }

        private List<string>? Blocks { get; set; }

        protected override void OnInitialized()
        {
            // ordering: *first* the ones containing "Latin", *then* the others (false < true)
            Blocks = myservice.GetAllBlocks().Select(b => b.Value).ToList();
            FilteredBlocks =
            [
                .. Blocks
                    .Where(n => !n.Contains("Private Use") && !n.Contains("Surrogate"))
                    .OrderBy(n => !n.Contains("Latin"))
                    .ThenBy(n => n),
            ];
        }

        protected override void OnParametersSet()
        {
            if (string.IsNullOrEmpty(BlockName))
            {
                BlockName = AppState?.BlockName ?? FilteredBlocks?.First() ?? "Basic Latin";
            }
 
            PerformSearch();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // "autofocus" doesn't work in Blazor
            await dropdownElement.FocusAsync();
        }

        private void PerformSearch()
        {
            if (!string.IsNullOrEmpty(BlockName) && Blocks is not null)
            {
                // do search in the full block list
                if (!Blocks.Any(b => b.Equals(BlockName, StringComparison.OrdinalIgnoreCase)))
                {
                    // not a known name, just get the default one
                    BlockName = FilteredBlocks?.First() ?? "Basic Latin";
                }

                Characters = myservice.GetCharactersOfBlock(BlockName);
                AppState.BlockName = BlockName;
            }
            else
            {
                Characters = [];
            }
        }
    }
}