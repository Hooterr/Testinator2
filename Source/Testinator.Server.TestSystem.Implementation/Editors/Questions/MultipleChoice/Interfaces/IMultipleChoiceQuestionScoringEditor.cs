using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// The editor for scoring part of the question
    /// </summary>
    public interface IMultipleChoiceQuestionScoringEditor : IQuestionScoringEditor
    {
        /// <summary>
        /// 0-based index of the correct answer for this question
        /// </summary>
        [EditorProperty]
        int CorrectAnswerIdx { get; set; }
    }
}
