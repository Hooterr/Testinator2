using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.TestSystem.Implementation
{
    [Serializable]
    public class InMemoryQuestionProvider : IQuestionProvider
    {
        private IQuestion mQuestion;

        public IQuestion GetQuestion()
        {
            return mQuestion;
        }

        public InMemoryQuestionProvider(IQuestion question)
        {
            mQuestion = question;
        }
    }
}
