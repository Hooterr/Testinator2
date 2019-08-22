using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class EditorBuilder<TEditorImpl, TEditorInterface, TQuestion> : IEditorBuilder<TEditorInterface, TQuestion>
        where TQuestion : Question, new()
        where TEditorImpl : BaseEditor<TQuestion>, TEditorInterface, new()
    {

        #region Private Members

        private TEditorImpl mConcreteEditor;
        private int mVersion;
        private TQuestion mQuestion;

        #endregion

        #region Constructors

        internal EditorBuilder()
        {
            // Setup new question at the start
            NewQuestion();
        }

        #endregion

        #region Public Builder Methods

        public TEditorInterface Build()
        {
            mConcreteEditor = new TEditorImpl();
            // New question is being created
            if (mQuestion == null)
                mConcreteEditor.CreateNew(mVersion);
            else
                mConcreteEditor.EditExisting(mQuestion);

            mConcreteEditor.CompleteSetup();

            return mConcreteEditor;
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
            if (mConcreteEditor.mQuestion != null && mQuestion.Version != version)
            {
                throw new NotSupportedException("Changing question version is not supported yet.");
            }

            if (Versions.NotInRange(version))
            {
                throw new NotSupportedException($"Version must be between highest ({Versions.Highest}) and lowest({Versions.Lowest}).");
            }

            mVersion = version;

            return this;
        }

        public IEditorBuilder<TEditorInterface, TQuestion> UseNewestVersion()
        {
            if (mConcreteEditor.mQuestion != null && mQuestion.Version != Versions.Highest)
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