using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    internal class Grading : IGrading
    {
        public IGradingStrategy Strategy { get; internal set; }

        public int MaxPointScore { get; internal set; }

        public IGrade GetGrade(int score)
        {
            return Strategy.GetGrade(score);
        }
    }
}
