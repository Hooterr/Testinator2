using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.Server.TestSystem.Implementation.Questions.Base
{
    /// <summary>
    /// Basic implementation of a question
    /// </summary>
    internal abstract class BaseQuestion : IQuestion
    {
        public IQuestionTask Task => throw new NotImplementedException();

        public IQuestionScoring Scoring => throw new NotImplementedException();

        public IQuestionOptions Options => throw new NotImplementedException();

        public string Author => throw new NotImplementedException();

        public Category Category => throw new NotImplementedException();

        public int Version => throw new NotImplementedException();
    }
}
