namespace Unidecoder.Maui.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
    private IEnumerable<StringElement> _elements = new List<StringElement>();

    private async Task ExecuteNameChanged()
    {
        var text = this.SampleName;

        try
        {
            if (!string.IsNullOrEmpty(text) && text.Length >= 3)
            {
                System.Diagnostics.Debug.WriteLine($"--- before {text}");
                await Task.Yield();
                var sw = Stopwatch.StartNew();
                var elements = service.FindByName(text);
                sw.Stop();
                System.Diagnostics.Debug.WriteLine($"--- after {text}: {sw.ElapsedMilliseconds} ms.");
                sw.Restart();

                Elements = elements;
                sw.Stop();
                System.Diagnostics.Debug.WriteLine($"--- after {text} - 2: {sw.ElapsedMilliseconds} ms");
            }
            else
            {
                Elements = new List<StringElement>();
            }
        }
        catch(OperationCanceledException) 
        {
            // ignore
            System.Diagnostics.Debug.WriteLine($"--- Op Canceled: '{text}'");
        }
    }
}
