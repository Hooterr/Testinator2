using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    internal class CollectionItemsOnlyDistinctAttribute : BaseEditorAttribute
    {
        public bool Value { get; private set; }

        public CollectionItemsOnlyDistinctAttribute(bool value = true)
        {
            Value = value;
        }

        public CollectionItemsOnlyDistinctAttribute(int fromVersion, bool value = true) : base(fromVersion)
        {
            Value = value;
        }

    }
}
