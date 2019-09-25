using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    [Serializable]
    internal class PointsGradingStrategy : IGradingStrategy
    {
        public ReadOnlyCollection<KeyValuePair<int, IGrade>> Thresholds { get; internal set; }

        public bool ContainsPoints => true;

        public IGrade GetGrade(int pointScore)
        {
            if (Thresholds == null)
                throw new NullReferenceException($"{nameof(Thresholds)} were null.");

            if (Thresholds.Count == 0)
                throw new InvalidOperationException($"{nameof(Thresholds)} cannot be empty.");

            var grade = Thresholds[0].Value;

            foreach (var threshold in Thresholds)
            {
                if (threshold.Key > pointScore)
                    break;

                grade = threshold.Value;
            }

            if (grade == null)
                throw new InvalidOperationException("There was no grade for this point score.");

            return grade;
        }
    }
}
