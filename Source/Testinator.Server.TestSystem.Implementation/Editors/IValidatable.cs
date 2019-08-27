using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{ 
    public interface IValidatable
    {
        void OnValidationError(Action<string> action);
    }
}
