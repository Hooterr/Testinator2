using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Abstractions.Results
{
    public interface ITestResults
    {
        ITest Test { get; set; }

        IDictionary<IUserInfo, IUserTestResult> UserResult { get; set; }

        DateTime Date { get; set; }
    }
}
