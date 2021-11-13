using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Shared;

namespace UniDecoderBlazorServer.Pages
{
    public partial class ShowBlock
    {
        private string? _blockName;

        [CascadingParameter]
        public CascadingAppState AppState { get; set; } = null!;

        [Parameter]
        public string? BlockName
        {
            get => _blockName;
            set
            {
                _blockName = value;
                PerformSearch();
            }
        }

        private List<CodepointInfo>? Characters { get; set; }

        private List<string>? FilteredBlocks { get; set; }

        private List<string>? Blocks { get; set; }

        protected override void OnInitialized()
        {
            // ordering: *first* the ones containing "Latin", *then* the others (false < true)
            Blocks = myservice.GetAllBlocks().Select(b => b.Value).ToList();
            FilteredBlocks = Blocks
                .Where(n => !n.Contains("Private Use") && !n.Contains("Surrogate"))
                .OrderBy(n => !n.Contains("Latin"))
                .ThenBy(n => n).ToList();
        }

        protected override void OnParametersSet()
        {
            if (string.IsNullOrEmpty(BlockName))
            {
                BlockName = AppState?.BlockName ?? FilteredBlocks?.First() ?? "Basic Latin";
            }
            else
            {
                PerformSearch();
            }
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
                Characters = new List<CodepointInfo>();
            }
        }
    }
}