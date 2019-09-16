using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Attributes
{
    public class CollectionItemsOnlyDistinctAttribute : BaseEditorAttribute
    {
        public bool Value { get; private set; }

        public CollectionItemsOnlyDistinctAttribute(int fromVersion, bool value = true) : base(fromVersion)
        {
            Value = value;
        }

    }
}
