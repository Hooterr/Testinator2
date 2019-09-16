using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation.Test.ErrorListener
{
    internal class ErrorListenerTestImplementation : ErrorListener<IListener_TEST>
    {
        public void HandleErrorFor(Expression<Func<IListener_TEST, object>> propertyExpression, string message)
        {
            base.HandleErrorFor(propertyExpression, message);
        }
        public new List<string> GetUnhandledErrors()
        {
            return base.GetUnhandledErrors();
        }
        public new void HandleError(string message)
        {
            base.HandleError(message);
        }

        public new void ClearAllErrors()
        {
            base.ClearAllErrors();
        }
    }

    public interface IListener_TEST
    {
        [EditorProperty]
        int Prop1 { get; set; }

        [EditorProperty]
        string Prop2 { get; set; }

        [EditorProperty]
        double Prop3 { get; set; }

        int PropNotEditorProp1 { get; set; }

        int PropNotEditorProp2 { get; set; }
    }
}
