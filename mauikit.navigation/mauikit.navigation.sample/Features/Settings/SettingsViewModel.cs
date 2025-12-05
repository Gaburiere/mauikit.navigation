using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;
using Microsoft.Extensions.Logging;

namespace mauikit.navigation.sample.Features.Settings;

public partial class SettingsViewModel(INavigationService navigationService, ILogger<SettingsViewModel> logger, IAlertService alertService) : MauiViewModel(navigationService)
{
    protected override async Task Appearing()
    {
        await alertService.DisplayAlert("Info", "Appearing called", "Ok");
    }

    protected override async Task Disappearing()
    {
        await alertService.DisplayAlert("Info", "Disappearing called", "Ok");
    }
}