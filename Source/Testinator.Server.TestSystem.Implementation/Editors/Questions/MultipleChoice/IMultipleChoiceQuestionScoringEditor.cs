using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IMultipleChoiceQuestionScoringEditor : IQuestionScoringEditor
    {
        [EditorProperty]
        int CorrectAnswerIdx { get; set; }
    }
}
