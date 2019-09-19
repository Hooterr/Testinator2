using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    public class PercentageGradingStrategy : IGradingStrategy
    {
        public ReadOnlyCollection<KeyValuePair<int, IGrade>> Thresholds { get; internal set; }

        public bool ContainsPoints => false;

        internal int MaxPointScore { get; set; }

        public IGrade GetGrade(int pointScore)
        {
            var percentageComplete = (int)(pointScore / (double)MaxPointScore * 100);

            if (Thresholds == null)
                throw new NullReferenceException($"{nameof(Thresholds)} were null.");

            if (Thresholds.Count == 0)
                throw new InvalidOperationException($"{nameof(Thresholds)} cannot be empty.");

            var grade = Thresholds[0].Value;

            foreach (var threshold in Thresholds)
            {
                if (threshold.Key > percentageComplete)
                    break;

                grade = threshold.Value;
            }

            if (grade == null)
                throw new InvalidOperationException("There was no grade for this point score.");

            return grade;
        }
    }
}
