
namespace mauikit.navigation.core.tests.Mocks;

class MockApplication : Application, IPlatformApplication
{
    public MockApplication(IServiceProvider serviceProvider)
    {
        Services = serviceProvider;
    }

    public IApplication Application => this;
    public IServiceProvider Services { get; }
}