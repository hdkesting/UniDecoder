namespace Unidecoder.MauiHybrid.Components.Pages;

using Microsoft.AspNetCore.Components;

using Unidecoder.MauiHybrid.Models;
using Unidecoder.MauiHybrid.Components.Shared;

public partial class ShowBlock
{
    ElementReference dropdownElement;

    [CascadingParameter]
    public CascadingAppState AppState { get; set; } = null!;

    [Parameter]
    public string? BlockName {  get; set; }

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

    private void UpdateBlockName(string? blockName)
    {
        BlockName = blockName;
        PerformSearch();
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