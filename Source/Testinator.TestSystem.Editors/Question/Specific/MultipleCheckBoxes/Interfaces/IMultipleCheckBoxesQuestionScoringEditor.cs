using System.Collections.Generic;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for scoring part of the question
    /// </summary>
    public interface IMultipleCheckBoxesQuestionScoringEditor : IQuestionScoringEditor
    {
        /// <summary>
        /// 0-based index of the correct answer for this question
        /// </summary>
        [EditorProperty]
        List<bool> CorrectAnswers { get; set; }
    }
}
