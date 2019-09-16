using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorPropertyAttribute : Attribute
    {
        public EditorPropertyAttribute()
        {

        }
    }
}
