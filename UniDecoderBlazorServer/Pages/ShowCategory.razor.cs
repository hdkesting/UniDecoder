using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;
using UniDecoderBlazorServer.Shared;

namespace UniDecoderBlazorServer.Pages
{
    public partial class ShowCategory
    {
        private string? _categoryName;

        [CascadingParameter]
        public CascadingAppState AppState { get; set; } = null!;

        [Parameter]
        public string? CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                PerformSearch();
            }
        }

        private List<CodepointInfo>? Characters { get; set; }

        private List<string>? Categories { get; set; }

        protected override void OnInitialized()
        {
            Categories = myservice.GetAllCategories()
                .Select(di => di.Value)
                .Where(n => n != "Other Not Assigned")
                .OrderBy(n => n).ToList();
        }

        protected override void OnParametersSet()
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                CategoryName = AppState?.CategoryName ?? "Lowercase Letter";
            }
            else
            {
                PerformSearch();
            }
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
                Characters = new List<CodepointInfo>();
            }
        }
    }
}