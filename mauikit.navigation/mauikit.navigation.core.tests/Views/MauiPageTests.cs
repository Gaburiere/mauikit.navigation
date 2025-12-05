using mauikit.navigation.core.Interfaces;
using mauikit.navigation.core.Views;
using NSubstitute;

namespace mauikit.navigation.core.tests.Views;

public class MauiPageTests : BaseViewTest
{
    [Fact]
    public void ShouldInvoke_AppearingCommand()
    {
        // Arrange
        var vm = Substitute.For<IMauiViewModel>();
        var page = new MauiPage { BindingContext = vm };

        // Act
        page.SendAppearing();

        // Assert
        vm.AppearingCommand.Received(1);
    }

    [Fact]
    public void ShouldInvoke_DisappearingCommand()
    {
        // Arrange
        var vm = Substitute.For<IMauiViewModel>();
        var page = new MauiPage { BindingContext = vm };

        // Act
        page.SendDisappearing();

        // Assert
        vm.DisappearingCommand.Received(1);
    }
}
