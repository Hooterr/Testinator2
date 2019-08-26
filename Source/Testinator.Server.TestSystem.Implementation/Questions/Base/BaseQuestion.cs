using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.Server.TestSystem.Implementation.Questions
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

        protected BaseQuestion(IQuestionTask task, IQuestionScoring scoring, IQuestionOptions options, string author, Category category, int version)
        {
            Task = task;
            Scoring = scoring;
            Options = options;
            Author = author;
            Category = category;
            Version = version;
        }
    }
}
