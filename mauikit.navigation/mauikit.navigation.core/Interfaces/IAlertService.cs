namespace mauikit.navigation.core.Interfaces;

/// <summary>
/// Interface for displaying alerts, action sheets and prompts to the user
/// </summary>
public interface IAlertService
{
    /// <summary>
    /// Displays an alert dialog with a single button
    /// </summary>
    /// <param name="title">Title of the alert</param>
    /// <param name="text">Message text</param>
    /// <param name="cancelButton">Text for the cancel button</param>
    Task DisplayAlert(string title, string text, string cancelButton);

    /// <summary>
    /// Displays an alert dialog with accept and cancel buttons
    /// </summary>
    /// <param name="title">Title of the alert</param>
    /// <param name="text">Message text</param>
    /// <param name="acceptButton">Text for the accept button</param>
    /// <param name="cancelButton">Text for the cancel button</param>
    /// <returns>True if accept was tapped, false if cancel was tapped</returns>
    Task<bool> DisplayAlert(string title, string text, string acceptButton, string cancelButton);

    /// <summary>
    /// Displays an action sheet with buttons
    /// </summary>
    /// <param name="title">Title of the action sheet</param>
    /// <param name="cancel">Text for cancel button</param>
    /// <param name="destruction">Text for destruction button</param>
    /// <param name="flowDirection">Flow direction of the action sheet</param>
    /// <param name="buttons">Additional button options</param>
    public Task DisplayActionSheet(string title, string cancel, string destruction, FlowDirection flowDirection,
        string buttons);

    /// <summary>
    /// Displays a prompt dialog for user input
    /// </summary>
    /// <param name="title">Title of the prompt</param>
    /// <param name="message">Message text</param>
    /// <param name="accept">Text for accept button</param>
    /// <param name="cancel">Text for cancel button</param>
    /// <param name="placeholder">Placeholder text for input field</param>
    /// <param name="maxLength">Maximum length of input</param>
    /// <param name="keyboard">Keyboard type to display</param>
    /// <param name="initialValue">Initial value for input field</param>
    /// <returns>The text entered by the user, or null if cancelled</returns>
    Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel",
        string? placeholder = null, int maxLength = 1, Keyboard? keyboard = null, string initialValue = "");
}