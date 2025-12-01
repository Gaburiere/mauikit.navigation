using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.core.Classes;

internal class AlertService: IAlertService
{
    /// <inheritdoc />
    public Task DisplayAlert(string title, string text, string cancelButton)
    {
        return Shell.Current.DisplayAlertAsync(title, text, cancelButton);
    }

    /// <inheritdoc />
    public Task<bool> DisplayAlert(string title, string text,  string acceptButton, string cancelButton)
    {
        return Shell.Current.DisplayAlertAsync(title, text, acceptButton, cancelButton);
    }

    /// <inheritdoc />
    public Task DisplayActionSheet(string title, string cancel, string destruction, FlowDirection flowDirection,
        string buttons)
    {
        return Shell.Current.DisplayActionSheetAsync(title, cancel, destruction, flowDirection, buttons);
    }

    /// <inheritdoc />
    public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK",
        string cancel = "Cancel", string? placeholder = null, int maxLength = 1, Keyboard? keyboard = null, string initialValue = "")
    {
        return Shell.Current.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);
    }
}