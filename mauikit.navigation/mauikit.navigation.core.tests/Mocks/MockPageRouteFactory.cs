namespace mauikit.navigation.core.tests.Mocks;

internal class MockPageRouteFactory(Func<Element> create) : RouteFactory
{
    public override Element GetOrCreate() => create();

    public override Element GetOrCreate(IServiceProvider services) => create();

}
