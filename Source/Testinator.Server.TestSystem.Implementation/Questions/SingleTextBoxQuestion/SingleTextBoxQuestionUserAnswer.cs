using System;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation.Questions
{
    [Serializable]
    public class SingleTextBoxQuestionUserAnswer : IUserAnswer
    {
        public int QuestionId { get; set; }

        public string UserAnswerText { get; set; }
    }
}
