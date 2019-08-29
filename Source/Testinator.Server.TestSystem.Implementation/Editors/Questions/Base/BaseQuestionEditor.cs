using System;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Provides base functionality for any question editor
    /// </summary>
    /// <typeparam name="TQuestion">The type of question this editor will be operating on</typeparam>
    /// <typeparam name="TOptionsEditor">The type of editor to use for the options part of the question</typeparam>
    /// <typeparam name="TScoringEditor">The type of editor to use for the scoring part of the question</typeparam>
    internal abstract class BaseQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>
        where TQuestion : BaseQuestion, new()//get rid of this new
        where TOptionsEditor : IOptionsEditor
        where TScoringEditor : IQuestionScoringEditor
    {

        #region Private Members

        /// <summary>
        /// The question we're building/editing
        /// </summary>
        private TQuestion mQuestion;

        /// <summary>
        /// The version of question model we're using
        /// </summary>
        private readonly int mVersion;

        /// <summary>
        /// The editor for task part of the question
        /// </summary>
        private TaskEditor mTaskEditor;

        #endregion

        #region Public Properties

        public ITaskEditor Task => mTaskEditor;

        // These two are question-specific
        public abstract TOptionsEditor Options { get; }
        public abstract TScoringEditor Scoring { get; }

        #endregion

        #region Public Methods

        public OperationResult<TQuestion> Build()
        {
            // Build all the parts of the question
            var taskBuildOperation = mTaskEditor.Build();
            var optionsBuildOperation = BuildOptions();
            var scoringBuildOperation = BuildScoring();

            // If any of the operations failed
            if (Helpers.AnyTrue(taskBuildOperation.Failed, optionsBuildOperation.Failed, scoringBuildOperation.Failed))
            {
                // Merge all of the errors together
                var questionBuildResult = OperationResult<TQuestion>.Fail();
                questionBuildResult.Merge(taskBuildOperation)
                                   .Merge(optionsBuildOperation)
                                   .Merge(scoringBuildOperation);

                return questionBuildResult;
            }

            // Assemble question
            // TODO maybe some additional checks will be required
            mQuestion.Task = taskBuildOperation.Result;
            mQuestion.Options = optionsBuildOperation.Result;
            mQuestion.Scoring = scoringBuildOperation.Result;

            return OperationResult<TQuestion>.Success(mQuestion);
        }

        #endregion

        #region All Constructors 

        /// <summary>
        /// Setups up the editor to edit an existing question
        /// </summary>
        /// <param name="question">Question to edit. Passing null will not create a new question but rather throw an exception</param>
        protected BaseQuestionEditor(TQuestion question)
        {
#pragma warning disable IDE0016 // Use 'throw' expression
            if (question == null)
                throw new ArgumentNullException(nameof(question),
                    "When editing cannot use a null question. If you want to create a new question call the constructor that accepts question model version.");
#pragma warning restore IDE0016 // Use 'throw' expression

            // Set the question and version
            mQuestion = question;
            mVersion = question.Version;

            InitializeEditor();
        }

        /// <summary>
        /// Setups up the editor to create a new question using specific version number
        /// </summary>
        /// <param name="version">The version number to use. Must be from within the supported version numbers</param>
        protected BaseQuestionEditor(int version)
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            mVersion = version;

            InitializeEditor();
        }

        #endregion

        #region Protected

        /// <summary>
        /// Fired when the editor is initializing
        /// In this method editors for options and scoring MUST be created
        /// </summary>
        protected abstract void OnInitializing();

        protected abstract OperationResult<IQuestionOptions> BuildOptions();

        protected abstract OperationResult<IQuestionScoring> BuildScoring();

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs initialization
        /// </summary>
        private void InitializeEditor()
        {
            // Create task editor
            mTaskEditor = new TaskEditor(mQuestion.Version);

            // Let the implementer initialize as well 
            OnInitializing();

            // Check if the implementer initialized editors

            if (Options == null)
                throw new NotSupportedException("Options editor has not been initialized.");

            // TODO uncomment this when scoring editor shall be implemented
            //if (Scoring == null)
            //    throw new NotSupportedException("Options scoring has not been initialized.");
        }

        #endregion
    }
}
