using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Components.Shared
{
    public partial class CharData
    {
        [Parameter]
        public CodepointInfo Character { get; set; } = null !;

        [Parameter]
        public ElementPosition Position { get; set; } = ElementPosition.Single;

        [Parameter]
        public string? Element { get; set; }

        private bool SeemsLatin()
        {
            return Character.Name.Contains("Latin", StringComparison.OrdinalIgnoreCase)
                || Character.Block.Contains("Latin", StringComparison.OrdinalIgnoreCase);
        }

        public enum ElementPosition
        {
            Single,
            First,
            Middle,
            Last,
        }
    }
}