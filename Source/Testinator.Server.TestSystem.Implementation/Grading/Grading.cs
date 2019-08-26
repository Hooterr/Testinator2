using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Grading
{
    public class Grading : IGrading
    {
        public string Name { get; internal set; }

        public DateTime CreationDate { get; internal set; }

        public DateTime LastEditionDate { get; internal set; }

        public IGradingStrategy Strategy { get; internal set; }

        public IGrade GetGrade(int score)
        {
            return Strategy.GetGrade(score);
        }
    }
}
