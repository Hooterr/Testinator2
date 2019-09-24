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

        protected override void OnInitializing()
        {
            // Create editors
            if (mQuestion == null)
            {
                mOptionsEditor = new MultipleChoiceQuestionOptionsEditor(mVersion, mErrorListiner);
                mScoringEditor = new MultipleChoiceQuestionScoringEditor(mVersion, mErrorListiner);
            }
            else
            {
                mOptionsEditor = new MultipleChoiceQuestionOptionsEditor(mQuestion.Options, mVersion, mErrorListiner);
                mScoringEditor = new MultipleChoiceQuestionScoringEditor(mQuestion.Scoring, mVersion, mErrorListiner);
            }
        }

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

        protected override bool FinalValidation()
        {
            // Important to clear all errors
            // But do this in the top level editor only
            var validationPassed = true;
            if (mScoringEditor.CorrectAnswerIdx < 0 || mScoringEditor.CorrectAnswerIdx + 1 > mOptionsEditor.ABCD.Count)
            {
                mErrorListiner.HandleErrorFor(x => x.Scoring, "Correct answer must be matched to the number of options.");
                validationPassed = false;
            }
            return validationPassed;
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
