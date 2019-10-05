using System.Collections.Generic;
using System.Linq;
using Testinator.TestSystem.Attributes;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    internal class SingleTextBoxQuestionScoringEditor : NestedEditor<SingleTextBoxQuestionScoring, ISingleTextBoxQuestionScoringEditor>, ISingleTextBoxQuestionScoringEditor
    {
        private int mMaxAnswers;
        private int mMinAnswers;
        private int mMaxScore;
        private int mMinScore;

        public IDictionary<string, float> CorrectAnswers { get; set; }

        public bool IsCaseSensitive { get; set; }

        public int MaximumScore { get; set; }

        public int MaximumCount => mMaxAnswers;

        public int MinimumCount => mMinAnswers;

        public int InitialCount => mMinAnswers;

        public SingleTextBoxQuestionScoringEditor(SingleTextBoxQuestionScoring scoring, int version) : base(scoring, version) { }

        public SingleTextBoxQuestionScoringEditor(int version) : base(version) { }

        protected override SingleTextBoxQuestionScoring BuildObject()
        {
            var returnObj = IsInCreationMode() ? new SingleTextBoxQuestionScoring() : OriginalObject;
            returnObj.CorrectAnswers = CorrectAnswers;
            returnObj.IsCaseSensitive = IsCaseSensitive;
            returnObj.MaximumScore = MaximumScore;
            returnObj.Strategy = null;
            return returnObj;
        }


        protected override void InitializeCreateNewObject()
        {
            base.InitializeCreateNewObject();
            CorrectAnswers = new Dictionary<string, float>();
            IsCaseSensitive = false;
        }

        protected override void InitializeEditExistingObject()
        {
            base.InitializeEditExistingObject();
            CorrectAnswers = OriginalObject.CorrectAnswers;
            IsCaseSensitive = OriginalObject.IsCaseSensitive;
            MaximumScore = OriginalObject.MaximumScore;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            var attr = AttributeHelper.GetPropertyAttribute<ISingleTextBoxQuestionScoringEditor, IDictionary<string, float>, CollectionCountAttribute>(x => x.CorrectAnswers, mVersion);
            mMaxAnswers = attr.Max;
            mMinAnswers = attr.Min;

            var attrScore = AttributeHelper.GetPropertyAttribute<ISingleTextBoxQuestionScoringEditor, int, IntegerValueRangeAttribute>(x => x.MaximumScore, mVersion);
            mMaxScore = attrScore.Max;
            mMinScore = attrScore.Min;
        }

        protected override bool Validate()
        {
            var validationPassed = true;

            if (CorrectAnswers == null)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.CorrectAnswers, "Correct answers cannot be null");
                validationPassed = false;
            }
            else
            {
                if (CorrectAnswers.Count < mMinAnswers || CorrectAnswers.Count > mMaxAnswers)
                {
                    ErrorHandlerAdapter.HandleErrorFor(x => x.CorrectAnswers, $"There must be from {mMinAnswers} to {mMaxAnswers} correct answers.");
                    validationPassed = false;
                }
                if (CorrectAnswers.Count != CorrectAnswers.Keys.Select(x => IsCaseSensitive ? x : x.ToLower()).Distinct().Count())
                {
                    ErrorHandlerAdapter.HandleErrorFor(x => x.CorrectAnswers, "More than one identical correct answers found.");
                    validationPassed = false;
                }

                if(CorrectAnswers.Values.Any(x => x < 0 || x > 1))
                {
                    ErrorHandlerAdapter.HandleErrorFor(x => x.CorrectAnswers, "Multiplier cannot be less than 0 and more than one.");
                    validationPassed = false;
                }
            }
            if (MaximumScore > mMaxScore || MaximumScore < mMinScore)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.MaximumScore, $"Maximum points score must be from {mMinScore} to {mMaxScore}.");
                validationPassed = false;
            }

            return validationPassed;
        }
    }
}
