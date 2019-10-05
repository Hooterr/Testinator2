using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Implementation;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using IQuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;
    using IQuestionEditorMultipleCheckBoxes = IQuestionEditor<MultipleCheckBoxesQuestion, IMultipleCheckBoxesQuestionOptionsEditor, IMultipleCheckBoxesQuestionScoringEditor>;
    using IQuestionEditorSingleTextBox = IQuestionEditor<SingleTextBoxQuestion, ISingleTextBoxQuestionOptionsEditor, ISingleTextBoxQuestionScoringEditor>;

    public interface ITestCreatorService
    {
        IGradingEditor GetEditorGrading();
        IQuestionEditorCollection GetEditorTestQuestions();
        IQuestionEditorMultipleChoice GetEditorMultipleChoice(int? questionNumber = null);
        IQuestionEditorMultipleCheckBoxes GetEditorMultipleCheckBoxes(int? questionNumber = null);
        IQuestionEditorSingleTextBox GetEditorSingleTextBox(int? questionNumber = null);
        ITestInfoEditor GetEditorTestInfo();
        ITestOptionsEditor GetEditorTestOptions();
        ICollection<IQuestion> GetPossibleQuestionsFromPool();
        void InitializeNewTest(ITest test = null);
        ITest BuildTest();
    }
}
