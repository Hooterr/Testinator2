using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public abstract class BaseQuestionScoring : IQuestionScoring
    {
        public int MaximumScore { get; internal set; }

        public abstract int CheckAnswer(IUserAnswer answer);
    }
}
