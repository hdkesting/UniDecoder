using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Unidecoder.Maui.ViewModels;

public class IntroductionVm : ObservableObject
{
	public IntroductionVm()
	{
		this.OpenLinkCommand = new Command<string>(async s => await OpenLink(s));
        this.NavigateCommand = new Command<string>(async s => await Navigate(s));
	}

    public ICommand OpenLinkCommand { get; init; }

    public ICommand NavigateCommand { get; init; }

	private async Task OpenLink(string href)
	{
        if (!string.IsNullOrWhiteSpace(href))
        {
            // https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/open-browser?tabs=android
            try
            {
                Uri uri = new(href);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }

    private async Task Navigate(string route)
    {
        // route sample: "text|proof" - "text" for page to navigate to, "proof" is parameter (text to process)
        // TODO
        await Task.CompletedTask;
    }
}
