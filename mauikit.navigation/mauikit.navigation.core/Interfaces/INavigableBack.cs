namespace mauikit.navigation.core.Interfaces;

/// <summary>
/// Interface for handling back navigation and pushing new pages
/// </summary>
public interface INavigableBack
{
    /// <summary>
    /// Called when navigating back to this page
    /// </summary>
    /// <param name="parameters">Optional parameters passed during back navigation</param>
    Task OnNavigatedBack(object? parameters);

    /// <summary>
    /// Pushes a new page of type T onto the navigation stack
    /// </summary>
    /// <typeparam name="T">The type of Page to push</typeparam>
    /// <param name="data">Optional data to pass to the pushed page</param>
    Task Push<T>(object? data = null) where T : Page;
}