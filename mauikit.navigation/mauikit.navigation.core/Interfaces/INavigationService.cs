using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using mauikit.navigation.core.Classes;

namespace mauikit.navigation.core.Interfaces;

/// <summary>
/// Interface for navigation service that handles page navigation and popups. This interface is used by the MauiViewModel class. You should not use it directly.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigates back to the previous page
    /// </summary>
    /// <param name="parameters">Optional parameters to pass to the previous page</param>
    Task GoBackAsync(object? parameters = null);

    /// <summary>
    /// Navigates to a specific page type
    /// </summary>
    /// <typeparam name="T">The page type to navigate to</typeparam>
    /// <param name="parameters">Optional parameters to pass to the new page</param>
    Task GoToAsync<T>(object? parameters = null) where T : Page;
    
    /// <summary>
    /// Shows a popup with specified view model
    /// </summary>
    /// <typeparam name="TPopupViewModel">The popup view model type</typeparam>
    /// <param name="dataToPass">Optional data dictionary to pass to popup</param>
    /// <param name="options">Optional popup display options</param>
    /// <param name="ct">Cancellation token</param>
    Task ShowPopupAsync<TPopupViewModel>(IDictionary<string, object>? dataToPass = null, IPopupOptions? options = null, CancellationToken ct = default)
        where TPopupViewModel : MauiPopupViewModel;

    /// <summary>
    /// Shows a popup and returns a result
    /// </summary>
    /// <typeparam name="TReturn">The return type from the popup</typeparam>
    /// <typeparam name="TPopupViewModel">The popup view model type</typeparam>
    /// <param name="dataToPass">Optional data dictionary to pass to popup</param>
    /// <param name="options">Optional popup display options</param>
    /// <returns>The popup result of type TReturn</returns>
    Task<TReturn?> ShowPopupAsync<TReturn, TPopupViewModel>(
        IDictionary<string, object>? dataToPass = null, IPopupOptions? options = null)
        where TReturn : class
        where TPopupViewModel : MauiPopupViewModel;
    
    /// <summary>
    /// Closes popup and returns a result
    /// </summary>
    /// <typeparam name="TResult">The result type</typeparam>
    /// <param name="result">The result value</param>
    /// <returns>Popup result wrapper containing the result</returns>
    Task<IPopupResult<TResult>> ClosePopupAsync<TResult>(TResult result);

    /// <summary>
    /// Closes the current popup
    /// </summary>
    Task ClosePopupAsync();
}