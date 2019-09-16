using System;
using System.Collections.Generic;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Provides base functionality for any question editor
    /// </summary>
    /// <typeparam name="TQuestion">The type of question this editor will be operating on</typeparam>
    /// <typeparam name="TOptionsEditor">The type of editor to use for the options part of the question</typeparam>
    /// <typeparam name="TScoringEditor">The type of editor to use for the scoring part of the question</typeparam>
    internal abstract class BaseQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : ErrorListener<IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>>,                                                                                                    IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>
        where TQuestion : BaseQuestion, new()
        where TOptionsEditor : IQuestionOptionsEditor
        where TScoringEditor : IQuestionScoringEditor
    {
        #region Protected Members

        /// <summary>
        /// The question we're building/editing
        /// </summary>
        protected TQuestion mQuestion;

        /// <summary>
        /// The version of question model we're using
        /// </summary>
        protected readonly int mVersion;

        /// <summary>
        /// The editor for task part of the question
        /// </summary>
        protected TaskEditor mTaskEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The editor for the task part of the question
        /// </summary>
        public ITaskEditor Task => mTaskEditor;

        /// <summary>
        /// The editor for the options part of the question
        /// </summary>
        public abstract TOptionsEditor Options { get; }

        /// <summary>
        /// The editor for the scoring part of the question
        /// </summary>
        public abstract TScoringEditor Scoring { get; }

        #endregion

        #region Public Methods

        public OperationResult<TQuestion> Build()
        {
            // Build all the parts of the question
            var taskBuildOperation = mTaskEditor.Build();
            var optionsBuildOperation = BuildOptions();
            var scoringBuildOperation = BuildScoring();
            var postValidationSuccess = FinalValidation();

            // If any of the operations failed
            if (Helpers.AnyFalse(taskBuildOperation.Succeeded, optionsBuildOperation.Succeeded, scoringBuildOperation.Succeeded, postValidationSuccess))
            {
                // Merge all of the errors together
                var questionBuildResult = OperationResult<TQuestion>.Fail();
                questionBuildResult.Merge(taskBuildOperation)
                                   .Merge(optionsBuildOperation)
                                   .Merge(scoringBuildOperation)
                                   .AddErrors(GetUnhandledErrors());

                return questionBuildResult;
            }

            // Assemble question
            if(mQuestion == null)
            {
                mQuestion = new TQuestion();
            }

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
        /// Called when the editor is initializing
        /// In this method editors for options and scoring MUST be created
        /// </summary>
        protected abstract void OnInitializing();

        /// <summary>
        /// Builds the options for the question
        /// </summary>
        /// <returns>The result of the operation</returns>
        protected abstract OperationResult<IQuestionOptions> BuildOptions();

        /// <summary>
        /// Builds the scoring for the question
        /// </summary>
        /// <returns>The result of the operation</returns>
        protected abstract OperationResult<IQuestionScoring> BuildScoring();

        /// <summary>
        /// Final validation for the question
        /// Called when task, options and scoring are built successfully 
        /// </summary>
        /// <returns>True if the validation passed, otherwise false</returns>
        protected virtual bool FinalValidation() => true;

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs initialization
        /// </summary>
        private void InitializeEditor()
        {
            // Create task editor
            if (mQuestion == null)

                mTaskEditor = new TaskEditor(mVersion);
            else
                mTaskEditor = new TaskEditor(mQuestion.Task, mVersion);

            // Let the implementer initialize as well 
            OnInitializing();

            // Check if the implementer initialized all the editors
            if (Options == null)
                throw new NotImplementedException("Options editor has not been initialized.");

            if (Scoring == null)
                throw new NotSupportedException("Options scoring has not been initialized.");

            if(HasHandlerFor(x => x.Task) == false)
            {
                mTaskEditor.OnUnhandledError((msg) => HandleErrorFor(x => x.Task, msg));
            }

            if (HasHandlerFor(x => x.Scoring) == false)
            {
                mTaskEditor.OnUnhandledError((msg) => HandleErrorFor(x => x.Scoring, msg));
            }

            if (HasHandlerFor(x => x.Options) == false)
            {
                mTaskEditor.OnUnhandledError((msg) => HandleErrorFor(x => x.Options, msg));
            }
        }

        #endregion
    }
}
