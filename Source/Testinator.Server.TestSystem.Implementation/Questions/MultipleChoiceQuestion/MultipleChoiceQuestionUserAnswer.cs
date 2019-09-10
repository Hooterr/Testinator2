﻿using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class MultipleChoiceQuestionUserAnswer : IUserAnswer
    {
        /// <summary>
        /// 0-based index of the answer the user selected
        /// 0-A, 1-B etc...
        /// </summary>
        public int SelectedAnswerIdx { get; set; }
        public int QuestionId { get; set; }

        public MultipleChoiceQuestionUserAnswer(int answerIdx)
        {
            SelectedAnswerIdx = answerIdx;
        }
    }
}
