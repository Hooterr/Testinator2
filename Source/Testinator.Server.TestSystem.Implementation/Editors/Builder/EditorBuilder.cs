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
    internal class EditorBuilder<TEditorImpl, TEditorInterface, TQuestion> : IEditorBuilder<TEditorInterface, TQuestion>
        where TQuestion : BaseQuestion
        where TEditorInterface : IQuestionEditor<TQuestion>
        where TEditorImpl : BaseEditor<TQuestion>, TEditorInterface, new()
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
        internal EditorBuilder()
        {
            // Setup new question at the start
            NewQuestion();
        }

        #endregion

        #region Public Builder Methods

        public TEditorInterface Build()
        {
            var ConcreteEditor = new TEditorImpl();

            // New question is being created
            if (mQuestion == null)
                ConcreteEditor.CreateNew(mVersion);
            else
                ConcreteEditor.EditExisting(mQuestion);

            return ConcreteEditor;
        }

        public IEditorBuilder<TEditorInterface, TQuestion> NewQuestion()
        {
            mQuestion = null;
            mVersion = Versions.Highest;
            return this;
        }

        public IEditorBuilder<TEditorInterface, TQuestion> SetInitialQuestion(TQuestion question)
        {
            if (question == null)
                return NewQuestion();

            mQuestion = question;
            mVersion = question.Version;

            return this;
        }

        public IEditorBuilder<TEditorInterface, TQuestion> SetVersion(int version)
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

        public IEditorBuilder<TEditorInterface, TQuestion> UseNewestVersion()
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