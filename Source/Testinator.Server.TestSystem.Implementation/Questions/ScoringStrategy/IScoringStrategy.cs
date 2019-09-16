using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Implementation.Questions.ScoringStrategy
{
    public interface IScoringStrategy
    {
        int Evaluate(int maxPointScore, int percentageDoneCorrect);
    }
}
