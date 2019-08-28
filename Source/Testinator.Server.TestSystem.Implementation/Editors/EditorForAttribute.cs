using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class ImplementedInterfaceAttribute : Attribute
    {
        public readonly Type ImplementedInterface;
        public ImplementedInterfaceAttribute(Type implementedInterface)
        {
            if (!implementedInterface.IsInterface)
                throw new ArgumentException($"{nameof(implementedInterface)} must be an interface.");

            ImplementedInterface = implementedInterface;
        }
    }
}
