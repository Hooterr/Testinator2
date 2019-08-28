using System;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Configures and builds a question editor 
    /// Hide this implementation and let the client communicate using <see cref="IEditorBuilder{TEditor, TQuestion}"/> interface
    /// </summary>
    /// NOTE: there are many generic parameters with many constrains but it is easier to do that once instead of 
    /// spending time (more importantly runtime time) get all of that information through reflection
    /// <typeparam name="TEditorImpl">Implementation of the editor that is being built</typeparam>
    /// <typeparam name="TEditorInterface">Interface behind which the implementation is hidden</typeparam>
    /// <typeparam name="TQuestion">The type of question that the editor we're building will be producing</typeparam>
    internal class EditorBuilder<TEditor, TQuestion> : IEditorBuilder<TEditor, TQuestion>
        where TQuestion : BaseQuestion
    {

        #region Private Members

        /// <summary>
        /// Question model version to use
        /// </summary>
        private int mVersion;

        /// <summary>
        /// The question to edit
        /// If null create a new question
        /// </summary>
        private TQuestion mQuestion;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditorBuilder()
        {
            // Setup new question at the start
            NewQuestion();
        }

        #endregion

        #region Public Builder Methods

        public TEditor Build()
        {

            var genericTypes = typeof(TEditor).GetGenericArguments();
            var concreteEditorType = typeof(QuestionEditor<,,>).MakeGenericType(genericTypes);

            object parameter;

            if (mQuestion == null)
                // New question is being created
                parameter = mVersion;
            else
                // We're editing an existing question
                parameter = mQuestion;

            var ConcreteEditor = (TEditor)Activator.CreateInstance(concreteEditorType, parameter);

            return ConcreteEditor;
        }

        public IEditorBuilder<TEditor, TQuestion> NewQuestion()
        {
            mQuestion = null;
            mVersion = Versions.Highest;
            return this;
        }

        public IEditorBuilder<TEditor, TQuestion> SetInitialQuestion(TQuestion question)
        {
            if (question == null)
                return NewQuestion();

            mQuestion = question;
            mVersion = question.Version;

            return this;
        }

        public IEditorBuilder<TEditor, TQuestion> SetVersion(int version)
        {
            if (mQuestion != null && mQuestion.Version != version)
            {
                throw new NotSupportedException("Changing question version is not supported yet.");
            }

            if (Versions.NotInRange(version))
            {
                throw new ArgumentException($"Version must be between highest ({Versions.Highest}) and lowest ({Versions.Lowest}).");
            }

            mVersion = version;

            return this;
        }

        public IEditorBuilder<TEditor, TQuestion> UseNewestVersion()
        {
            if (mQuestion != null && mQuestion.Version != Versions.Highest)
            {
                throw new NotSupportedException("Changing question version is not supported yet.");
            }
            else
                mVersion = Versions.Highest;

            return this;
        }

        #endregion
    }
    
}