using System;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Editor implementation for <see cref="MultipleChoiceQuestion"/>
    /// </summary>
    [EditorFor(typeof(MultipleCheckBoxesQuestion))]
    internal class MultipleCheckBoxesQuestionEditor : BaseQuestionEditor<MultipleCheckBoxesQuestion, IMultipleCheckBoxesQuestionOptionsEditor, IMultipleCheckBoxesQuestionScoringEditor>
    {
        #region Private Members

        /// <summary>
        /// Concrete options editor for this question
        /// </summary>
        private MultipleCheckBoxesQuestionOptionsEditor mOptionsEditor;

        /// <summary>
        /// Concrete scoring editor for this question
        /// </summary>
        private MultipleCheckBoxesQuestionScoringEditor mScoringEditor;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Editor for options part of the question
        /// </summary>
        public override IMultipleCheckBoxesQuestionOptionsEditor Options => mOptionsEditor;

        /// <summary>
        /// Editor for scoring part of the question
        /// </summary>
        public override IMultipleCheckBoxesQuestionScoringEditor Scoring => mScoringEditor;

        #endregion

        #region Protected Methods

        protected override OperationResult<IQuestionOptions> BuildOptions()
        {
            var optionsBuildResult = mOptionsEditor.Build();
            return OperationResult<IQuestionOptions>.Convert<IQuestionOptions, MultipleCheckBoxesQuestionOptions>(optionsBuildResult);

        }

        protected override OperationResult<IQuestionScoring> BuildScoring()
        {
            var scoringBuildResult = mScoringEditor.Build();
            return OperationResult<IQuestionScoring>.Convert<IQuestionScoring, MultipleCheckBoxesQuestionScoring>(scoringBuildResult);
        }

        protected override bool Validate()
        { 
            var validationPassed = true;
            if (mScoringEditor.CorrectAnswers == null)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Scoring.CorrectAnswers, "Correct answers list cannot be null.");
                validationPassed = false;
            }
            else if (mScoringEditor.CorrectAnswers.Count != mOptionsEditor.Boxes.Count)
            {
                ErrorHandlerAdapter.HandleErrorFor(x => x.Scoring.CorrectAnswers, "Correct answer must be matched to the number of options.");
                validationPassed = false;
            }

            return validationPassed;
        }
        protected override void CreateNestedEditorExistingObject()
        {
            mOptionsEditor = new MultipleCheckBoxesQuestionOptionsEditor(OriginalObject.Options, mVersion);
            mScoringEditor = new MultipleCheckBoxesQuestionScoringEditor(OriginalObject.Scoring, mVersion);
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
            mOptionsEditor = new MultipleCheckBoxesQuestionOptionsEditor(mVersion);
            mScoringEditor = new MultipleCheckBoxesQuestionScoringEditor(mVersion);
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
        public MultipleCheckBoxesQuestionEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes editor to edit an existing question
        /// </summary>
        /// <param name="question">question to edit</param>
        public MultipleCheckBoxesQuestionEditor(MultipleCheckBoxesQuestion question) : base(question) { } 

        #endregion
    }
}
