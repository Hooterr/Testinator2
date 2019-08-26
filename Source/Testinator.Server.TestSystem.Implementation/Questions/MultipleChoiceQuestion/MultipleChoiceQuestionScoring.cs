using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public sealed class MultipleChoiceQuestionScoring : BaseQuestionScoring
    {
        /// <summary>
        /// 0-based index of the correct answer for this question
        /// 0 - A, 1 - B etc...
        /// </summary>
        public int CorrectAnswerIdx { get; internal set; }
        
        public override int CheckAnswer(IUserAnswer answer)
        {
            // TODO check if the answer is of type MultipleChoiceQuestionUserAnswer
            throw new NotImplementedException();
        }
    }
}
