using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Results
{
    public interface IUserTestResult
    {
        IList<IUserAnswer> Answers { get; set; }

        int NumberOfPoints { get; set; }

        IGrade Grade { get; set; }

        DateTime DayOfTest { get; set; }

        TimeSpan CompletitionTime { get; set; }

        IGrading CustomGrading { get; set; }
    }
}
