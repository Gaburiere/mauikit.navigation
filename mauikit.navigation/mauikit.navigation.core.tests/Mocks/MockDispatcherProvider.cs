
namespace mauikit.navigation.core.tests.Mocks;

sealed class MockDispatcherProvider : IDispatcherProvider, IDisposable
{
    static readonly MockDispatcher dispatcherMock = new();

    readonly ThreadLocal<IDispatcher> dispatcherInstance = new(() => dispatcherMock);

    public IDispatcher GetForCurrentThread() => dispatcherInstance.Value ?? throw new InvalidOperationException();

    void IDisposable.Dispose() => dispatcherInstance.Dispose();
}
