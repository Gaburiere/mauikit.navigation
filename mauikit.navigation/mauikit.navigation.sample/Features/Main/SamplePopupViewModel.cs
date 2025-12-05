using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.sample.Features.Main;

public partial class SamplePopupViewModel(INavigationService navigationService)
    : MauiPopupViewModel(navigationService)
{
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("WantToReturn", out var value))
            WantToReturn = (bool)value;
    }

    protected override async Task Opened()
    {
        IsBusy = true;
        await Task.Delay(1000);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task Close()
    {
        if(WantToReturn)
            await ClosePopupAsync<string>(ReturnValue);
        else
            await ClosePopupAsync();
    }

    [ObservableProperty] private bool _wantToReturn;
    [ObservableProperty] private string? _returnValue;
}