using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IEditor<T>
    {
        OperationResult<T> Build();
    }
}
