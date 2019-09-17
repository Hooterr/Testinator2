using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.TestSystem.Editors
{
    public interface ITestEditor : IBuildable<ITest>
    {
        // TODO: Maybe operation results instead of voids
        void SubmitInfo(ITestInfo testInfo);
        void SubmitQuestion(IQuestion question);
        void SubmitOptions(ITestOptions testOptions);
        void SubmitGrading(IGrading grading);

        /// <summary>
        /// Gets question from the current test at specified index
        /// </summary>
        /// <param name="index">The index of a question to take from test</param>
        /// <returns>Question object as <see cref="IQuestion"/></returns>
        IQuestion GetQuestionFromTestAt(int index);
    }
}
