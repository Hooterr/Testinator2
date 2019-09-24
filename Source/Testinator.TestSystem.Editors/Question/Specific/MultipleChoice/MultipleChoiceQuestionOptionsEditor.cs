using System.Collections.Generic;
using System.Linq;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The implementation of the options editor for multiple choice question
    /// </summary>
    internal class MultipleChoiceQuestionOptionsEditor : NestedEditor<MultipleChoiceQuestionOptions, IMultipleChoiceQuestionOptionsEditor>, IMultipleChoiceQuestionOptionsEditor
    {
        #region Private Members

        /// <summary>
        /// Maximum count of options
        /// </summary>
        private int mMaxCount;
        
        /// <summary>
        /// Minimum count of options
        /// </summary>
        private int mMinCount;

        /// <summary>
        /// Maximum single option length
        /// </summary>
        private int mMaxOptionLen;

        /// <summary>
        /// Minimum single option length
        /// </summary>
        private int mMinOptionLen;

        /// <summary>
        /// Whether or not allow only distinct options
        /// </summary>
        private bool mOnlyDistinct;

        #endregion

        #region Public Properties

        /// <summary>
        /// ABC options for this question
        /// </summary>
        public List<string> ABCD { get; set; }

        #endregion

        #region Public Methods
        
        public int GetMaximumCount()
        {
            return mMaxCount;
        }

        public int GetMinimumCount()
        {
            return mMinCount;
        }
        public void SetOptions(params string[] options)
        {
            ABCD.Clear();
            ABCD.AddRange(options);
        }

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes the editor to create new question options
        /// </summary>
        /// <param name="version">The version of question model to use</param>
        public MultipleChoiceQuestionOptionsEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes the editor to edit an existing question options
        /// </summary>
        /// <param name="objToEdit">The options to edit</param>
        /// <param name="version">The version of question model to use</param>
        public MultipleChoiceQuestionOptionsEditor(MultipleChoiceQuestionOptions objToEdit, int version) : base(objToEdit, version) { }

        #endregion

        #region Overridden

        protected override void OnInitialize()
        {
            LoadAttributeValues();
        }

        protected override void InitializeCreateNewObject()
        {
            ABCD = new List<string>();

        }

        protected override void InitializeEditExistingObject()
        {
            ABCD = new List<string>(OriginalObject.Options);
        }

        protected override MultipleChoiceQuestionOptions BuildObject()
        {
            MultipleChoiceQuestionOptions questionOptions;
            if (IsInCreationMode())
            {
                questionOptions = new MultipleChoiceQuestionOptions()
                {
#pragma warning disable IDE0003
                    Options = this.ABCD,
                };
            }
            else
            {
                questionOptions = OriginalObject;
                questionOptions.Options = this.ABCD;
            }

            return questionOptions;
        }

        
        protected override bool Validate()
        {
            var validationPassed = true;

            // Delete last options that are null or empty
            ABCD.RemoveAllLast(x => string.IsNullOrEmpty(x));

            if (mOnlyDistinct)
            {
                if (ABCD.Count > 1 && (ABCD.Distinct().Count() != ABCD.Count()))
                {
                    ErrorHandlerAdapter.HandleErrorFor(x => x.ABCD, "Options must be unique");
                    validationPassed = false;
                }
            }

            if (ABCD.Count() < mMinCount || ABCD.Count() > mMaxCount)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.ABCD, $"There must be from {mMinCount} to {mMaxCount} options.");
                validationPassed = false;
            }

            if (!ABCD.All(str => OptionsLengthInRange(str)))
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.ABCD, $"Every options must be from {mMinOptionLen} to {mMaxOptionLen} characters long.");
                validationPassed = false;
            }

            return validationPassed;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the values from the attributes
        /// </summary>
        private void LoadAttributeValues()
        {
            var collectionCountAttr = AttributeHelper.GetPropertyAttribute<MultipleChoiceQuestionOptions, List<string>, CollectionCountAttribute>
                (x => x.Options, mVersion);
            mMaxCount = collectionCountAttr.Max;
            mMinCount = collectionCountAttr.Min;

            mOnlyDistinct = AttributeHelper.GetPropertyAttributeValue<MultipleChoiceQuestionOptions, List<string>, CollectionItemsOnlyDistinctAttribute, bool>
                (x => x.Options, attr => attr.Value, mVersion);

            var stringLenAttr = AttributeHelper.GetPropertyAttribute<MultipleChoiceQuestionOptions, List<string>, StringLengthAttribute>
                    (x => x.Options, mVersion);
            mMaxOptionLen = stringLenAttr.Max;
            mMinOptionLen = stringLenAttr.Min;
        }

        /// <summary>
        /// Checks if the given option's length is in range
        /// </summary>
        /// <param name="option">The options to test</param>
        /// <returns>True if it's in range, otherwise false</returns>
        private bool OptionsLengthInRange(string option)
        {
            if (option == null)
                return false;

            return (option.Length >= mMinOptionLen) && (option.Length <= mMaxOptionLen);
        } 

        #endregion
    }
}
