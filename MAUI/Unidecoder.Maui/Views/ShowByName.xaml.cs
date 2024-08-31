namespace Unidecoder.Maui.Views;

public partial class ShowByName : ContentPage
{
	public ShowByName()
	{
		InitializeComponent();
        var vm = MauiProgram.App.Services.GetService<ViewModels.ShowByNameVm>();
        this.BindingContext = vm;
    }
}