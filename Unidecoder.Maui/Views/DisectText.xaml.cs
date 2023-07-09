using Unidecoder.Maui.ViewModels;

namespace Unidecoder.Maui.Views;

public partial class DisectText : ContentPage
{
	public DisectText()
	{
		InitializeComponent();
        this.BindingContext = App.Current.Services.GetService<ViewModels.DisectTextVm>();
	}
}