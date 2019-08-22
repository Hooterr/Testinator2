using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class BaseEditor<TQuestion>
        where TQuestion : Question, new()
    {

        internal TQuestion mQuestion;
        private ITaskEditor mTaskEditor;


        public int Version => mQuestion == null ? -1 : mQuestion.Version;

        public ITaskEditor Task => mTaskEditor;

        internal void CompleteSetup()
        {
            mTaskEditor = new TaskEditor(Version);
        }

        internal void EditExisting(TQuestion question)
        {
            mQuestion = question ?? throw new NullReferenceException("Cannot edit null question.");       
        }

        internal void CreateNew(int version)
        {
            mQuestion = new TQuestion
            {
                Version = version 
            };
        }
    }
}
