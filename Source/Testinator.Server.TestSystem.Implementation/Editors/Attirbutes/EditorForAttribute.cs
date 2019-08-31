using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    public class EditorForAttribute : Attribute
    {
        public readonly Type QuestionType;
        public EditorForAttribute(Type questionType)
        {
            QuestionType = questionType;
        }
    }
}
