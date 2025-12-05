using AwesomeAssertions;
using CommunityToolkit.Maui;
using mauikit.navigation.core.Classes;
using mauikit.navigation.core.tests.Mocks;
using NSubstitute;

namespace mauikit.navigation.core.tests.Classes;

public class NavigationTests : BaseViewTest
{
    private readonly string _targetRoute = nameof(ContentPage);
    private readonly Shell _mockShell;
    private readonly NavigationService _navigationService;
    private readonly object _dummyComplexObject = new { Data = "Data" };

    public NavigationTests()
    {
        _mockShell = new Shell();
        _navigationService = new NavigationService(Substitute.For<IPopupService>(), new MockShellService(_mockShell));
    }

    [Fact]
    public async Task MauiViewModel_Receive_OnNavigatedTo_And_OnNavigatedBack_Calls_Properly()
    {
        //arrange
        var sourceVm = Substitute.For<MauiViewModel>(_navigationService);
        var sourcePage = new ContentPage() { BindingContext = sourceVm };
        _mockShell.Items.Add(sourcePage);

        var targetVm = Substitute.For<MauiViewModel>(_navigationService);
        var targetPage = new ContentPage() { BindingContext = targetVm };

        Routing.RegisterRoute(_targetRoute, new MockPageRouteFactory(() => targetPage));

        //act push
        await sourceVm.Push<ContentPage>(_dummyComplexObject);

        //assert push
        await targetVm.Received(1).OnNavigatedTo(_dummyComplexObject);
        _mockShell.CurrentPage.Should().BeSameAs(targetPage);

        //act pop
        await targetVm.Pop(_dummyComplexObject);

        //assert pop
        await sourceVm.Received(1).OnNavigatedBack(_dummyComplexObject);
        _mockShell.CurrentPage.Should().BeSameAs(sourcePage);
    }

}
