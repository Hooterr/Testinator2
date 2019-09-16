using System;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Informs the system what type of question this editor is made for
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EditorForAttribute : Attribute
    {
        /// <summary>
        /// The type of question the editor is made to operate on
        /// </summary>
        public Type QuestionType { get; private set; }

        /// <summary>
        /// Initializes the attribute
        /// </summary>
        /// <param name="questionType">Type of question the editor will operate on</param>
        public EditorForAttribute(Type questionType)
        {
            QuestionType = questionType;
        }
    }
}
