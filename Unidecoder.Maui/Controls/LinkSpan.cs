using System.Windows.Input;

using Microsoft.Maui.Controls;

namespace Unidecoder.Maui.Controls;

/// <summary>
/// A <see cref="Span"/> with link properties.
/// </summary>
/// <remarks>Use in a Label.FormattedText.</remarks>
public class LinkSpan : Span
{
    // https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/label#create-a-hyperlink

    public LinkSpan()
	{
		this.TextDecorations = TextDecorations.Underline;
		this.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(async () => await this.PerformTapAction())});

		// TODO: but the method is not invoked (unlike LinkLabel)
	}

	/// <summary>
	/// Gets or sets the external URL to open the browser for. Expected to start with "https://".
	/// </summary>
	public string Href { get; set; }

	/// <summary>
	/// Gets or sets the internal route to redirect to.
	/// </summary>
	public string Route { get; set; }

	private async Task PerformTapAction()
	{
		if (!string.IsNullOrWhiteSpace(this.Href))
		{
			// https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/appmodel/open-browser?tabs=android
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
			System.Diagnostics.Debug.WriteLine("LinkLabel tapped, but it cannot yet handle the specified Route.");
		}
		else
		{
			System.Diagnostics.Debug.WriteLine("LinkLabel tapped, but no Href or Route was specified.");
		}
	}
}