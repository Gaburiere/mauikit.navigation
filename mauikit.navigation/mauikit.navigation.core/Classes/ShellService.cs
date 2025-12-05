using mauikit.navigation.core.Interfaces;

namespace mauikit.navigation.core.Classes;

internal class ShellService : IShellService
{
    public Shell CurrentShell => Shell.Current;
}
