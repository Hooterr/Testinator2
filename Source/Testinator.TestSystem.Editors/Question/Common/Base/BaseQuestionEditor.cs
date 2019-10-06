using System;
using System.Collections.Generic;
using Testinator.TestSystem.Implementation.Questions;
using Testinator.TestSystem.Abstractions;
using System.Linq.Expressions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Provides base functionality for any question editor
    /// </summary>
    /// <typeparam name="TQuestion">The type of question this editor will be operating on</typeparam>
    /// <typeparam name="TOptionsEditor">The type of editor to use for the options part of the question</typeparam>
    /// <typeparam name="TScoringEditor">The type of editor to use for the scoring part of the question</typeparam>
    internal abstract class BaseQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : MasterEditor<TQuestion, IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>> , IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>
        where TQuestion : BaseQuestion, new()
        where TOptionsEditor : IQuestionOptionsEditor
        where TScoringEditor : IQuestionScoringEditor
    {
        #region Protected Members

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

        public override OperationResult<TQuestion> Build()
        {
            // CUSTOM BUILD PROCESS, BuildObject shouldn't be called

            // Build all the parts of the question
            ErrorHandlerAdapter.Clear();
            var taskBuildOperation = mTaskEditor.Build();
            var optionsBuildOperation = BuildOptions();
            var scoringBuildOperation = BuildScoring();
            var postValidationSuccess = Validate();

            // If any of the operations failed
            if (Helpers.AnyFalse(taskBuildOperation.Succeeded, optionsBuildOperation.Succeeded, scoringBuildOperation.Succeeded, postValidationSuccess))
            {
                // Merge all of the errors together
                var questionBuildResult = OperationResult<TQuestion>.Fail();
                questionBuildResult.Merge(taskBuildOperation)
                                   .Merge(optionsBuildOperation)
                                   .Merge(scoringBuildOperation);
                                    //.AddErrors(GetUnhandledErrors());

                return questionBuildResult;
            }

            TQuestion resultQuestion;
            // Assemble question
            if (OriginalObject == null)
                resultQuestion = new TQuestion()
                {
                    Id = Guid.NewGuid(),
                };
            else
                resultQuestion = OriginalObject;

            
            resultQuestion.Task = taskBuildOperation.Result;
            resultQuestion.Options = optionsBuildOperation.Result;
            resultQuestion.Scoring = scoringBuildOperation.Result;
            resultQuestion.Version = mVersion;

            return OperationResult<TQuestion>.Success(resultQuestion);
        }

        protected override TQuestion BuildObject()
        {
            // BuildObject shouldn't be called
            throw new NotSupportedException();
        }

        public void OnErrorFor(Expression<Func<IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>, object>> propertyExpression, ICollection<string> handler)
        {
            ErrorHandlerAdapter.OnErrorFor(propertyExpression, handler);
        }

        #endregion

        #region All Constructors 

        /// <summary>
        /// Setups up the editor to edit an existing question
        /// </summary>
        /// <param name="question">Question to edit. Passing null will not create a new question but rather throw an exception</param>
        protected BaseQuestionEditor(TQuestion question) : base(question, question.Version) { }

        /// <summary>
        /// Setups up the editor to create a new question using specific version number
        /// </summary>
        /// <param name="version">The version number to use. Must be from within the supported version numbers</param>
        protected BaseQuestionEditor(int version) : base(version) { }

        #endregion

        #region Protected

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

        #endregion

        #region Private Methods

        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override void CreateNestedEditorExistingObject()
        {
            mTaskEditor = new TaskEditor(OriginalObject.Task, mVersion);
        }

        protected override void CreateNestedEditorsNewObject()
        {
            mTaskEditor = new TaskEditor(mVersion);
        }

        protected override void OnEditorsCreated()
        {
            if (mTaskEditor == null)
                throw new NotImplementedException("Task editor has not been initialized.");

            mTaskEditor.SetInternalErrorHandler(mInternalErrorHandler);
            mTaskEditor.Initialize();

            if (Options == null)
                throw new NotImplementedException("Options editor has not been initialized.");

            if (Scoring == null)
                throw new NotSupportedException("Scoring editor has not been initialized.");

        }

        protected override void CreateHandlers(IInternalErrorHandler internalHandler)
        {
            base.CreateHandlers(internalHandler);
            mTaskEditor.AttachErrorHandler(internalHandler, nameof(Task));
        }

        bool IErrorListener<IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>>.Validate()
        {
            return Validate();
        }

        #endregion
    }
}
