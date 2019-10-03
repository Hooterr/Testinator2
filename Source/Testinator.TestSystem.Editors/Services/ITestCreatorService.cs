using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using IQuestionEditorMultipleCheckBoxes = IQuestionEditor<MultipleCheckBoxesQuestion, IMultipleCheckBoxesQuestionOptionsEditor, IMultipleCheckBoxesQuestionScoringEditor>;
    using IQuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    public interface ITestCreatorService
    {
        IGradingEditor GetEditorGrading();
        IGradingPresetEditor GetEditorGradingPreset(IGradingPreset gradingPreset = null);
        IQuestionEditorCollection GetEditorTestQuestions();
        IQuestionEditorMultipleChoice GetEditorMultipleChoice(int? questionNumber = null);
        IQuestionEditorMultipleCheckBoxes GetEditorMultipleCheckBoxes(int? questionNumber = null);
        ITestInfoEditor GetEditorTestInfo();
        ITestOptionsEditor GetEditorTestOptions();
        ICollection<IQuestion> GetPossibleQuestionsFromPool();
        void InitializeNewTest(ITest test = null);
        ITest BuildTest();
    }
}
