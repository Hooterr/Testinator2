using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public class ConcreteEditorForAttribute : Attribute
    {
        public readonly Type QuestionType;
        public ConcreteEditorForAttribute(Type questionType)
        {
            QuestionType = questionType;
        }
    }
}
