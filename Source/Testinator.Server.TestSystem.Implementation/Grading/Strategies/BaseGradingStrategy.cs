using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    //
    // WORK IN PROGRESS
    //

    public abstract class BaseGradingStrategy : IGradingStrategy
    {
        public virtual bool ThresholdsContainPercentage() => true;

        public SortedList<int, IGrade> Thresholds { get; internal set; }

        public virtual IGrade GetGrade(int pointScore, int maxPointScore)
        {
            var percentageCorrect = (double)pointScore / maxPointScore * 100;

            IGrade grade = null;

            if (Thresholds == null)
                throw new NullReferenceException($"{nameof(Thresholds)} were null.");

            if (Thresholds.Count == 0)
                throw new InvalidOperationException($"{nameof(Thresholds)} cannot be empty.");

            foreach(var threshold in Thresholds)
            {
                if (threshold.Key > percentageCorrect)
                    break;

                grade = threshold.Value;
            }

            if (grade == null)
                throw new InvalidOperationException("There was no grade for this point score.");

            return grade;
        }
    }
}
