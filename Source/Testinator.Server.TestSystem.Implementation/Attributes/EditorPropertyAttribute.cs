using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorPropertyAttribute : Attribute
    {
        public EditorPropertyAttribute()
        {

        }
    }
}
