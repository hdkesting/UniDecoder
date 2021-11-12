using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

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
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;IEnumerable&lt;CodepointInfo&gt;&gt;.</returns>
        private Task<IEnumerable<CodepointInfo>> GetItems(InfiniteScrollingItemsProviderRequest request)
        {
            return Task.FromResult((Characters ?? Enumerable.Empty<CodepointInfo>()).Skip(request.StartIndex).Take(50));
        }

        private ValueTask<ItemsProviderResult<CodepointInfo>> GetVirtualItems(ItemsProviderRequest request)
        {
            var chars = Characters ?? Enumerable.Empty<CodepointInfo>();
            var res = chars.Skip(request.StartIndex).Take(request.Count);
            var ipres = new ItemsProviderResult<CodepointInfo>(res, chars.Count());
            return ValueTask.FromResult(ipres);
        }
    }
}