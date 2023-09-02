namespace Unidecoder.Maui.Views;

public partial class ShowByBlock : ContentPage
{
	public ShowByBlock()
	{
		InitializeComponent();
        var vm = MauiProgram.App.Services.GetService<ViewModels.ShowByBlockVm>();
        this.BindingContext = vm;
    }
}