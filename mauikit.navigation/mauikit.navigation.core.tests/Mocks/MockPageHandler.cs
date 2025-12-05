using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace mauikit.navigation.core.tests.Mocks;

public class MockPageHandler : ViewHandler<IContentView, object>
{
    public MockPageHandler() : base(new PropertyMapper<IView>())
    {

    }


    public MockPageHandler(IPropertyMapper mapper) : base(mapper)
    {

    }

    protected override object CreatePlatformView()
    {
        return new object();
    }
}