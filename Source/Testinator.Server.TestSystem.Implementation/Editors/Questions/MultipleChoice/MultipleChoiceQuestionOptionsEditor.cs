using System;
using System.Collections.Generic;
using System.Linq;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class MultipleChoiceQuestionOptionsEditor : BaseEditor<MultipleChoiceQuestionOptions, IMultipleChoiceQuestionOptionsEditor>, IMultipleChoiceQuestionOptionsEditor
    {
        private int mMaxCount;
        private int mMinCount;
        private int mMaxOptionLen;
        private int mMinOptionLen;
        private bool mOnlyDistinct;

        public List<string> Options { get; set; }

        public int GetMaximumCount()
        {
            return mMaxCount;
        }

        protected override void OnInitialize()
        {
            if (IsInCreationMode())
                Options = new List<string>();
            else
                Options = new List<string>(OriginalObject.Options);

            var collectionCountAttr = AttributeHelper.GetPropertyAttribute<MultipleChoiceQuestionOptions, List<string>, CollectionCountAttribute>
                (x => x.Options, Version);
            mMaxCount = collectionCountAttr.Max;
            mMinCount = collectionCountAttr.Min;

            mOnlyDistinct = AttributeHelper.GetPropertyAttributeValue<MultipleChoiceQuestionOptions, List<string>, CollectionItemsOnlyDistinctAttribute, bool>
                (x => x.Options, attr => attr.Value, Version);

            var stringLenAttr = AttributeHelper.GetPropertyAttribute<MultipleChoiceQuestionOptions, List<string>, StringLengthAttribute>
                    (x => x.Options, Version);
            mMaxOptionLen = stringLenAttr.Max;
            mMinOptionLen = stringLenAttr.Min;
        }

        internal override bool Validate()
        {
            var validationPassed = true;
            if (mOnlyDistinct)
            {
                if (Options.Distinct().Count() != Options.Count())
                {
                    HandleErrorFor(x => x.Options, "Options must be unique");
                    validationPassed = false;
                }
            }

            if (Options.Count() < mMinCount || Options.Count() > mMaxCount)
            {
                HandleErrorFor(x => x.Options, $"There must be from {mMinCount} to {mMaxCount} options.");
                validationPassed = false;
            }

            if (!Options.All(str => (str.Length >= mMinOptionLen) && (str.Length <= mMaxOptionLen)))
            {
                HandleErrorFor(x => x.Options, $"Every options must be from {mMinOptionLen} to {mMaxOptionLen} characters long.");
                validationPassed = false;
            }

            return validationPassed;
        }

        protected override MultipleChoiceQuestionOptions BuildObject()
        {
            MultipleChoiceQuestionOptions questionOptions;
            if (IsInCreationMode())
            {
                questionOptions = new MultipleChoiceQuestionOptions()
                {
                    Options = this.Options,
                };
            }
            else
            {
                questionOptions = OriginalObject;
                questionOptions.Options = this.Options;
            }

            return questionOptions;
        }

        public void SetOptions(params string[] options)
        {
            Options.Clear();
            Options.AddRange(options);
        }

        public MultipleChoiceQuestionOptionsEditor(int version) : base(version) { }

        public MultipleChoiceQuestionOptionsEditor(MultipleChoiceQuestionOptions objToEdit, int version) : base(objToEdit, version) { }
    }
}
