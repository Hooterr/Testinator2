using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions.MultipleChoiceQuestion.Scoring
{
    public sealed class MultipleChoiceQuestionScoring : BaseQuestionScoring
    {
        
        public override int CheckAnswer(IUserAnswer answer)
        {
            // TODO check if the answer is of type MultipleChoiceQuestionUserAnswer
            throw new NotImplementedException();
        }
    }
}
