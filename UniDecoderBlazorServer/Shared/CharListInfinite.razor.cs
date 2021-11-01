using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Support;

namespace UniDecoderBlazorServer.Shared
{
    public partial class CharListInfinite
    {
        [Parameter]
        public List<CodepointInfo>? Characters { get; set; }

        [Parameter]
        public string? EmptyListMessage { get; set; }

        [Parameter]
        public string? CountMessageFormat { get; set; }

        /// <summary>
        /// The callback function to get the next batch of items.
        /// </summary>
        /// <param name = "request"></param>
        /// <returns></returns>
        private Task<IEnumerable<CodepointInfo>> GetItems(InfiniteScrollingItemsProviderRequest request)
        {
            return Task.FromResult((Characters ?? Enumerable.Empty<CodepointInfo>()).Skip(request.StartIndex).Take(50));
        }
    }
}