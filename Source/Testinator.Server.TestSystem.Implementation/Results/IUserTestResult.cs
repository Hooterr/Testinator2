using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Results;

namespace Testinator.TestSystem.Implementation.Results
{
    public class UserTestResult : IUserTestResult
    {
        public IList<IUserAnswer> Answers { get; set; }
        public int NumberOfPoints { get; set; }
        public IGrade Grade { get; set; }
        public DateTime DayOfTest { get; set; }
        public TimeSpan CompletitionTime { get; set; }
        public IGrading CustomGrading { get; set; }
    }
}
