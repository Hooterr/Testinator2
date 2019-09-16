using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    public class Grading : IGrading
    {
        public string Name { get; internal set; }

        public IGradingStrategy Strategy { get; internal set; }

        public int MaxPointScore { get; internal set; }

        public IGrade GetGrade(int score)
        {
            return Strategy.GetGrade(score);
        }
    }
}
