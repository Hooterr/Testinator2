using System;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.Server.TestSystem.Implementation
{
    public class Test : ITest
    {
        #region Internal Members

        internal List<IQuestionProvider> mQuestionList;
        internal TestOptions mTestOptions;
        internal Grading mGrading;
        internal TestInfo mInfo;

        #endregion

        public ITestInfo Info => mInfo;
      

        public IGrading Grading => mGrading;

        public IList<IQuestionProvider> Questions => mQuestionList;

        public TimeSpan CompletionTime { get; internal set; }

        public ITestOptions Options => mTestOptions;

    }
}
