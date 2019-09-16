using System;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Implementation.Questions.ScoringStrategy;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Implementation of ABC question scoring editor
    /// </summary>
    internal class MultipleChoiceQuestionScoringEditor : BaseEditor<MultipleChoiceQuestionScoring, IMultipleChoiceQuestionScoringEditor>, IMultipleChoiceQuestionScoringEditor
    {
        #region Private Members

        /// <summary>
        /// The type of default strategy to use
        /// </summary>
        private Type mDefaultStrategyType;

        /// <summary>
        /// The maximum point score 
        /// </summary>
        private int mMaxScore;

        /// <summary>
        /// The minimum point score
        /// </summary>
        private int mMinScore;

        /// <summary>
        /// The scoring strategy to use
        /// </summary>
        private IScoringStrategy mScoringStrategy;

        #endregion

        #region Public Properties

        /// <summary>
        /// Maximum point score
        /// </summary>
        public int MaximumScore { get; set; }

        /// <summary>
        /// The index of a correct answer
        /// </summary>
        public int CorrectAnswerIdx { get; set; }

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes this editor to create new scoring guidelines
        /// </summary>
        /// <param name="version">The question model version to use</param>
        public MultipleChoiceQuestionScoringEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes this editor to edit an existing scoring
        /// </summary>
        /// <param name="scoring">The scoring to edit</param>
        /// <param name="version">The question model version to use</param>
        public MultipleChoiceQuestionScoringEditor(MultipleChoiceQuestionScoring scoring, int version) : base(scoring, version) { }

        #endregion

        #region Overridden

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

        protected override void OnInitialize()
        {
            var scoreRangeAttr = AttributeHelper.GetPropertyAttribute<MultipleChoiceQuestionScoring, int, IntegerValueRangeAttribute>
                (x => x.MaximumScore, Version);

            mMaxScore = scoreRangeAttr.Max;
            mMinScore = scoreRangeAttr.Min;

            mDefaultStrategyType = AttributeHelper.GetPropertyAttributeValue<MultipleChoiceQuestionScoring, DefaultStrategyAttribute, Type>
                (x => x.Strategy, attr => attr.DefaultStrategyType, Version);

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

        #endregion
    }
}
