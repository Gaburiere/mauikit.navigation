using mauikit.navigation.core.Localizations;
using System.Globalization;

namespace mauikit.navigation.sample
{
    /// <summary>
    /// Sample custom provder. Instead of dictionary you can use local storage or an API to get the translations.
    /// </summary>
    internal class CustomLocalizationProvider : ILocalizationProvider
    {
        private readonly Dictionary<string, string> _enUS = new()
        {
            { "ChangeLanguage", "Change Language Us" },
            { "LabelKey", "Hello World from Resx Us" },
            { "LabelOtherKey", "Label formatted Us" },
            { "NavigateToModalPage", "Navigate To Modal Page Us" },
            { "NavigateToSimplePage", "Navigate To Simple Page Us" },
            { "OpenPopup", "Open Popup Us" },
            { "OpenPopupWithReturn", "Open Popup With Return Us" }
        };

        private readonly Dictionary<string, string> _frFR = new()
        {
            { "ChangeLanguage", "Change Language Fr" },
            { "LabelKey", "Hello World from Resx Fr" },
            { "LabelOtherKey", "Label formatted Fr" },
            { "NavigateToModalPage", "Navigate To Modal Page Fr" },
            { "NavigateToSimplePage", "Navigate To Simple Page Fr" },
            { "OpenPopup", "Open Popup Fr" },
            { "OpenPopupWithReturn", "Open Popup With Return Fr" }
        };

        private readonly Dictionary<string, string> _itIT = new()
        {
            { "ChangeLanguage", "Change Language It" },
            { "LabelKey", "Hello World from Resx It" },
            { "LabelOtherKey", "Label formatted It" },
            { "NavigateToModalPage", "Navigate To Modal Page It" },
            { "NavigateToSimplePage", "Navigate To Simple Page It" },
            { "OpenPopup", "Open Popup It" },
            { "OpenPopupWithReturn", "Open Popup With Return It" }
        };

        public string? GetText(string key, CultureInfo? culture = null)
        {
            if(culture?.Name == "it-IT")
            {
                return _itIT.TryGetValue(key, out var value) ? value : key;
            }
            if (culture?.Name == "fr-FR")
            {
                return _frFR.TryGetValue(key, out var value) ? value : key;
            }
            return _enUS.TryGetValue(key, out var enValue) ? enValue : key;
        }
    }
}
