using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Components.Shared;

namespace UniDecoderBlazorServer.Components.Pages
{
    public partial class ShowCategory
    {
        ElementReference dropdownElement;

        [CascadingParameter]
        public CascadingAppState AppState { get; set; } = null!;

        [Parameter]
        public string? CategoryName {  get; set; }

        private List<CodepointInfo>? Characters { get; set; }

        private List<string>? Categories { get; set; }

        protected override void OnInitialized()
        {
            Categories =
            [
                .. myservice.GetAllCategories()
                                .Select(di => di.Value)
                                .Where(n => n != "Other Not Assigned")
                                .OrderBy(n => n),
            ];
        }

        protected override void OnParametersSet()
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                CategoryName = AppState?.CategoryName ?? "Lowercase Letter";
            }

            PerformSearch();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // "autofocus" doesn't work in Blazor
            await dropdownElement.FocusAsync();
        }

        private void UpdateCategory(string? categoryName)
        {
            CategoryName = categoryName;
            PerformSearch();
        }

        private void PerformSearch()
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                Characters = myservice.GetCharactersOfCategory(CategoryName);
                if (AppState is not null)
                {
                    AppState.CategoryName = CategoryName;
                }
            }
            else
            {
                Characters = [];
            }
        }
    }
}