using System;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation.Questions
{
    /// <summary>
    /// Basic implementation of a question
    /// </summary>
    [Serializable]
    public abstract class BaseQuestion : IQuestion
    {
        public Guid Id { get; internal set; }

        public IQuestionTask Task { get; internal set; }

        public IQuestionScoring Scoring { get; internal set; }

        public IQuestionOptions Options { get; internal set; }

        public string Author { get; internal set; }

        public Category Category { get; internal set; }

        public int Version { get; internal set; }
    }
}
