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

    public string? SampleText { get; set; }

	[ObservableProperty]
	private IList<StringElement> _elements = new List<StringElement>();

    private Task ExecuteTextChanged()
	{
		var text = this.SampleText;

        if (string.IsNullOrEmpty(text))
        {
            Elements = new List<StringElement>();
        }
        else
        {
            Elements = service.ListElements(text);
            System.Diagnostics.Debug.WriteLine(text);
        }

        return Task.CompletedTask;
	}
}
