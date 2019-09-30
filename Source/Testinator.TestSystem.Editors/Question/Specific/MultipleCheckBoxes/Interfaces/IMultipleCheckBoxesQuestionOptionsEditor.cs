using System.Collections.Generic;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for options part of multiple choice question
    /// </summary>
    public interface IMultipleCheckBoxesQuestionOptionsEditor : IQuestionOptionsEditor
    {
        /// <summary>
        /// The options for this question
        /// </summary>
        [EditorProperty]
        List<string> Options { get; set; }

        /// <summary>
        /// Gets the maximum count of the allowed options
        /// </summary>
        int MaximumCount { get; }

        /// <summary>
        /// Gets the minimum count of the allowed options
        /// </summary>
        int MinimumCount { get; }

        /// <summary>
        /// Gets the initial count of options for brand-new question
        /// </summary>
        int InitialCount { get; }
    }
}
