using CommunityToolkit.Mvvm.Input;

namespace mauikit.navigation.core.Interfaces;

/// <summary>
/// Interface for popup view models in MAUI applications.
/// </summary>
public interface IMauiPopupViewModel
{
    /// <summary>
    /// Gets or sets a value indicating whether the popup is busy performing an operation.
    /// </summary>
    public bool IsBusy { get; set; }
    
    /// <summary>
    /// Gets the command executed when the popup is opened.
    /// </summary>
    public IAsyncRelayCommand OpenedCommand { get; }

    /// <summary>
    /// Gets the command executed when the popup is closed.
    /// </summary>
    public IAsyncRelayCommand ClosedCommand { get; }
}