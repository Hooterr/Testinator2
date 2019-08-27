using System;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Base question editor
    /// </summary>
    /// <typeparam name="TQuestion">The type of question this editor will operate on</typeparam>
    internal abstract class BaseQuestionEditor<TQuestion> : IQuestionEditor<TQuestion>
        where TQuestion : BaseQuestion, new()
    {

        #region Private Members

        /// <summary>
        /// The question we're building/editing
        /// </summary>
        private TQuestion mQuestion;

        #endregion

        #region Public Properties

        /// <summary>
        /// The editor for the task part of the question
        /// </summary>
        public ITaskEditor Task { get; }

        #endregion

        #region All constructors

        /// <summary>
        /// Always forward initialization to this constructor
        /// </summary>
        private BaseQuestionEditor()
        {
            // Create task editor
            Task = new TaskEditor(mQuestion.Version);
        }
        
        /// <summary>
        /// Setups up the editor to edit an existing question
        /// </summary>
        /// <param name="question">Question to edit. Passing null will not create a new question but rather throw an exception</param>
        internal BaseQuestionEditor(TQuestion question) : this()
        {
#pragma warning disable IDE0016 // Use 'throw' expression
            if (question == null)
                throw new ArgumentNullException(nameof(question), 
                    "When editing cannot use a null question. If you want to create a new question call the constructor that accepts question model version.");
#pragma warning restore IDE0016 // Use 'throw' expression

            mQuestion = question;
        }

        /// <summary>
        /// Setups up the editor to create a new question using specific version number
        /// </summary>
        /// <param name="version">The version number to use. Must be from within the supported version numbers</param>
        internal BaseQuestionEditor(int version) : this()
        {
            if (Versions.NotInRange(version))
                throw new ArgumentOutOfRangeException(nameof(version), "Version must be from within the range.");

            // Set up a new question
            mQuestion = new TQuestion()
            {
                Version = version 
            };
        }

        #endregion

        public TQuestion Build()
        {
            Task.Build();
            throw new NotImplementedException();
        }

    }
}
