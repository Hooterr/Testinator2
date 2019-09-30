﻿using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Implementation.Questions
{
    public class MultipleCheckBoxesQuestionScoring : BaseQuestionScoring<MultipleCheckBoxesQuestionUserAnswer>
    {
        public List<bool> CorrectAnswer { get; internal set; }

        protected override int CalculateCorrectPercentage(MultipleCheckBoxesQuestionUserAnswer userAnswer)
        {
            if (userAnswer.CheckedOptions.Count != CorrectAnswer.Count)
                throw new ArgumentException("Incompatible user answer length.", nameof(userAnswer));

            // TODO make a simple fraction class
            var correctAnswersCount = 0;

            for(var i = 0; i < CorrectAnswer.Count; i++)
            {
                if (CorrectAnswer[i] == userAnswer.CheckedOptions[i])
                    correctAnswersCount++;
            }

            // Yeah really need that fraction class
            // eg. return new Fraction(correctAnswerCount, TotalCount);
            // or something
            return (int)Math.Round((double)correctAnswersCount * 100 / CorrectAnswer.Count);
        }
    }
}
