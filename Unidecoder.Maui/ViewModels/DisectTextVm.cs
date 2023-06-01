using System.Windows.Input;

using Unidecoder.Maui.Components;
using Unidecoder.Maui.Models;
using Unidecoder.Maui.Services;

namespace Unidecoder.Maui.ViewModels;

public class DisectTextVm : ViewModelBase
{
	private readonly UnidecoderService service;
	private IList<StringElement> _elements = new List<StringElement>();

	public DisectTextVm(UnidecoderService service)
	{
		this.SampleTextChanged = new DebouncedCommand(TimeSpan.FromMilliseconds(500), () => ExecuteTextChanged(this.SampleText));
		this.service = service;
	}

	public ICommand SampleTextChanged { get; init; }

	public string? SampleText { get; set; }

	public IList<StringElement> Elements
	{
		get => _elements;
		set => Set(ref _elements, value);
	}

	private async Task ExecuteTextChanged(string? text)
	{
		if (!string.IsNullOrEmpty(text))
		{
			Elements = service.ListElements(text);
			System.Diagnostics.Debug.WriteLine(text);
			// TODO
			await Task.CompletedTask;
		}
	}
}
