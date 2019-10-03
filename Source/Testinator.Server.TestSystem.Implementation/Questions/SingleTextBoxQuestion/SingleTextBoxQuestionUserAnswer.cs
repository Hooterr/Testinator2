using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation.Questions
{
    public class SingleTextBoxQuestionUserAnswer : IUserAnswer
    {
        public int QuestionId { get; set; }

        public string UserAnswerText { get; set; }
    }
}
