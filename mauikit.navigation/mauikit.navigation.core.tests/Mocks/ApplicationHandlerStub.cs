using Microsoft.Maui.Handlers;

namespace mauikit.navigation.core.tests.Mocks;

class ApplicationHandlerStub() : ElementHandler<IApplication, object>(Mapper)
{
    public static IPropertyMapper<IApplication, ApplicationHandlerStub> Mapper = new PropertyMapper<IApplication, ApplicationHandlerStub>(ElementMapper);

    protected override object CreatePlatformElement()
    {
        return new object();
    }
}
