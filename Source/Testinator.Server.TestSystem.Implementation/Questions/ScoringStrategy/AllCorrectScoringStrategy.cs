using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Questions.ScoringStrategy
{
    /// <summary>
    /// The strategy that requires 100% correct answer to give full point score, otherwise 0
    /// </summary>
    public class AllCorrectScoringStrategy : IScoringStrategy
    {
        public int Evaluate(int maxPointScore, int percentageDoneCorrect)
        {
            return percentageDoneCorrect == 100 ? maxPointScore : 0;
        }
    }
}
