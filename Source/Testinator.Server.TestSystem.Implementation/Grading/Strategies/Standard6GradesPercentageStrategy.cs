using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public class Standard6GradesPercentageStrategy : BaseGradingStrategy
    {
        public SortedList<int, IGrade> PercentageThresholds { get; private set; }

        public Standard6GradesPercentageStrategy(int maxPointScore) : base(maxPointScore)
        {
            PercentageThresholds = new SortedList<int, IGrade>()
            {
                { 30,  new Grade("ndst") },
                { 50,  new Grade("dop") },
                { 60,  new Grade("dst") },
                { 75,  new Grade("db") },
                { 90,  new Grade("bdb") },
                { 100,  new Grade("cel") },
                // Or something... 
            };

            ConvertPercentageToPoints();
        }

        public void ConvertPercentageToPoints()
        {
            PointThresholds.Clear();
            foreach(var percentageThreshold in PercentageThresholds)
            {
                // That will probably need some tweaking
                var points = (int)(percentageThreshold.Key / 100D * mMaxPointScore);

                PointThresholds.Add(points, percentageThreshold.Value);
            }
        }
    }
}
