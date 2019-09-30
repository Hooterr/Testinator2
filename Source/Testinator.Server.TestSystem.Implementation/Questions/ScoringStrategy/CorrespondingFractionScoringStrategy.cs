using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation.Questions.ScoringStrategy
{
    /// <summary>
    /// For each part of the answer that is correct give appropriate fraction of the point score
    /// </summary>
    [Serializable]
    [Name("Max number of points times % correct strategy")]
    // TODO make name with description thing
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
