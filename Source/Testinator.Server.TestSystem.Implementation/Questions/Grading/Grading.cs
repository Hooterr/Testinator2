using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Grading
{
    public class Grading : IGrading
    {
        public string Name { get; private set; }


        public IGradingStrategy Strategy { get; private set; }

        public IGrade GetGrade(int score)
        {
            return Strategy.GetGrade(score);
        }

        public Grading(string name, IGradingStrategy strategy)
        {
            Name = name;
            Strategy = strategy;
        }
    }
}
