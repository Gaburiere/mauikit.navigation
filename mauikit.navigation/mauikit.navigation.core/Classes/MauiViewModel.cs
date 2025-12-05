using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.core.Classes;

public partial class MauiViewModel(INavigationService navigationService)
    : ObservableObject, INavigableBack, INavigableTo, IMauiViewModel
{
    /// <summary>
    /// Gets or sets a value indicating whether the view model is busy.
    /// </summary>
    [ObservableProperty] private bool _isBusy;

    /// <inheritdoc />
    public virtual async Task OnNavigatedBack(object? parameters)
    {
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual async Task OnNavigatedTo(object? parameters)
    {
        await Task.CompletedTask;
    }
    
    /// <summary>
    /// Pushes a new page onto the navigation stack.
    /// </summary>
    /// <typeparam name="T">The type of page to push.</typeparam>
    /// <param name="data">Optional data to pass to the page.</param>
    public async Task Push<T>(object? data = null) where T : Page
    {
        await navigationService.GoToAsync<T>(data);
    }

    /// <summary>
    /// Pops the current page from the navigation stack.
    /// </summary>
    /// <param name="data">Optional data to pass back.</param>
    public async Task Pop(object? data = null)
    {
        await navigationService.GoBackAsync(data);
    }
    
    /// <summary>
    /// Shows a popup using the specified view model.
    /// </summary>
    /// <typeparam name="TPopupViewModel">The type of popup view model.</typeparam>
    /// <param name="data">Optional data to pass to the popup.</param>
    /// <param name="options">Optional popup options.</param>
    public async Task ShowPopupAsync<TPopupViewModel>(IDictionary<string, object>? data = null, CommunityToolkit.Maui.IPopupOptions? options = null) where TPopupViewModel : MauiPopupViewModel
    {
        await navigationService.ShowPopupAsync<TPopupViewModel>(data, options);
    }

    /// <summary>
    /// Shows a popup using the specified view model and returns a result.
    /// </summary>
    /// <typeparam name="T">The type of result.</typeparam>
    /// <typeparam name="TPopupViewModel">The type of popup view model.</typeparam>
    /// <param name="data">Optional data to pass to the popup.</param>
    /// <param name="options">Optional popup options.</param>
    /// <returns>The result from the popup.</returns>
    public async Task<T?> ShowPopupAsync<T, TPopupViewModel>(IDictionary<string, object>? data = null, CommunityToolkit.Maui.IPopupOptions? options = null) where T : class where TPopupViewModel : MauiPopupViewModel
    {
        return await navigationService.ShowPopupAsync<T, TPopupViewModel>(data, options);
    }
    
    /// <summary>
    /// Called when the page is appearing.
    /// </summary>
    [RelayCommand(AllowConcurrentExecutions = false)]
    protected virtual Task Appearing()
    {
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Called when the page is disappearing.
    /// </summary>
    [RelayCommand(AllowConcurrentExecutions = false)]
    protected virtual Task Disappearing()
    {
        return Task.CompletedTask;
    }
}