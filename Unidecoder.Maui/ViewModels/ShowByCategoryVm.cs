namespace Unidecoder.Maui.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using Unidecoder.Maui.Models;
using Unidecoder.Maui.Services;

internal partial class ShowByCategoryVm : ObservableObject
{
    private readonly UnidecoderService service;

    [ObservableProperty]
    private IEnumerable<StringElement> _elements = new List<StringElement>();

    public ICommand SelectedIndexChangedCommand { get; }

    public Dictionary<int, string> Categories { get; }

    public List<string> CategoryNames { get; }

    public string SelectedCategory { get; set; } = "";

    public ShowByCategoryVm(UnidecoderService service)
    {
        this.service = service;
        this.Categories = service.GetAllCategories();
        this.CategoryNames = this.Categories.Select(kv => kv.Value).ToList();
        SelectedIndexChangedCommand = new Command(OnSelectedIndexChanged);
    }

    private void OnSelectedIndexChanged()
    {
        Elements = service.GetCharactersOfCategory(SelectedCategory);
    }
}
