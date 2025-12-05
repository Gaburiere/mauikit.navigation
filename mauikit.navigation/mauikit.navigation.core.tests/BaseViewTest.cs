using CommunityToolkit.Maui;
using mauikit.navigation.core.tests.Mocks;

namespace mauikit.navigation.core.tests;

public abstract class BaseViewTest : BaseTest
{
    protected MauiApp MauiApp { get; }

    public BaseViewTest()
    {
#pragma warning disable CA1416 // Validate platform compatibility
        var builder = MauiApp.CreateBuilder()
        .UseMauiApp<MockApplication>()
        .UseMauiCommunityToolkit()
        .ConfigureFonts(_ => { });
#pragma warning restore CA1416 // Validate platform compatibility

        AddServices(builder.Services);
        MauiApp = builder.Build();

        var shell = new Shell();
        shell.Items.Add(new ShellContent { Content = new ContentPage() });

        var application = (MockApplication)MauiApp.Services.GetRequiredService<IApplication>();
        IPlatformApplication.Current = application;

        application.Handler = new ApplicationHandlerStub();
        application.Handler.SetMauiContext(new HandlersContextStub(MauiApp.Services));

        CreateViewHandler<MockPageHandler>(shell);
    }

    protected virtual void AddServices(IServiceCollection sc)
    {

    }

    protected static TViewHandler CreateViewHandler<TViewHandler>(IView view, bool doesRequireMauiContext = true)
    where TViewHandler : IViewHandler, new()
    {
        var mockViewHandler = new TViewHandler();
        mockViewHandler.SetVirtualView(view);

        if (doesRequireMauiContext)
        {
            mockViewHandler.SetMauiContext(Application.Current?.Handler?.MauiContext ?? throw new NullReferenceException());
        }

        return mockViewHandler;
    }
}
