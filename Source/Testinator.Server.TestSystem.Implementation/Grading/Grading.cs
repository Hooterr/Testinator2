using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation
{
    internal class Grading : IGrading
    {
        [CollectionCount(min: 2, max: 12, fromVersion: 1)]
        public IGradingStrategy Strategy { get; internal set; }

        public int MaxPointScore { get; internal set; }

        public IGrade GetGrade(int score)
        {
            return Strategy.GetGrade(score);
        }
    }
}
