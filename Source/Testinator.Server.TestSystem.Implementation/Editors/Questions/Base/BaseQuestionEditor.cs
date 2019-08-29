using System;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class BaseQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> : IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor>
        where TQuestion : BaseQuestion, new()//get rid of this new
        where TOptionsEditor : IOptionsEditor
        where TScoringEditor : IQuestionScoringEditor
    {
        /// <summary>
        /// The question we're building/editing
        /// </summary>
        private TQuestion mQuestion;

        private TaskEditor mTask;

        /// <summary>
        /// The editor for the task part of the question
        /// </summary>
        public ITaskEditor Task => mTask;

        public abstract TOptionsEditor Options { get; }

        public abstract TScoringEditor Scoring { get; }

        public OperationResult<TQuestion> Build()
        {
            var taskBuildOperation = mTask.Build();
            if (taskBuildOperation.Failed)
            {
                var questionBuildResult = OperationResult<TQuestion>.Fail();
                questionBuildResult.Merge(taskBuildOperation);
                return questionBuildResult;
            }

            mQuestion.Task = taskBuildOperation.Result;

            return OperationResult<TQuestion>.Success(mQuestion);
        }

        protected abstract void InitializeEditor();

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

            mQuestion = question;
            // Create task editor
            mTask = new TaskEditor(mQuestion.Version);

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

            // Set up a new question

            // TODO use a factory method
            mQuestion = new TQuestion()
            {
                Version = version
            };

            // Create task editor
            mTask = new TaskEditor(mQuestion.Version);

            InitializeEditor();
        }

        #endregion
    }
}
