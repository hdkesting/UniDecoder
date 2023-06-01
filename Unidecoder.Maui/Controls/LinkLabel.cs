using System.Windows.Input;

namespace Unidecoder.Maui.Controls;

public class LinkLabel : Label
{
    // https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/open-browser?tabs=android

    public LinkLabel()
	{
		this.TapCommand = new Command(async () => await this.PerformTapAction());
		this.TextDecorations = TextDecorations.Underline;
		this.GestureRecognizers.Add(new TapGestureRecognizer() { Command = this.TapCommand });
	}

	/// <summary>
	/// Gets or sets the external URL to open the browser for. Expected to start with "https://".
	/// </summary>
	public string? Href { get; set; }

	/// <summary>
	/// Gets or sets the internal route to redirect to.
	/// </summary>
	public string? Route { get; set; }

	private ICommand TapCommand { get; set; }

	private async Task PerformTapAction()
	{
		if (!string.IsNullOrWhiteSpace(this.Href))
		{
            try
            {
                Uri uri = new (this.Href);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
				// An unexpected error occured. No browser may be installed on the device.
				System.Diagnostics.Debug.WriteLine(ex);
            }

        }
        else if (!string.IsNullOrWhiteSpace(this.Route))
		{
			// TODO
		}
		else
		{
			System.Diagnostics.Debug.WriteLine("LinkLabel tapped, but no Href or Route was specified.");
		}
	}
}