using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using QuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    public interface ITestCreatorService
    {
        IGradingEditor GetEditorGrading(IGrading grading = null);
        QuestionEditorMultipleChoice GetEditorMultipleChoice(int? questionNumber = null);
        ITestInfoEditor GetEditorTestInfo(ITestInfo testInfo = null);
        ITestOptionsEditor GetEditorTestOptions(ITestOptions testOptions = null);
        ICollection<IQuestion> GetPossibleQuestionsFromPool();
        void InitializeNewTest(ITest test = null);
        void SubmitGrading(IGrading grading);
        void SubmitQuestion(IQuestion question);
        ITest SubmitTest();
        void SubmitTestInfo(ITestInfo testInfo);
        void SubmitTestOptions(ITestOptions testOptions);
    }
}
