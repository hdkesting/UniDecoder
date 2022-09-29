﻿namespace Unidecoder.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterViewModels()
                .RegisterViews();

            return builder.Build();
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddSingleton<ViewModels.IntroductionVm>();
            appBuilder.Services.AddSingleton<ViewModels.DisectTextVm>();
            // TODO

            return appBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddSingleton<Views.Introduction>();
            appBuilder.Services.AddSingleton<Views.DisectText>();
            appBuilder.Services.AddSingleton<Views.ShowByBlock>();
            appBuilder.Services.AddSingleton<Views.ShowByCategory>();
            appBuilder.Services.AddSingleton<Views.ShowByName>();

            return appBuilder;
        }
    }
}