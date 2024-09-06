using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components;

namespace UniDecoderBlazorServer.Components.Shared;

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

    /* this works locally, but not when deployed to Azure ??
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // NB localStorage persists across browser restarts and tabs
        // sessionStorage is local to tab
        // use AfterRender lifecycle event, to have the SignalR link up-and-running
        // assign to the backing fields, so as not to invoke Update
        var storedResult = await storage.GetAsync<string>(nameof(NameSearchText));
        _nameSearchText = storedResult.Success ? storedResult.Value : string.Empty;
        storedResult = await storage.GetAsync<string>(nameof(TextSplitText));
        _textSplitText = storedResult.Success ? storedResult.Value : string.Empty;
        storedResult = await storage.GetAsync<string>(nameof(BlockName));
        _blockName = storedResult.Success ? storedResult.Value : string.Empty;
        storedResult = await storage.GetAsync<string>(nameof(CategoryName));
        _categoryName = storedResult.Success ? storedResult.Value : string.Empty;
    }
    */

    private async Task Update(string? value, [CallerMemberName] string member = "")
    {
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
