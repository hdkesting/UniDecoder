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
    private static readonly TimeSpan AutoCancelTimespan = TimeSpan.FromMilliseconds(2000);
    private readonly UnidecoderService service;
    private CancellationTokenSource _tokenSource = new(AutoCancelTimespan);

    public ShowByNameVm(UnidecoderService service)
    {
        this.service = service;
        this.SampleNameChanged = new DebouncedCommand(DebounceDelay, ExecuteNameChanged);
    }

    public string? SampleName { get; set; }

    public ICommand SampleNameChanged { get; init; }

    [ObservableProperty]
    private IList<StringElement> _elements = new List<StringElement>();

    private async Task ExecuteNameChanged()
    {
        var text = this.SampleName;
        Interlocked.Exchange(ref _tokenSource, new CancellationTokenSource(AutoCancelTimespan)).Cancel();

        try
        {
            if (!string.IsNullOrEmpty(text))
            {
                System.Diagnostics.Debug.WriteLine($"--- before {text}");
                await Task.Yield();
                Elements = service.FindByName(text, capped: true, cancellationToken: _tokenSource.Token);
                System.Diagnostics.Debug.WriteLine($"--- after {text}");
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
