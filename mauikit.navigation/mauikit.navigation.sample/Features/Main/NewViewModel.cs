using CommunityToolkit.Mvvm.ComponentModel;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.sample.Features.Main;

public partial class NewViewModel(INavigationService navigationService) : MauiViewModel(navigationService)
{
    [ObservableProperty] private string _dataReceived;
    public override Task OnNavigatedTo(object? parameters)
    {
        DataReceived = parameters?.ToString() ?? string.Empty;
        return base.OnNavigatedTo(parameters);
    }
}