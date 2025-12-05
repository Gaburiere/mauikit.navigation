using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.core.Classes;

internal class NavigationService(IPopupService popupService, IShellService shellService) : INavigationService
{
    public async Task GoBackAsync(object? parameters = null)
    {
        await shellService.CurrentShell.GoToAsync("..", animate: true);

        if (shellService.CurrentShell.CurrentPage.BindingContext is INavigableBack navigableBack)
        {
            await navigableBack.OnNavigatedBack(parameters);
        }
    }

    /// <inheritdoc/>
    public async Task GoToAsync<T>(object? parameters = null) where T : Page
    {
        await shellService.CurrentShell.GoToAsync(typeof(T).Name, animate: true);

        if (shellService.CurrentShell.CurrentPage.BindingContext is INavigableTo navigableTo)
        {
            await navigableTo.OnNavigatedTo(parameters);
        }
    }
    
    /// <summary>
    /// Shows a popup asynchronously
    /// </summary>
    public async Task ShowPopupAsync<TPopupViewModel>(IDictionary<string, object>? dataToPass = null, IPopupOptions? options = null, CancellationToken ct = default )
         where TPopupViewModel : MauiPopupViewModel
    {
        await popupService.ShowPopupAsync<TPopupViewModel>(shellService.CurrentShell, options: options, dataToPass, ct);
    }
    
    /// <summary>
    /// Shows a popup asynchronously and returns a result
    /// </summary>
    public async Task<TReturn?> ShowPopupAsync<TReturn, TPopupViewModel>(
        IDictionary<string, object>? dataToPass = null, IPopupOptions? options = null )
        where TReturn : class
        where TPopupViewModel : MauiPopupViewModel
    {
        var result =
            await popupService.ShowPopupAsync<TPopupViewModel, TReturn>(shellService.CurrentShell, options:options, dataToPass);
        return result.Result;
    }

    /// <summary>
    /// Closes the current popup with a result
    /// </summary>
    public Task<IPopupResult<TResult>> ClosePopupAsync<TResult>(TResult result)
    {
        return popupService.ClosePopupAsync(shellService.CurrentShell, result);
    }

    /// <summary>
    /// Closes the current popup
    /// </summary> 
    public Task ClosePopupAsync()
    {
        return popupService.ClosePopupAsync(shellService.CurrentShell);
    }
}