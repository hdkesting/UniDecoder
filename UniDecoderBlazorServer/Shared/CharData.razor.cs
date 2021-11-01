using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Shared
{
    public partial class CharData
    {
        [Parameter]
        public CodepointInfo Character { get; set; } = null !;

        private bool SeemsLatin()
        {
            return Character.Name.Contains("Latin", StringComparison.OrdinalIgnoreCase)
                || Character.Block.Contains("Latin", StringComparison.OrdinalIgnoreCase);
        }
    }
}