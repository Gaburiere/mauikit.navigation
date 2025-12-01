using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace mauikit.navigation.core.tests.Mocks;
class HandlersContextStub : IMauiContext
{
    public HandlersContextStub(IServiceProvider services)
    {
        Services = services;
        Handlers = Services.GetRequiredService<IMauiHandlersFactory>();
    }

    public IServiceProvider Services { get; }

    public IMauiHandlersFactory Handlers { get; }
}