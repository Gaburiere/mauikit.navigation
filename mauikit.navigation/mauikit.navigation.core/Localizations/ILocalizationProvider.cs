using System.Globalization;

namespace mauikit.navigation.core.Localizations
{
    public interface ILocalizationProvider
    {
        string? GetText(string key, CultureInfo? culture = null);
    }

}
