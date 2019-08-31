using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IBuildable<T>
    {
        OperationResult<T> Build();
    }
}
