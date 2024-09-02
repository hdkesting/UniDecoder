namespace Unidecoder.MauiHybrid.Components.Shared;

using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components;

public partial class CascadingAppState
{
    private string? _nameSearchText;
    private string? _textSplitText;
    private string? _blockName;
    private string? _categoryName;

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    public string? NameSearchText
    {
        get => _nameSearchText;
        set
        {
            _nameSearchText = value;
            Task.Run(async () => await Update(value));
        }
    }

    public string? TextSplitText
    {
        get => _textSplitText;
        set
        {
            _textSplitText = value;
            Task.Run(async () => await Update(value));
        }
    }

    public string? BlockName
    {
        get => _blockName;
        set
        {
            _blockName = value;
            Task.Run(async () => await Update(value));
        }
    }

    public string? CategoryName
    {
        get => _categoryName;
        set
        {
            _categoryName = value;
            Task.Run(async () => await Update(value));
        }
    }

    /*
    protected override async Task OnInitializedAsync()
    {
        // NB localStorage persists across browser restarts and tabs
        // sessionStorage is local to tab
        var storedResult = await storage.GetAsync<string>(nameof(NameSearchText));
        NameSearchText = storedResult.Success ? storedResult.Value : string.Empty;
        storedResult = await storage.GetAsync<string>(nameof(TextSplitText));
        TextSplitText = storedResult.Success ? storedResult.Value : string.Empty;
        storedResult = await storage.GetAsync<string>(nameof(BlockName));
        BlockName = storedResult.Success ? storedResult.Value : string.Empty;
        storedResult = await storage.GetAsync<string>(nameof(CategoryName));
        CategoryName = storedResult.Success ? storedResult.Value : string.Empty;
    }
    */
    private async Task Update(string? value, [CallerMemberName] string member = "")
    {
        // TODO
        // possibly: https://dev.to/icebeam7/storing-local-data-in-a-net-maui-blazor-hybrid-app-using-indexeddb-part-1-3hn2
        // or https://dev.to/nick_alonge/using-local-browser-storage-in-net-maui-blazor-hybrid-3loe
        // or simply use https://github.com/Blazored/LocalStorage
        /*
        if (value != null)
        {
            await storage.SetAsync(member, value);
        }
        else
        {
            await storage.DeleteAsync(member);
        }
        */
        await Task.CompletedTask;
    }
}
