using System;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.TestSystem.Implementation
{
    [Serializable]
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

        public ITestOptions Options => mTestOptions;

        public int Version { get; internal set; }
    }
}
