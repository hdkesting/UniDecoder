namespace Unidecoder.MauiHybrid.Components.Pages;

using System.Web;

using Microsoft.AspNetCore.Components;

using Unidecoder.MauiHybrid.Models;
using Unidecoder.MauiHybrid.Components.Shared;

public partial class CharsInText
{
    ElementReference textInput;

    [CascadingParameter]
    public CascadingAppState AppState { get; set; } = null!;

    [Parameter]
    public string? TextParam { get; set; }

    public string? SearchText
    {
        get => AppState.TextSplitText;
        set => AppState.TextSplitText = value;
    }

    public List<StringElement>? Characters { get; set; }

    protected override void OnParametersSet()
    {
        if (!string.IsNullOrWhiteSpace(TextParam))
        {
            SearchText = HttpUtility.HtmlDecode(Uri.UnescapeDataString(TextParam));
        }
            
        PerformSearch();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // "autofocus" doesn't work in Blazor
        await textInput.FocusAsync();
    }

    private void PerformSearch()
    {
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            Characters = myservice.ListElements(SearchText);
        }
        else
        {
            Characters = [];
        }
    }

    private void OnInput(string? text)
    {
        SearchText = text;
        PerformSearch();
    }
}