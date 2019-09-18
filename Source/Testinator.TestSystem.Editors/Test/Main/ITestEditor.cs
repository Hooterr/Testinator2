using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Edits/creates a test
    /// </summary>
    public interface ITestEditor : IBuildable<ITest> //, TODO nested error listener
    {
        /// <summary>
        /// The editor for the info part of the test
        /// </summary>
        ITestInfoEditor Info { get; }
         
        /// <summary>
        /// The editor for the options part of the test
        /// </summary>
        ITestOptionsEditor Options { get; }

        /// <summary>
        /// The editor for the grading part of the test
        /// </summary>
        IGradingEditor Grading { get; }

        /// <summary>
        /// Question collection of the test
        /// </summary>
        IQuestionEditorCollection Questions { get; }
    }
}
