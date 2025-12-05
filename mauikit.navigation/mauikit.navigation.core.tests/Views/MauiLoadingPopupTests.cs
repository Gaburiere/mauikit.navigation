using AwesomeAssertions;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.Interfaces;
using mauikit.navigation.core.Views;
using NSubstitute;

namespace mauikit.navigation.core.tests.Views;

public class MauiLoadingPopupTests : BaseViewTest
{
    [Fact]
    public void Should_Have_White_As_Default_LoaderColor()
    {
        var popup = new MauiLoadingPopup();
        popup.LoaderColor.Should().Be(Colors.White);
    }

    [Fact]
    public async Task Should_Show_And_Hide_Overlay_When_IsBusy_Changes()
    {
        // Arrange
        var vm = new MauiPopupViewModel(Substitute.For<INavigationService>())
        {
            IsBusy = false
        };

        var popup = new MauiLoadingPopup { BindingContext = vm };

        // Simulate MAUI template creation and attach it to the page
        var content = (Layout)popup.ControlTemplate.CreateContent();
        content.BindingContext = vm;
        content.Parent = popup;
        await Task.Yield();

        var overlay = content.FindByName<Grid>("LoadingPopupOverlay");
        overlay.Should().NotBeNull();

        // Act 1: IsBusy false
        overlay!.IsVisible.Should().BeFalse("IsBusy is initially false");

        // Act 2: IsBusy true
        vm.IsBusy = true;
        await Task.Delay(50);
        overlay.IsVisible.Should().BeTrue("overlay should be visible when IsBusy = true");

        // Act 3: IsBusy false again
        vm.IsBusy = false;
        await Task.Delay(50);
        overlay.IsVisible.Should().BeFalse("overlay should hide when IsBusy = false");
    }
}
