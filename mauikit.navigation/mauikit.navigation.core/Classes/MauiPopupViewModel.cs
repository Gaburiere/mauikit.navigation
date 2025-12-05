using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.core.Classes;


public partial class MauiPopupViewModel(INavigationService navigationService) : ObservableObject, IPopupNavigableTo, IQueryAttributable, IMauiPopupViewModel
{
    /// <inheritdoc cref="INavigationService.ClosePopupAsync{TResult}"/>
    public Task<CommunityToolkit.Maui.Core.IPopupResult<TResult>> ClosePopupAsync<TResult>(TResult result)
    {
        return navigationService.ClosePopupAsync(result);
    }
    
    /// <inheritdoc cref="INavigationService.ClosePopupAsync"/>
    public Task ClosePopupAsync()
    {
        return navigationService.ClosePopupAsync();
    }

    /// <inheritdoc/>
    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the view model is busy.
    /// </summary>
    [ObservableProperty] private bool _isBusy;
    
    /// <inheritdoc cref="IMauiPopupViewModel.OpenedCommand"/>"/>
    [RelayCommand(AllowConcurrentExecutions = false)]
    protected virtual Task Opened()
    {
        return Task.CompletedTask;
    }
    
    /// <inheritdoc cref="IMauiPopupViewModel.ClosedCommand"/>"/>
    [RelayCommand(AllowConcurrentExecutions = false)]
    protected virtual Task Closed()
    {
        return Task.CompletedTask;
    }
}