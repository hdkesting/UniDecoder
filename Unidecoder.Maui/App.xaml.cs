namespace Unidecoder.Maui;

public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();

        InitializeComponent();

        MainPage = new AppShell();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current!;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // services
        services.AddSingleton<Services.UnidecoderService>();

        // view models
        services.AddSingleton<ViewModels.DisectTextVm>();
        services.AddSingleton<ViewModels.ElementListVm>();
        services.AddSingleton<ViewModels.IntroductionVm>();

        return services.BuildServiceProvider();
    }

}