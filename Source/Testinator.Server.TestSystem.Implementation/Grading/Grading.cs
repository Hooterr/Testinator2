using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public class Grading : IGrading
    {
        public string Name { get; internal set; }

        // Strategies are not complete
        public IGradingStrategy Strategy { get; internal set; }

        public int MaxPointScore { get; internal set; }

        public IGrade GetGrade(int score)
        {
            return Strategy.GetGrade(score, MaxPointScore);
        }
    }
}
