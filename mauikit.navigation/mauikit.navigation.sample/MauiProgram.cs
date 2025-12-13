using CommunityToolkit.Maui;
using mauikit.navigation.core.Extensions;
using mauikit.navigation.sample.Features.Main;
using mauikit.navigation.sample.Features.Settings;
using mauikit.navigation.sample.Resources.Languages;
using Microsoft.Extensions.Logging;

namespace mauikit.navigation.sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>()
            .UseMauiKitNavigation()
            .UseLocalizationProvider(new CustomLocalizationProvider())
            // OR use standard RESX
            // .UseLocalizationProvider(AppResources.ResourceManager)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>(nameof(MainPage));
        builder.Services.AddTransientWithShellRoute<NewPage, NewViewModel>(nameof(NewPage));
        builder.Services.AddTransientWithShellRoute<NewModalPage, NewModalViewModel>(nameof(NewModalPage));
        builder.Services.AddTransientWithShellRoute<SettingsPage, SettingsViewModel>(nameof(SettingsPage));
        builder.Services.AddTransientPopup<SamplePopupPage, SamplePopupViewModel>();

        return builder.Build();
    }
}