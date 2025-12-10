using System.Globalization;
using System.Resources;
using AwesomeAssertions;
using mauikit.navigation.core.Localizations;
using NSubstitute;

namespace mauikit.navigation.core.tests.Localizations;

public class LocalizatorTests
{

    [Fact]
    public void Indexer_ShouldReturnLocalizedString_WhenKeyExists()
    {
        // Arrange
        var resourceManager = Substitute.For<ResourceManager>();
        resourceManager.GetObject("Hello", Arg.Any<CultureInfo>()).Returns("Ciao!");
        var provider = Substitute.For<ResourceManagerProviderWrapper>(resourceManager);

        var sut = new Localizator(provider);

        // Act
        var result = sut["Hello"];

        // Assert
        result.Should().Be("Ciao!");
    }

    [Fact]
    public void Indexer_ShouldReturnKey_WhenResourceIsMissing()
    {
        // Arrange
        var resourceManager = Substitute.For<ResourceManager>();
        resourceManager.GetObject("MissingKey", Arg.Any<CultureInfo>()).Returns((object?)null);
        var provider = Substitute.For<ResourceManagerProviderWrapper>(resourceManager);

        var sut = new Localizator(provider);

        // Act
        var result = sut["MissingKey"];

        // Assert
        result.Should().Be("MissingKey");
    }

    [Fact]
    public void SettingCulture_ShouldUpdateGlobalCulture()
    {
        // Arrange
        var resourceManager = Substitute.For<ResourceManager>();
        var provider = Substitute.For<ResourceManagerProviderWrapper>(resourceManager);
        var sut = new Localizator(provider);
        var newCulture = new CultureInfo("fr-FR");

        // Act
        sut.Culture = newCulture;

        // Assert
        CultureInfo.CurrentUICulture.Should().Be(newCulture);
        CultureInfo.CurrentCulture.Should().Be(newCulture);
    }

    [Fact]
    public void SettingCulture_ShouldRaisePropertyChanged()
    {
        // Arrange
        var resourceManager = Substitute.For<ResourceManager>();
        var provider = Substitute.For<ResourceManagerProviderWrapper>(resourceManager);
        var sut = new Localizator(provider);
        bool eventRaised = false;
        sut.PropertyChanged += (_, __) => eventRaised = true;

        // Act
        sut.Culture = new CultureInfo("es-ES");

        // Assert
        eventRaised.Should().BeTrue();
    }


}
