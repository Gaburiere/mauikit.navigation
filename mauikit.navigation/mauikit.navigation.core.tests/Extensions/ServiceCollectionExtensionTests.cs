using System.Resources;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Extensions;
using mauikit.navigation.core.Interfaces;
using mauikit.navigation.core.Localizations;
using NSubstitute;

namespace mauikit.navigation.core.tests.Extensions;

public class ServiceCollectionExtensionTests
{
    [Fact]
    public void IServiceCollection_VerifySingleton_UseGiaVannigation()
    {
        // Arrange
        const ServiceLifetime expectedServiceLifetime = ServiceLifetime.Singleton;
        var builder = MauiApp.CreateBuilder();

        // Act
        builder.UseMauiKitNavigation();
        var services = builder.Services;

        // Assert
        _ = services.Single(s => s.ServiceType == typeof(INavigationService) && s.ImplementationType == typeof(NavigationService) && s.Lifetime.Equals(expectedServiceLifetime));
        _ = services.Single(s => s.ServiceType == typeof(IAlertService) && s.ImplementationType == typeof(AlertService) && s.Lifetime.Equals(expectedServiceLifetime));
        _ = services.Single(s => s.ServiceType == typeof(IShellService) && s.ImplementationType == typeof(ShellService) && s.Lifetime.Equals(expectedServiceLifetime));
    }

    [Fact]
    public void IServiceCollection_UseLocalizationProvider_ShouldThrowArgumentNullException()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act // Assert
		Assert.Throws<ArgumentNullException>(() => builder.UseLocalizationProvider(managerProvider:null));
    }

    [Fact]
    public void IServiceCollection_VerifySingleton_UseLocalizationProvider()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var sp = builder.UseLocalizationProvider(Substitute.For<ResourceManager>()).Services.BuildServiceProvider();
        
        // Assert
        _ = sp.GetRequiredService<Localizator>();
    }

    [Fact]
    public void IServiceCollection_VerifySingleton_UseLocalizationProviderCustom()
    {
        // Arrange
        var builder = MauiApp.CreateBuilder();

        // Act
        var sp = builder.UseLocalizationProvider(Substitute.For<ILocalizationProvider>()).Services.BuildServiceProvider();

        // Assert
        _ = sp.GetRequiredService<Localizator>();
    }
}
