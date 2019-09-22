using System.Collections.Generic;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Attributes;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test.EditorsTests
{
    public class MultipleChoiceQuestionEditorTests
    {
        internal static MultipleChoiceQuestionEditor NewEditor(int version = 1) => new MultipleChoiceQuestionEditor(1);

        [Fact]
        public void CreateEditorNewQuestionVersion1()
        {
            var editor = NewEditor();
            Assert.NotNull(editor);
        }

        [Fact]
        public void PostBuildValidationCorrectAnswerLessThan0()
        {
            var editor = NewEditor();
            editor.Scoring.CorrectAnswerIdx = -1;
            var result = editor.Build();
            Assert.False(result.Succeeded);
            Assert.Contains("Correct answer must be matched to the number of options.", result.Errors);
        }

        [Fact]
        public void PostBuildValidationCorrectAnswerMoreThanOptionsCount()
        {
            var editor = NewEditor();
            editor.Scoring.CorrectAnswerIdx = 1;
            var result = editor.Build();
            Assert.False(result.Succeeded);
            Assert.Contains("Correct answer must be matched to the number of options.", result.Errors);
        }

        [Fact]
        public void PostBuildValidationCorrectAnswerMoreThanOptionsWhenOptionsMoreThan0()
        {
            var editor = NewEditor();
            editor.Options.ABCD.Add("ddd");
            editor.Options.ABCD.Add("22");
            editor.Options.ABCD.Add("dd");
            editor.Scoring.CorrectAnswerIdx = 4;
            var result = editor.Build();
            Assert.False(result.Succeeded);
            Assert.Contains("Correct answer must be matched to the number of options.", result.Errors);
        }

        [Fact]
        public void PostBuildValidationAllGood()
        {
            var editor = NewEditor();
            editor.Options.ABCD.Add("ddd");
            editor.Options.ABCD.Add("22");
            editor.Options.ABCD.Add("dd");
            editor.Scoring.CorrectAnswerIdx = 2;
            var result = editor.Build();
            Assert.DoesNotContain("Correct answer must be matched to the number of options.", result.Errors);
        }
    }

    public class ABCOptionsMock : IQuestionOptions
    {
        /// <summary>
        /// ABC options for this question
        /// </summary>
        [CollectionCount(min: 2, max: 5, fromVersion: 1)]
        [StringLength(min: 1, max: 150, fromVersion: 1)]
        [CollectionItemsOnlyDistinct(value: true, fromVersion: 1)]
        public List<string> Options { get; internal set; }
    }

    public sealed class ABCScoringMock : BaseQuestionScoring<MultipleChoiceQuestionUserAnswer>
    {
        [EditorProperty]
        public int CorrectAnswerIdx { get; internal set; }

        protected override int CalculateCorrectPercentage(MultipleChoiceQuestionUserAnswer answer)
        {
            return answer.SelectedAnswerIdx == CorrectAnswerIdx ? MaximumScore : 0;
        }
    }

    public sealed class ABCQuestionMock : BaseQuestion
    {

        #region Implementation

        public new ABCOptionsMock Options { get; internal set; }

        public new ABCScoringMock Scoring { get; internal set; }

        #endregion

        public ABCQuestionMock() { }

    }
}
