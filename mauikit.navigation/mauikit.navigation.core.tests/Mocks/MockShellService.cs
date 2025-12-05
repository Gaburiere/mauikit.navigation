using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.core.tests.Mocks;

internal class MockShellService(Shell shell) : IShellService
{
    public Shell CurrentShell => shell;
}
