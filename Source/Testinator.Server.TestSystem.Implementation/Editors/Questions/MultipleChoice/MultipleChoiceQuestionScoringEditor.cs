using System;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.Server.TestSystem.Implementation.Questions.ScoringStrategy;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class MultipleChoiceQuestionScoringEditor : BaseEditor<MultipleChoiceQuestionScoring, IMultipleChoiceQuestionScoringEditor>, IMultipleChoiceQuestionScoringEditor
    {
        private Type mDefaultStrategyType;
        private int mMaxScore;
        private int mMinScore;
        private IScoringStrategy mScoringStrategy;

        public int MaximumScore { get; set; }
        public int CorrectAnswerIdx { get; set; }

        protected override MultipleChoiceQuestionScoring BuildObject()
        {
            MultipleChoiceQuestionScoring scoring = null;
            if (IsInEditorMode())
                scoring = OriginalObject;
            else
                scoring = new MultipleChoiceQuestionScoring();

            scoring.CorrectAnswerIdx = CorrectAnswerIdx;
            scoring.MaximumScore = MaximumScore;
            scoring.Strategy = mScoringStrategy;
            return scoring;
        }

        public MultipleChoiceQuestionScoringEditor(int version) : base(version) { }

        public MultipleChoiceQuestionScoringEditor(MultipleChoiceQuestionScoring scoring, int version) : base(scoring, version) { }

        protected override void OnInitialize()
        {
            var scoreRangeAttr = AttributeHelper.GetPropertyAttribute<IMultipleChoiceQuestionScoringEditor, int, IntegerValueRangeAttribute>
                (x => x.MaximumScore, Version);

            mMaxScore = scoreRangeAttr.Max;
            mMinScore = scoreRangeAttr.Min;

            mDefaultStrategyType = AttributeHelper.GetPropertyAttributeValue<IMultipleChoiceQuestionScoringEditor, int, DefaultStrategyAttribute, Type>
                (x => x.MaximumScore, attr => attr.DefaultStrategyType, Version);

            if (mDefaultStrategyType != null)
            {
                if (mDefaultStrategyType.GetInterface(nameof(IScoringStrategy)) == null)
                    throw new Exception($"Default value for scoring strategy but implement {nameof(IScoringStrategy)}.");

                mScoringStrategy = (IScoringStrategy)Activator.CreateInstance(mDefaultStrategyType);
            }

        }

        internal override bool Validate()
        {
            var validationPassed = true;
            if (MaximumScore < mMinScore || MaximumScore > mMaxScore)
            {
                HandleErrorFor(x => x.MaximumScore, $"Maximum point score must from {mMinScore} to {mMaxScore}.");
                validationPassed = false;
            }

            if (false == mDefaultStrategyType.IsAssignableFrom(mScoringStrategy.GetType()))
            {
                HandleError($"The only valid type of scoring strategy for this question is {mDefaultStrategyType.Name}.");
                validationPassed = false;
            }

            return validationPassed;
        }
    }
}
