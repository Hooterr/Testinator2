using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.TestSystem.Editors
{
    public interface ITestEditor : IBuildable<ITest> //, TODO nested error listener
    {
        // TODO: Maybe operation results instead of voids
        /*void SubmitInfo(ITestInfo testInfo);
        void SubmitQuestion(IQuestion question);
        void SubmitOptions(ITestOptions testOptions);
        void SubmitGrading(IGrading grading);*/

        ITestInfoEditor Info { get; }
         
        ITestOptionsEditor Options { get; }

        IGradingEditor Grading { get; }

        IQuestionEditorCollection Questions { get; }
    }
}
