using System;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public class Test : ITest
    {
        #region Internal Members

        internal TestInfo mTestInfo;
        internal QuestionList mQuestionList;
        internal TestOptions mTestOptions;
        internal Grading mGrading;

        #endregion

        public ITestInfo TestInfo => mTestInfo;

        public DateTime CreationDate { get; internal set; }

        public DateTime LastEditionDate { get; internal set; }

        public Category Category { get; internal set; }

        public IGrading Grading => mGrading;

        public IQuestionList Questions => mQuestionList;

        public TimeSpan CompletionTime { get; internal set; }

        public ITestOptions Options => mTestOptions;
    }
}
