using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Questions.ScoringStrategy
{
    /// <summary>
    /// For each part of the answer that is correct give appropriate fraction of the point score
    /// </summary>
    public class CorrespondingFractionScoringStrategy : IScoringStrategy
    {
        public int Evaluate(int maxPointScore, int percentageDoneCorrect)
        {
            var fractionDoneCorrect = (decimal)percentageDoneCorrect / 100;
            var pointscore = (int)Math.Floor(fractionDoneCorrect);
            return pointscore;
        }
    }
}
