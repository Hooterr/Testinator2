using System;
using System.Linq;
using System.Reflection;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Configures and builds a question editor 
    /// Hide this implementation and let the client communicate using <see cref="IEditorBuilder{TEditor, TQuestion}"/> interface
    /// </summary>
    /// <typeparam name="TQuestion">The type of question that the editor we're building will be producing</typeparam>
    /// <typeparam name="TEditor">The type of editor to configure and build</typeparam>
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
        /// If null, create a new question
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
            var concreteEditorType = EditorImplementationLocalizer.FindImplementation(typeof(TQuestion));

            TEditor ConcreteEditor;

            // Could've used Activator but for some reason it wasn't able to find the constructor
            if (mQuestion == null)
                // New question is being created
                ConcreteEditor = (TEditor)concreteEditorType.GetConstructor(new[] { typeof(int) }).Invoke(new object[] { mVersion } );
            else
                // We're editing an existing question
                ConcreteEditor = (TEditor)concreteEditorType.GetConstructor(new[] { typeof(TQuestion) }).Invoke(new object[] { mQuestion });

            return ConcreteEditor;
        }

        public IEditorBuilder<TEditor, TQuestion> NewQuestion()
        {
            mQuestion = null;
            // By default use the highest version
            // Can be changed
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
            // There already is a question to edit and the caller wants to change the version
            if (mQuestion != null && mQuestion.Version != version)
            {

                throw new NotSupportedException("Changing question version is not supported yet.");
                // Tho it may be one day
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
            return SetVersion(Versions.Highest);
        }

        #endregion
    }
    
} 