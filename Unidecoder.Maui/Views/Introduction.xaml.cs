namespace Unidecoder.Maui.Views;

public partial class Introduction : ContentPage
{
	public Introduction()
	{
		InitializeComponent();
        this.BindingContext = MauiProgram.App.Services.GetService<ViewModels.IntroductionVm>();
    }
}