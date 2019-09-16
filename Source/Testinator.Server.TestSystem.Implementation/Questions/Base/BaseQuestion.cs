using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.TestSystem.Implementation.Questions
{
    /// <summary>
    /// Basic implementation of a question
    /// </summary>
    public abstract class BaseQuestion : IQuestion
    {
        public IQuestionTask Task { get; internal set; }

        public IQuestionScoring Scoring { get; internal set; }

        public IQuestionOptions Options { get; internal set; }

        public string Author { get; internal set; }

        public Category Category { get; internal set; }

        public int Version { get; internal set; }
    }
}
