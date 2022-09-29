using System.Windows.Input;

using Unidecoder.Maui.Components;
using Unidecoder.Maui.Services;

namespace Unidecoder.Maui.ViewModels;

public class DisectTextVm : ViewModelBase
{
	private readonly UnidecoderService service;

    public DisectTextVm(UnidecoderService service)
	{
        //this.SampleTextChanged = new Command<string>(async (s) => await DebouncedSearch(() => ExecuteTextChanged(s)).ConfigureAwait(false));
        //new Command<string>(async s => await ExecuteTextChanged(s));
        this.SampleTextChanged = new DebouncedCommand(TimeSpan.FromMilliseconds(500), () => ExecuteTextChanged(this.SampleText));
        this.service = service;
	}

	public ICommand SampleTextChanged { get; init; }

	public string SampleText { get; set; }

	private async Task ExecuteTextChanged(string text)
	{
		var elements = service.ListElements(text);
        System.Diagnostics.Debug.WriteLine(text);
		// TODO
		await Task.CompletedTask;
	}
}
