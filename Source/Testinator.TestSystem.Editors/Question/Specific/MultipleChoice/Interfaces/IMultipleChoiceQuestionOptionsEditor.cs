using System.Collections.Generic;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for options part of multiple choice question
    /// </summary>
    public interface IMultipleChoiceQuestionOptionsEditor : IQuestionOptionsEditor, IQuestionMultipleAnswersEditor
    {
        /// <summary>
        /// The ABCD options for this question
        /// </summary>
        [EditorProperty]
        List<string> ABCD { get; set; }
    }
}
