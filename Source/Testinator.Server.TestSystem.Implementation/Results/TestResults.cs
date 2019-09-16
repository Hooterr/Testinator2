using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions.Results;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.TestSystem.Implementation.Results
{
    public class TestResults : ITestResults
    {
        public ITest Test { get; set; }
        public IDictionary<IUserInfo, IUserTestResult> UserResult { get; set; }
        public DateTime Date { get; set; }
    }
}
