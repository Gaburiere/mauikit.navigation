using CommunityToolkit.Mvvm.Input;

namespace mauikit.navigation.core.Interfaces;

/// <summary>
/// Interface for MAUI view models providing base functionality
/// </summary>
public interface IMauiViewModel
{
    /// <summary>
    /// Gets or sets a value indicating whether the view model is currently busy
    /// </summary>
    public bool IsBusy { get; set; }

    /// <summary>
    /// Command that gets executed when the view appears
    /// </summary>
    public IAsyncRelayCommand AppearingCommand { get; }

    /// <summary>
    /// Command that gets executed when the view disappears
    /// </summary>
    public IAsyncRelayCommand DisappearingCommand { get; }
}