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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // NB localStorage persists across browser restarts and tabs
        // sessionStorage is local to tab
        // do not access local storage before this lifecycle event
        // set local backing fields to prevent calling Update
        _nameSearchText = await localStorage.GetItemAsync<string>(nameof(NameSearchText));
        _textSplitText = await localStorage.GetItemAsync<string>(nameof(TextSplitText));
        _blockName = await localStorage.GetItemAsync<string>(nameof(BlockName));
        _categoryName = await localStorage.GetItemAsync<string>(nameof(CategoryName));
    }

    private async Task Update(string? value, [CallerMemberName] string member = "")
    {
        if (value is not null)
        {
            await localStorage.SetItemAsync(member, value);
        }
        else
        {
            await localStorage.RemoveItemAsync(member);
        }
    }
}
