using System.Globalization;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Input;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;
using mauikit.navigation.core.Localizations;

namespace mauikit.navigation.sample.Features.Main;

public partial class MainViewModel (INavigationService navigationService, IAlertService alertService, Localizator localizator)
    : MauiViewModel(navigationService)
{
    protected override async Task Appearing()
    {
        IsBusy = true;
        await Task.Delay(2000);
        IsBusy = false;
    }
    
    [RelayCommand]
    private async Task OpenPage()
    {
        var parameterToPass = await alertService.DisplayPromptAsync("Info", "Insert a parameter to pass");
        await Push<NewPage>(parameterToPass);
    }

    [RelayCommand]
    private async Task OpenModalPage()
    {
        var parameterToPass = await alertService.DisplayPromptAsync("Info", "Insert a parameter to pass");
        await Push<NewModalPage>(parameterToPass);
    }

    [RelayCommand]
    private async Task OpenPopup()
    {
        await ShowPopupAsync<SamplePopupViewModel>(options: new PopupOptions()
        {
            Shadow = null,
            CanBeDismissedByTappingOutsideOfPopup = true,
            Shape = null
        });
    }

    [RelayCommand]
    private async Task OpenPopupWithReturn()
    {
        var parameter = new Dictionary<string, object> { { "WantToReturn", true } };
        var result = await ShowPopupAsync<string, SamplePopupViewModel>(parameter, new PopupOptions()
        {
            Shadow = null,
            CanBeDismissedByTappingOutsideOfPopup = false,
            Shape = null
        });

        if (result is not null)
            await alertService.DisplayAlert("Result", result, "OK");
    }

    public override async Task OnNavigatedBack(object? parameters)
    {
        if (parameters is not null)
        {
            var datareceived = parameters.ToString();
            await alertService.DisplayAlert("Data received back", datareceived!, "OK");
        }
    }

    private int _languageCounter = 1;
    [RelayCommand]
    private Task ChangeLanguage()
    {
        var availableCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("it-IT")
    };
        var index = _languageCounter % availableCultures.Length;
        localizator.Culture = availableCultures[index];
        _languageCounter++;
        if (_languageCounter == 3)
            _languageCounter = 0;
        return Task.CompletedTask;
    }

}