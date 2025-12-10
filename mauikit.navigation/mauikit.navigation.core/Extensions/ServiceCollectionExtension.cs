using System.Resources;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;
using mauikit.navigation.core.Localizations;

namespace mauikit.navigation.core.Extensions;

public static class ServiceCollectionExtension
{

    /// <summary>
    /// Registers the core GiaVannigation services used for navigation and user alerts.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="MauiAppBuilder"/> instance to extend.
    /// </param>
    /// <returns>
    /// The same <see cref="MauiAppBuilder"/> instance, enabling method chaining.
    /// </returns>
    /// <remarks>
    /// <para>Example usage:</para>
    /// <code>
    /// var builder = MauiApp.CreateBuilder();
    /// builder.UseMauiKitNavigation();
    /// </code>
    /// </remarks>
    public static MauiAppBuilder UseMauiKitNavigation(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddSingleton<IShellService, ShellService>();

        return builder;
    }

    /// <summary>
    /// Registers a <see cref="Localizator"/> service in the dependency injection container.
    /// </summary>
    /// <param name="builder">The <see cref="MauiAppBuilder"/> instance to extend.</param>
    /// <param name="managerProvider">The <see cref="ResourceManager"/> providing localized resources.</param>
    /// <returns>The same <see cref="MauiAppBuilder"/> instance for chaining.</returns>
    /// <remarks>
    /// <para>Example:</para>
    /// <code>
    /// builder.UseLocalizationProvider(AppResources.ResourceManager);
    /// </code>
    /// </remarks>
    public static MauiAppBuilder UseLocalizationProvider(
        this MauiAppBuilder builder,
        ResourceManager managerProvider)
    {
        ArgumentNullException.ThrowIfNull(managerProvider);
        builder.Services.AddSingleton(new Localizator(new ResourceManagerProviderWrapper(managerProvider)));
        return builder;
    }

    /// <summary>
    /// Registers a <see cref="Localizator"/> service in the dependency injection container using a custom localization provider,
    /// for example if you need to fetch localizations from a local database 
    /// </summary>
    /// <param name="builder">The <see cref="MauiAppBuilder"/> instance to extend.</param>
    /// <param name="localizationProvider">The custom <see cref="ILocalizationProvider"/> providing localized resources.</param>
    /// <returns>The same <see cref="MauiAppBuilder"/> instance for chaining.</returns>
    /// <remarks>
    /// <para>Example:</para>
    /// <code>
    /// builder.UseLocalizationProvider(new CustomILocalizationProviderInstance());
    /// </code>
    /// </remarks>
    public static MauiAppBuilder UseLocalizationProvider(
        this MauiAppBuilder builder,
        ILocalizationProvider localizationProvider)
    {
        ArgumentNullException.ThrowIfNull(localizationProvider);
        builder.Services.AddSingleton(new Localizator(localizationProvider));
        return builder;
    }

}

