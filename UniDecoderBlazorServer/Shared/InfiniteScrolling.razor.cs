using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using UniDecoderBlazorServer.Support;

namespace UniDecoderBlazorServer.Shared
{
    public partial class InfiniteScrolling<T>
    {
        private List<T> _items = new();
        private ElementReference _lastItemIndicator;
        private DotNetObjectReference<InfiniteScrolling<T>>? _currentComponentReference;
        private IJSObjectReference? _module;
        private IJSObjectReference? _instance;
        private bool _loading = false;
        private CancellationTokenSource? _loadItemsCts;

        [Parameter]
        public ItemsProviderRequestDelegate<T> ItemsProvider { get; set; } = null !;

        [Parameter]
        public RenderFragment<T> ItemTemplate { get; set; } = null !;

        [Parameter]
        public RenderFragment LoadingTemplate { get; set; } = null !;

        [JSInvokable]
        public async Task LoadMoreItems()
        {
            if (_loading)
            {
                return;
            }

            _loading = true;
            try
            {
                _loadItemsCts ??= new CancellationTokenSource();
                StateHasChanged(); // Allow the UI to display the loading indicator
                try
                {
                    var newItems = await ItemsProvider(new InfiniteScrollingItemsProviderRequest(_items.Count, _loadItemsCts.Token));
                    _items.AddRange(newItems);
                }
                catch (OperationCanceledException oce)when (oce.CancellationToken == _loadItemsCts.Token)
                {
                // No-op; we canceled the operation, so it's fine to suppress this exception.
                }
            }
            finally
            {
                _loading = false;
            }

            StateHasChanged(); // Display the new items and hide the loading indicator
        }

        protected override async Task OnParametersSetAsync()
        {
            // Cancel the current load items operation
            if (_loading)
            {
                _loadItemsCts?.Cancel();
                while (_loading)
                {
                    await Task.Delay(100);
                }

                _loadItemsCts?.Dispose();
                _loadItemsCts = null;
            }

            // new list of items, so reset
            _items = new();
            await LoadMoreItems();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Initialize the IntersectionObserver
            if (firstRender)
            {
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/infinite-scrolling.js");
                _currentComponentReference = DotNetObjectReference.Create(this);
                _instance = await _module.InvokeAsync<IJSObjectReference>("initialize", _lastItemIndicator, _currentComponentReference);
            }
        }

        public async ValueTask DisposeAsync()
        {
            // Cancel the current load items operation
            if (_loadItemsCts != null)
            {
                _loadItemsCts.Cancel();
                _loadItemsCts.Dispose();
                _loadItemsCts = null;
            }

            // Stop the IntersectionObserver
            if (_instance != null)
            {
                await _instance.InvokeVoidAsync("dispose");
                await _instance.DisposeAsync();
                _instance = null;
            }

            if (_module != null)
            {
                await _module.DisposeAsync();
            }

            _currentComponentReference?.Dispose();
        }
    }
}