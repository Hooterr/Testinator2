using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    public class TestOptions : ITestOptions
    {
        public bool ShouldUseAllQuestions { get; internal set; }
    }
}
