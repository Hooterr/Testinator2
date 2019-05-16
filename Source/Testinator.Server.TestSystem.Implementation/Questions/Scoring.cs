using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class PointScoring : IEvaluable
    {
        private int mMax = 1;
        public int Max
        {
            get => mMax;
            set
            {
                if (mMax < 1)
                    throw new ArgumentException("The maximum point score value cannot be less than 1");

                mMax = value;
            }
        }
        // TODO: evalution strategies
        // Presuming it's linear for now

        public int Evalute(int correctPercentage)
        {
            if (correctPercentage < 0 || correctPercentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100");

            return correctPercentage / 100 * Max;
        }

        public int GetMaximumPossibleScore()
        {
            return Max;
        }

        public bool IsWellDefined()
        {
            // TODO do the actual validation
            return true;
        }
    }
}
