using Unidecoder.Maui.ViewModels;

namespace Unidecoder.Maui.Views;

public partial class DisectText : ContentPage
{
	public DisectText()
	{
		InitializeComponent();
        this.BindingContext = MauiProgram.App.Services.GetService<ViewModels.DisectTextVm>();
	}
}