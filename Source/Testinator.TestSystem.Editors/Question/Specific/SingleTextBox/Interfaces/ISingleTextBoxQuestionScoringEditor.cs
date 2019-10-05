using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    public interface ISingleTextBoxQuestionScoringEditor : IQuestionScoringEditor, IQuestionMultipleAnswersEditor
    {
        [EditorProperty]
        IDictionary<string, float> CorrectAnswers { get; set; }

        [EditorProperty]
        bool IsCaseSensitive { get; set; }

    }
}
