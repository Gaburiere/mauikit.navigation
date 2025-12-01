namespace mauikit.navigation.core.Interfaces;

/// <summary>
/// Interface for a view model that can be closed as a popup.
/// </summary>
public interface IPopupNavigableTo
{
    /// <summary>
    /// Closes the popup and returns a result of type TResult.
    /// </summary>
    /// <typeparam name="TResult">The type of the result to return.</typeparam>
    /// <param name="result">The result value to return.</param>
    /// <returns>A task that represents the asynchronous close operation and contains the popup result.</returns>
    Task<CommunityToolkit.Maui.Core.IPopupResult<TResult>> ClosePopupAsync<TResult>(TResult result);

    /// <summary>
    /// Closes the popup without returning a result.
    /// </summary>
    /// <returns>A task that represents the asynchronous close operation.</returns>
    Task ClosePopupAsync();
}