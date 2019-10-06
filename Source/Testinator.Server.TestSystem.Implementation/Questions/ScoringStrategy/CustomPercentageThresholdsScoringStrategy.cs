using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Implementation.Questions.ScoringStrategy
{
    [Serializable]
    public class CustomPercentageThresholdsScoringStrategy : IScoringStrategy
    {
        /// <summary>
        /// The threshold for this strategy in format specified below
        /// Key: percentage of the answer correct from which this step applies (inclusive)
        /// Value: fraction of the points that shall be granted
        /// NOTE: if the list is empty AllCorrectOr0 strategy will be used
        /// </summary>
        public SortedList<int, double> Thresholds { get; }

        public int Evaluate(int maxPointScore, int percentageDoneCorrect)
        {
            if (Thresholds == null || Thresholds.Count == 0)
                return percentageDoneCorrect == 100 ? maxPointScore : 0;

            var scoreMultipler = 0d;

            foreach(var threshold in Thresholds)
            {
                if (threshold.Key <= percentageDoneCorrect)
                    scoreMultipler = threshold.Value;
                else
                    break;
            }

            return (int)Math.Floor(maxPointScore * scoreMultipler);
        }
    }
}
