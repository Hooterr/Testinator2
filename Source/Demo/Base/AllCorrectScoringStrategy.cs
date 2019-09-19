using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Implementation.Questions.ScoringStrategy;

namespace Demo
{
    /// <summary>
    /// The strategy that requires 100% correct answer to give full point score, otherwise 0
    /// </summary>
    [Serializable]
    public class AllCorrectScoringStrategy : IScoringStrategy
    {
        public int Evaluate(int maxPointScore, int percentageDoneCorrect)
        {
            return percentageDoneCorrect == 100 ? maxPointScore : 0;
        }
    }
}
