using mauikit.navigation.core.tests.Mocks;

namespace mauikit.navigation.core.tests;

[Collection("maui.navigation.core.tests")]
public abstract class BaseTest
{
    protected BaseTest()
    {
		DispatcherProvider.SetCurrent(new MockDispatcherProvider());
    }
}
