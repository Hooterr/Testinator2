using System;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    //
    // WORK IN PROGRESS
    //

    public abstract class BaseGradingStrategy : IGradingStrategy
    {
        protected int mMaxPointScore;

        // key: upper limit (inclusive)
        // value : grade
        public SortedList<int, IGrade> PointThresholds { get; private set; }

        public virtual IGrade GetGrade(int pointScore)
        {

            if (PointThresholds == null)
                throw new NullReferenceException($"{nameof(PointThresholds)} were null.");

            if (PointThresholds.Count == 0)
                throw new InvalidOperationException($"{nameof(PointThresholds)} cannot be empty.");

            var grade = PointThresholds[0];

            foreach(var threshold in PointThresholds)
            {
                if (threshold.Key > pointScore)
                    break;

                grade = threshold.Value;
            }

            if (grade == null)
                throw new InvalidOperationException("There was no grade for this point score.");

            return grade;
        }

        public BaseGradingStrategy(int maxPointScore)
        {
            mMaxPointScore = maxPointScore;
            PointThresholds = new SortedList<int, IGrade>();
        }
    }
}
