namespace Unidecoder.Maui.ViewModels;

using System;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using Unidecoder.Maui.Models;
using Unidecoder.Maui.Services;

internal partial class ShowByBlockVm: ObservableObject
{
    private readonly UnidecoderService service;

    public Dictionary<int, string> Blocks { get; }

    public List<string> BlockNames { get; }

    [ObservableProperty]
    private IEnumerable<StringElement> _elements = new List<StringElement>();

    public ICommand SelectedIndexChangedCommand { get; }

    public string SelectedBlock { get; set; } = "";

    public ShowByBlockVm(UnidecoderService service)
    {
        this.service = service;
        Blocks = service.GetAllBlocks();

        // TODO: ignore low & high surrogates (more?)
        BlockNames = Blocks.Select(kv => kv.Value).Order().ToList();

        SelectedIndexChangedCommand = new Command(OnSelectedIndexChanged);
    }

    private void OnSelectedIndexChanged()
    {
        Elements = service.GetCharactersOfBlock(SelectedBlock);
    }
}
