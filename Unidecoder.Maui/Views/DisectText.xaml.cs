using Unidecoder.Maui.ViewModels;

namespace Unidecoder.Maui.Views;

public partial class DisectText : ContentPage
{
	private readonly DisectTextVm vm;

	public DisectText(ViewModels.DisectTextVm vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
		this.vm = vm;
	}

	// TODO event-to-command
	private void Entry_TextChanged(object sender, TextChangedEventArgs e)
	{
		vm.SampleTextChanged.Execute(e.NewTextValue);
    }
}