using System.Windows.Input;

namespace Unidecoder.Maui.Components;

internal class DebouncedCommand : ICommand
{
    // based on https://www.codingwithcalvin.net/xamarin-forms-debouncing-an-entry-field/

    private readonly TimeSpan delay;
	private readonly Func<Task> action;

    private CancellationTokenSource _throttleCts = new ();

    public DebouncedCommand(TimeSpan delay, Func<Task> action)
	{
		this.delay = delay;
		this.action = action;
	}

	public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => action is not null;

    public void Execute(object? parameter)
	{
        _ = DebouncedSearch();
	}

    private async Task DebouncedSearch()
    {
        try
        {
            Interlocked.Exchange(ref _throttleCts, new CancellationTokenSource()).Cancel();

            //NOTE THE 500 HERE - WHICH IS THE TIME TO WAIT
            await Task.Delay(this.delay, _throttleCts.Token)

                //NOTICE THE "ACTUAL" SEARCH METHOD HERE
                .ContinueWith(async _ => await this.action(),
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
        catch
        {
            //Ignore any Threading errors
        }
    }

}
