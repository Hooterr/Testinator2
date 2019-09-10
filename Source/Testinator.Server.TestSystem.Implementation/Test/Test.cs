using System;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.Server.TestSystem.Implementation
{
    public class Test : ITest
    {
        #region Internal Members

        internal QuestionList mQuestionList;
        internal TestOptions mTestOptions;
        internal Grading mGrading;

        #endregion

        public string Name { get; internal set; }

        public DateTime CreationDate { get; internal set; }

        public DateTime LastEditionDate { get; internal set; }

        public Category Category { get; internal set; }

        public IGrading Grading => mGrading;

        public IQuestionList Questions => mQuestionList;

        public TimeSpan CompletionTime { get; internal set; }

        public ITestOptions Options => mTestOptions;
    }
}
