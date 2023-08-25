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
                var sw = Stopwatch.StartNew();
                // this seems to be cancelled correctly and takes ~500 ms
                var elements = service.FindByName(text, capped: true, cancellationToken: _tokenSource.Token);
                sw.Stop();
                System.Diagnostics.Debug.WriteLine($"--- after {text}: {sw.ElapsedMilliseconds} ms");
                this._tokenSource.Token.ThrowIfCancellationRequested();
                sw.Restart();

                // this cannot be cancelled and takes ~7 secs
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
