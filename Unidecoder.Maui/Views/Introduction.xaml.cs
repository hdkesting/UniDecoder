namespace Unidecoder.Maui.Views;

public partial class Introduction : ContentPage
{
	public Introduction()
	{
		InitializeComponent();
        this.BindingContext = App.Current.Services.GetService<ViewModels.IntroductionVm>();
    }
}