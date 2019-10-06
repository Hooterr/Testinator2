using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation.Questions
{
    [Serializable]
    public class MultipleCheckBoxesQuestionUserAnswer : IUserAnswer
    {
        /// <summary>
        /// Selected/not selected list of user answer
        /// </summary>
        public List<bool> CheckedOptions { get; set; }
        public int QuestionId { get; set; }

        public MultipleCheckBoxesQuestionUserAnswer()
        {
            CheckedOptions = new List<bool>();
        }
    }
}
