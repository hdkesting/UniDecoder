using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Pages
{
    public partial class ShowBlock
    {
        const string sessionkey = "blockname";
        private string? _blockName;

        [Parameter]
        public string? BlockName
        {
            get => _blockName;
            set
            {
                _blockName = value;
                PerformSearch(EventArgs.Empty);
            }
        }

        private List<CodepointInfo>? Characters { get; set; }

        private List<string>? Blocks { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // ordering: *first* the ones containing "Latin", *then* the others (false < true)
            Blocks = myservice.GetAllBlocks().Select(di => di.Value).Where(n => !n.Contains("Private Use") && !n.Contains("Surrogate")).OrderBy(n => !n.Contains("Latin")).ThenBy(n => n).ToList();
            if (string.IsNullOrEmpty(BlockName))
            {
                var storedResult = await sessionStore.GetAsync<string>(sessionkey);
                BlockName = storedResult.Success ? storedResult.Value : Blocks.First();
            }
        }

        private void PerformSearch(EventArgs args)
        {
            if (!string.IsNullOrEmpty(BlockName))
            {
                var allblocks = myservice.GetAllBlocks();
                if (!allblocks.Any(b => b.Value.Equals(BlockName, StringComparison.OrdinalIgnoreCase)))
                {
                    // not a known name, just get the default one
                    BlockName = Blocks?.First() ?? "Basic Latin";
                }

                Characters = myservice.GetCharactersOfBlock(BlockName);
                Task.Run(async () => await sessionStore.SetAsync(sessionkey, BlockName));
            }
            else
            {
                Characters = new List<CodepointInfo>();
                Task.Run(async () => await sessionStore.DeleteAsync(sessionkey));
            }
        }
    }
}