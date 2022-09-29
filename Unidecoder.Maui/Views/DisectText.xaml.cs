namespace Unidecoder.Maui.Views;

public partial class DisectText : ContentPage
{
	public DisectText(ViewModels.DisectTextVm vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}