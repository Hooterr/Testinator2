using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    public static class Editors
    {
        public static IEditorBuilder<IMultipleChoiceQuestionEditor, MultipleChoiceQuestion> MultipleChoiceQuestion
            => new EditorBuilder<MultipleChoiceQuestionEditor, IMultipleChoiceQuestionEditor, MultipleChoiceQuestion>();
    }
}
