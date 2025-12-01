using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.sample.Features.Main;

public partial class NewModalViewModel(INavigationService navigationService) : MauiViewModel(navigationService)
{
    [ObservableProperty] private string _dataReceived;
    [ObservableProperty] private string _dataToSendBack;
    
    public override Task OnNavigatedTo(object? parameters)
    {
        DataReceived = parameters?.ToString() ?? string.Empty;
        return base.OnNavigatedTo(parameters);
    }
    
    

    [RelayCommand]
    private Task Close()
    {
        return Pop(DataToSendBack);
    }
}