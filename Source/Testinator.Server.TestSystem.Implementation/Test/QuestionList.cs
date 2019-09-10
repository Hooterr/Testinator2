using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public class QuestionList : IQuestionList
    {
        public ICollection<IQuestionProvider> Questions { get; internal set; }
    }
}
