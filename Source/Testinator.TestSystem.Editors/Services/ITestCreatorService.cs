using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using QuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    public interface ITestCreatorService
    {
        IGradingEditor GetEditorGrading();
        QuestionEditorMultipleChoice GetEditorMultipleChoice(int? questionNumber = null);
        ITestInfoEditor GetEditorTestInfo();
        ITestOptionsEditor GetEditorTestOptions();
        ICollection<IQuestion> GetPossibleQuestionsFromPool();
        void InitializeNewTest(ITest test = null);
        ITest BuildTest();
    }
}
