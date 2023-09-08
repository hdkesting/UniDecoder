namespace Unidecoder.Maui.Views;

public partial class ShowByCategory : ContentPage
{
	public ShowByCategory()
	{
		InitializeComponent();
        var vm = MauiProgram.App.Services.GetService<ViewModels.ShowByCategoryVm>();
        this.BindingContext = vm;
    }
}