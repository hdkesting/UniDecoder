namespace Unidecoder.Maui.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using Unidecoder.Maui.Components;
using Unidecoder.Maui.Models;
using Unidecoder.Maui.Services;

internal partial class ShowByNameVm : ObservableObject
{
    private static readonly TimeSpan DebounceDelay = TimeSpan.FromMilliseconds(200);
    private readonly UnidecoderService service;

    public ShowByNameVm(UnidecoderService service)
    {
        this.service = service;
        this.SampleNameChanged = new DebouncedCommand(DebounceDelay, ExecuteNameChanged);
    }

    public string? SampleName { get; set; }

    public ICommand SampleNameChanged { get; init; }

    [ObservableProperty]
    private IList<StringElement> _elements = new List<StringElement>();

    private Task ExecuteNameChanged()
    {
        var text = this.SampleName;

        if (!string.IsNullOrEmpty(text))
        {
            Elements = service.FindByName(text);
            System.Diagnostics.Debug.WriteLine(text);
        }
        else
        {
            Elements = new List<StringElement>();
        }

        return Task.CompletedTask;
    }

}
