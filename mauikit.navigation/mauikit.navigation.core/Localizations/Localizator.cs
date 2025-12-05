using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace mauikit.navigation.core.Localizations;

/// <summary>
/// Provides access to localized resources and manages the current UI culture.
/// </summary>
/// <remarks>
/// This class acts as a localization helper that retrieves translated strings
/// from a <see cref="ResourceManager"/> and notifies the UI when the culture changes.
/// </remarks>
public partial class Localizator(ResourceManager resourceManagerProvider) : INotifyPropertyChanged
{
    private readonly ResourceManager _resourceManagerProvider = resourceManagerProvider;

    /// <summary>
    /// Gets or sets the current culture used for localization.
    /// Changing this value updates <see cref="CultureInfo.CurrentUICulture"/>
    /// and <see cref="CultureInfo.CurrentCulture"/> globally and notifies listeners.
    /// </summary>
    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }

    /// <summary>
    /// Gets a localized string for the specified resource key.
    /// </summary>
    /// <param name="resourceKey">The key of the string resource to retrieve.</param>
    /// <returns>
    /// The localized string if found; otherwise, returns the key itself.
    /// </returns>
    public string this[string resourceKey]
        => _resourceManagerProvider.GetObject(resourceKey, Culture)?.ToString() ?? resourceKey;

    /// <summary>
    /// Occurs when a property value changes, typically after updating the <see cref="Culture"/>.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}
