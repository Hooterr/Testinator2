using System.Collections.Generic;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for options part of multiple checkboxes question
    /// </summary>
    public interface IMultipleCheckBoxesQuestionOptionsEditor : IQuestionOptionsEditor, IQuestionMultipleAnswersEditor
    {
        /// <summary>
        /// The options for this question
        /// </summary>
        [EditorProperty]
        List<string> Boxes { get; set; }

    }
}
