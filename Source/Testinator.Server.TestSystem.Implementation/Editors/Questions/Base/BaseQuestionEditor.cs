using System;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class BaseQuestionEditor<TQuestion> : IQuestionEditor<TQuestion>
        where TQuestion : BaseQuestion
    {

        private TQuestion mQuestion;
        protected TaskEditor mTaskEditor;


        public int Version => mQuestion == null ? -1 : mQuestion.Version;

        public ITaskEditor Task => throw new NotImplementedException();


        internal void EditExisting(TQuestion question)
        {
            mQuestion = question ?? throw new NullReferenceException("Cannot edit null question.");       
        }

        internal void CreateNew(int version)
        {
            // TODO
        }

        public TQuestion Build()
        {
            throw new NotImplementedException();
        }
    }
}
