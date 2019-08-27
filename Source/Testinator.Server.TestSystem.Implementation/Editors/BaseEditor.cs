using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class BaseEditor : IValidatable
    {
        public abstract void OnValidationError(Action<string> action);
    }
}
