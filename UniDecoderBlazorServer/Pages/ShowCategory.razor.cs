using Microsoft.AspNetCore.Components;

using UniDecoderBlazorServer.Models;

namespace UniDecoderBlazorServer.Pages
{
    public partial class ShowCategory
    {
        const string sessionkey = "catname";
        private string? _categoryName;

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

        protected override async Task OnInitializedAsync()
        {
            Categories = myservice.GetAllCategories()
                .Select(di => di.Value)
                .Where(n => n != "Other Not Assigned")
                .OrderBy(n => n).ToList();
            if (string.IsNullOrEmpty(CategoryName))
            {
                var storedResult = await sessionStore.GetAsync<string>(sessionkey);
                CategoryName = storedResult.Success ? storedResult.Value : "Lowercase Letter";
            }
        }

        private void PerformSearch()
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                Characters = myservice.GetCharactersOfCategory(CategoryName);
                Task.Run(async () => await sessionStore.SetAsync(sessionkey, CategoryName));
            }
            else
            {
                Characters = new List<CodepointInfo>();
                Task.Run(async () => await sessionStore.DeleteAsync(sessionkey));
            }
        }
    }
}