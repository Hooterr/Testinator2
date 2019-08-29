using System;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Editor implementation for <see cref="MultipleChoiceQuestion"/>
    /// </summary>
    [ConcreteEditorFor(typeof(MultipleChoiceQuestion))]
    internal class MultipleChoiceQuestionEditor : BaseQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IQuestionScoringEditor>
    {
        #region Private Members

        /// <summary>
        /// Concrete options editor for this question
        /// </summary>
        private MultipleChoiceQuestionOptionsEditor mOptionsEditor;

        /// <summary>
        /// Concrete scoring editor for this question
        /// </summary>
        //private TBD mScoringEditor;

        #endregion

        #region Public Properties

        public override IMultipleChoiceQuestionOptionsEditor Options => mOptionsEditor;

        public override IQuestionScoringEditor Scoring => throw new NotImplementedException();

        #endregion

        #region Protected Methods

        protected override void OnInitializing()
        {
            // Create editors
            if (mQuestion == null)
                mOptionsEditor = new MultipleChoiceQuestionOptionsEditor(mVersion);
            else
                mOptionsEditor = new MultipleChoiceQuestionOptionsEditor(mQuestion.Options, mVersion);

            // TODO implement scoring editor
        }

        protected override OperationResult<IQuestionOptions> BuildOptions()
        {
            var optionsBuildResult = mOptionsEditor.Build();
            return OperationResult<IQuestionOptions>.Convert<IQuestionOptions, MultipleChoiceQuestionOptions>(optionsBuildResult);

        }

        protected override OperationResult<IQuestionScoring> BuildScoring()
        {
            throw new NotImplementedException();
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
