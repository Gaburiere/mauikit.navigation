using System.Globalization;
using System.Resources;
using AwesomeAssertions;
using mauikit.navigation.core.Localizations;
using NSubstitute;

namespace mauikit.navigation.core.tests.Localizations;

public class TranslateExtensionTests : BaseViewTest
{
    protected override void AddServices(IServiceCollection sc)
    {
        var fakeRm = Substitute.For<ResourceManager>();
        fakeRm.GetObject("Hello", Arg.Any<CultureInfo>()).Returns("Ciao!");
        fakeRm.GetObject("Goodbye", Arg.Any<CultureInfo>()).Returns("Addio!");
        sc.AddSingleton(new Localizator(fakeRm));
    }

    [Fact]
    public void TranslatedName_ShouldReturnLocalizedString()
    {
        var sut = new TranslateExtension { Name = "Hello" };
        sut.TranslatedName.Should().Be("Ciao!");
    }

    [Fact]
    public void TranslatedName_ShouldFallbackToKey_WhenMissing()
    {
        var sut = new TranslateExtension { Name = "Missing" };
        sut.TranslatedName.Should().Be("Missing");
    }

    [Fact]
    public void ShouldRaisePropertyChanged_WhenCultureChanges()
    {
        // arrange
        var localizator = IPlatformApplication.Current!.Services.GetRequiredService<Localizator>();
        var sut = new TranslateExtension();
        bool raised = false;
        sut.PropertyChanged += (_, e) =>
            raised = e.PropertyName == nameof(TranslateExtension.TranslatedName);

        // act
        localizator.Culture = new CultureInfo("fr-FR");

        // assert
        raised.Should().BeTrue();
    }

}

