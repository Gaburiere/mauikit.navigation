using CommunityToolkit.Maui;
using mauikit.navigation.core.Classes;

namespace mauikit.navigation.core.Interfaces;

/// <summary>
/// Interface for navigation within the application
/// </summary>
public interface INavigableTo
{
    /// <summary>
    /// Called when navigating to a page
    /// </summary>
    /// <param name="parameters">Navigation parameters</param>
    Task OnNavigatedTo(object? parameters);

    /// <summary>
    /// Pop the current page from navigation stack
    /// </summary>
    /// <param name="data">Optional data to pass back</param>
    Task Pop(object? data = null);

    /// <summary>
    /// Show a popup with specified ViewModel
    /// </summary>
    /// <typeparam name="TPopupViewModel">Type of popup ViewModel</typeparam>
    /// <param name="data">Optional data to pass to popup</param>
    /// <param name="options">Optional popup display options</param>
    Task ShowPopupAsync<TPopupViewModel>(IDictionary<string, object>? data = null, IPopupOptions? options = null)
        where TPopupViewModel : MauiPopupViewModel;

    /// <summary>
    /// Show a popup with specified ViewModel and return result
    /// </summary>
    /// <typeparam name="T">Type of result</typeparam>
    /// <typeparam name="TPopupViewModel">Type of popup ViewModel</typeparam>
    /// <param name="data">Optional data to pass to popup</param>
    /// <param name="options">Optional popup display options</param>
    /// <returns>Result from popup</returns>
    Task<T?> ShowPopupAsync<T, TPopupViewModel>(IDictionary<string, object>? data = null, IPopupOptions? options = null)
        where T : class where TPopupViewModel : MauiPopupViewModel;
}
