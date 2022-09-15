using Unidecoder.Maui.ViewModels;

namespace Unidecoder.Maui.Views;

public partial class Introduction : ContentPage
{
	public Introduction(ViewModels.IntroductionVm vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}