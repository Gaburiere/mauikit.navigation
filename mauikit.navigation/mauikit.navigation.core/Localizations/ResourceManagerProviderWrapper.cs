using System;
using System.Globalization;
using System.Resources;

namespace mauikit.navigation.core.Localizations
{
    internal class ResourceManagerProviderWrapper(ResourceManager resourceManager) : ILocalizationProvider
    {
        public string? GetText(string key, CultureInfo? culture = null)
        {
             return resourceManager.GetObject(key, culture)?.ToString();
        }
    }
}
