using System.Collections.Generic;
using System.Linq;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The implementation of the options editor for multiple choice question
    /// </summary>
    internal class MultipleCheckBoxesQuestionOptionsEditor : NestedEditor<MultipleCheckBoxesQuestionOptions, IMultipleCheckBoxesQuestionOptionsEditor>, IMultipleCheckBoxesQuestionOptionsEditor
    {
        #region Private Members

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
        /// Options for this question
        /// </summary>
        public List<string> Boxes { get; set; }

        /// <summary>
        /// Maximum allowed amount of options
        /// </summary>
        public int MaximumCount { get; private set; }

        /// <summary>
        /// Minimum required amount of options
        /// </summary>
        public int MinimumCount { get; private set; }

        /// <summary>
        /// Initial amount of options for new question
        /// </summary>
        public int InitialCount => 4; // TODO: Get this from attributes or wherever you want

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes the editor to create new question options
        /// </summary>
        /// <param name="version">The version of question model to use</param>
        public MultipleCheckBoxesQuestionOptionsEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes the editor to edit an existing question options
        /// </summary>
        /// <param name="objToEdit">The options to edit</param>
        /// <param name="version">The version of question model to use</param>
        public MultipleCheckBoxesQuestionOptionsEditor(MultipleCheckBoxesQuestionOptions objToEdit, int version) : base(objToEdit, version) { }

        #endregion

        #region Overridden

        protected override void OnInitialize()
        {
            LoadAttributeValues();
        }

        protected override void InitializeCreateNewObject()
        {
            Boxes = new List<string>();
        }

        protected override void InitializeEditExistingObject()
        {
            Boxes = new List<string>(OriginalObject.Options);
        }

        protected override MultipleCheckBoxesQuestionOptions BuildObject()
        {
            MultipleCheckBoxesQuestionOptions questionOptions;
            if (IsInCreationMode())
            {
                questionOptions = new MultipleCheckBoxesQuestionOptions()
                {
#pragma warning disable IDE0003
                    Options = this.Boxes,
                };
            }
            else
            {
                questionOptions = OriginalObject;
                questionOptions.Options = this.Boxes;
            }

            return questionOptions;
        }

        
        protected override bool Validate()
        {
            var validationPassed = true;

            if (mOnlyDistinct)
            {
                if (Boxes.Count > 1 && (Boxes.Distinct().Count() != Boxes.Count()))
                {
                    ErrorHandlerAdapter.HandleErrorFor(x => x.Boxes, "Options must be unique");
                    validationPassed = false;
                }
            }

            if (Boxes.Count() < MinimumCount || Boxes.Count() > MaximumCount)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Boxes, $"There must be from {MinimumCount} to {MaximumCount} options.");
                validationPassed = false;
            }

            if (!Boxes.All(str => OptionLengthInRange(str)))
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Boxes, $"Every options must be from {mMinOptionLen} to {mMaxOptionLen} characters long.");
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
            var collectionCountAttr = AttributeHelper.GetPropertyAttribute<MultipleCheckBoxesQuestionOptions, List<string>, CollectionCountAttribute>
                (x => x.Options, mVersion);
            MaximumCount = collectionCountAttr.Max;
            MinimumCount = collectionCountAttr.Min;

            mOnlyDistinct = AttributeHelper.GetPropertyAttributeValue<MultipleCheckBoxesQuestionOptions, List<string>, CollectionItemsOnlyDistinctAttribute, bool>
                (x => x.Options, attr => attr.Value, mVersion);

            var stringLenAttr = AttributeHelper.GetPropertyAttribute<MultipleCheckBoxesQuestionOptions, List<string>, StringLengthAttribute>
                    (x => x.Options, mVersion);
            mMaxOptionLen = stringLenAttr.Max;
            mMinOptionLen = stringLenAttr.Min;
        }

        /// <summary>
        /// Checks if the given option's length is in range
        /// </summary>
        /// <param name="option">The options to test</param>
        /// <returns>True if it's in range, otherwise false</returns>
        private bool OptionLengthInRange(string option)
        {
            if (option == null)
                return false;

            return (option.Length >= mMinOptionLen) && (option.Length <= mMaxOptionLen);
        } 

        #endregion
    }
}
