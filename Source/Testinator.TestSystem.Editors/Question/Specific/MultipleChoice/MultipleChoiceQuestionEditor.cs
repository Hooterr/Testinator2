using System;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Editor implementation for <see cref="MultipleChoiceQuestion"/>
    /// </summary>
    [EditorFor(typeof(MultipleChoiceQuestion))]
    internal class MultipleChoiceQuestionEditor : BaseQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>
    {
        #region Private Members

        /// <summary>
        /// Concrete options editor for this question
        /// </summary>
        private MultipleChoiceQuestionOptionsEditor mOptionsEditor;

        /// <summary>
        /// Concrete scoring editor for this question
        /// </summary>
        private MultipleChoiceQuestionScoringEditor mScoringEditor;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Editor for options part of the question
        /// </summary>
        public override IMultipleChoiceQuestionOptionsEditor Options => mOptionsEditor;

        /// <summary>
        /// Editor for scoring part of the question
        /// </summary>
        public override IMultipleChoiceQuestionScoringEditor Scoring => mScoringEditor;

        #endregion

        #region Protected Methods

        protected override OperationResult<IQuestionOptions> BuildOptions()
        {
            var optionsBuildResult = mOptionsEditor.Build();
            return OperationResult<IQuestionOptions>.Convert<IQuestionOptions, MultipleChoiceQuestionOptions>(optionsBuildResult);

        }

        protected override OperationResult<IQuestionScoring> BuildScoring()
        {
            var scoringBuildResult = mScoringEditor.Build();
            return OperationResult<IQuestionScoring>.Convert<IQuestionScoring, MultipleChoiceQuestionScoring>(scoringBuildResult);
        }

        protected override bool Validate()
        { 
            var validationPassed = true;
            if (mScoringEditor.CorrectAnswerIdx < 0 || mScoringEditor.CorrectAnswerIdx + 1 > mOptionsEditor.ABCD.Count)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Scoring, "Correct answer must be matched to the number of options.");
                validationPassed = false;
            }
            return validationPassed;
        }
        protected override void CreateNestedEditorExistingObject()
        {
            base.CreateNestedEditorExistingObject();
            mOptionsEditor = new MultipleChoiceQuestionOptionsEditor(OriginalObject.Options, mVersion);
            mScoringEditor = new MultipleChoiceQuestionScoringEditor(OriginalObject.Scoring, mVersion);
        }

        protected override void OnEditorsCreated()
        {
            base.OnEditorsCreated();
            mOptionsEditor.SetInternalErrorHandler(mInternalErrorHandler);
            mOptionsEditor.Initialize();
            mOptionsEditor.SetInternalErrorHandler(mInternalErrorHandler);
            mScoringEditor.Initialize();
        }

        protected override void CreateNestedEditorsNewObject()
        {
            base.CreateNestedEditorsNewObject();
            mOptionsEditor = new MultipleChoiceQuestionOptionsEditor(mVersion);
            mScoringEditor = new MultipleChoiceQuestionScoringEditor(mVersion);
        }

        protected override void CreateHandlers(IInternalErrorHandler internalHandler)
        {
            base.CreateHandlers(internalHandler);
            mOptionsEditor.AttachErrorHandler(internalHandler, nameof(Options));
            mScoringEditor.AttachErrorHandler(internalHandler, nameof(Scoring));
        }

        #endregion

        #region All Constructors

        /// <summary>
        /// Initializes editor to create a new question
        /// </summary>
        /// <param name="version">Question model version to use</param>
        public MultipleChoiceQuestionEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes editor to edit an existing question
        /// </summary>
        /// <param name="question">question to edit</param>
        public MultipleChoiceQuestionEditor(MultipleChoiceQuestion question) : base(question) { } 

        #endregion
    }
}
