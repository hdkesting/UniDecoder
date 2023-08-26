namespace Unidecoder.Maui.ViewModels;

using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using Unidecoder.Maui.Components;
using Unidecoder.Maui.Models;
using Unidecoder.Maui.Services;

public partial class DisectTextVm : ObservableObject
{
    private static readonly TimeSpan DebounceDelay = TimeSpan.FromMilliseconds(200);

	private readonly UnidecoderService service;
	//private IList<StringElement> _elements = new List<StringElement>();

	public DisectTextVm(UnidecoderService service)
	{
		this.SampleTextChanged = new DebouncedCommand(DebounceDelay, ExecuteTextChanged);
        this.service = service;
    }

	public ICommand SampleTextChanged { get; init; }

    public string? SampleText { get; set; } = "this is a longer string";

	[ObservableProperty]
	private IList<StringElement> _elements = new List<StringElement>();

	[ObservableProperty]
	private string _elementCount = "?";

    private Task ExecuteTextChanged()
	{
		var text = this.SampleText;

        if (!string.IsNullOrEmpty(text))
		{
			Elements = service.ListElements(text);
			System.Diagnostics.Debug.WriteLine(text);
			ElementCount = Elements.Count.ToString();
			// TODO?
		}
		else
		{
			Elements = new List<StringElement>();
			ElementCount = "zero";
		}

		return Task.CompletedTask;
	}
}
