using System.Collections.Generic;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for options part of multiple choice question
    /// </summary>
    public interface IMultipleChoiceQuestionOptionsEditor : IQeustionOptionsEditor
    {
        /// <summary>
        /// The ABC options for this question
        /// NOTE: every null or empty options that are at the end of the list will be skipped
        /// </summary>
        [EditorProperty]
        List<string> Options { get; set; }

        /// <summary>
        /// Shortcut to set multiple options at once
        /// </summary>
        /// <param name="options"></param>
        void SetOptions(params string[] options);

        /// <summary>
        /// Gets the maximum count of the allowed options
        /// </summary>
        /// <returns>The maximum number of options</returns>
        int GetMaximumCount();


        /// <summary>
        /// Gets the minimum count of the allowed options
        /// </summary>
        /// <returns>The minimum number of options</returns>
        int GetMinimumCount();
    }
}
