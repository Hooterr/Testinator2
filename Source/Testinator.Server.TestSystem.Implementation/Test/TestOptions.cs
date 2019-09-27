using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    [Serializable]
    public class TestOptions : ITestOptions
    {
        public bool ShouldUseAllQuestions { get; internal set; }
    }
}
