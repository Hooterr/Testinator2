using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Edits/creates a test
    /// </summary>
    public interface ITestEditor : IBuildable<ITest>
    {
        /// <summary>
        /// The editor for the info part of the test
        /// </summary>
        [Editor]
        ITestInfoEditor Info { get; }
         
        /// <summary>
        /// The editor for the options part of the test
        /// </summary>
        [Editor]
        ITestOptionsEditor Options { get; }

        /// <summary>
        /// The editor for the grading part of the test
        /// </summary>
        [Editor]
        IGradingEditor Grading { get; }

        /// <summary>
        /// Question collection of the test
        /// </summary>
        [Editor]
        IQuestionEditorCollection Questions { get; }
    }
}
