using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class MultipleChoiceAnswer : IQuestionAnswer, IPercentageEvaluable<int>
    {
        /// <summary>
        /// The answers for this question
        /// NOTE: user IList interface since ICollection does not guarantee 
        /// </summary>
        private IList<string> mAnswers;

        /// <summary>
        /// The answers for this question
        /// NOTE: using IEnumerable so the order cannot be modified outside this class
        /// </summary>
        public IEnumerable<string> Answers => mAnswers;

        /// <summary>
        /// The index of the correct answer
        /// NOTE: we're counting from 0!
        /// </summary>
        public int CorrectAnswerIndex { get; set; }

        public bool IsWellDefined()
        {
            var isWellDefined = true;

            isWellDefined &= mAnswers.Count > 0;
            isWellDefined &= CorrectAnswerIndex < mAnswers.Count;

            return isWellDefined;
        }

        public MultipleChoiceAnswer()
        {
            mAnswers = new List<string>();
        }

        public void AppendAnswer(string value)
        {
            mAnswers.Add(value);
        }

        public void SetAnswers(IList<string> answers)
        {
            mAnswers = answers;
        }

        /// <summary>
        /// Deletes the answer at position
        /// The correct answer index will be update automatically
        /// </summary>
        /// <param name="positionToDeleteAt">The position to delete the answer at. Indexing from 0</param>
        public void DeleteAnswerAt(int positionToDeleteAt)
        {
            if (positionToDeleteAt < 0 || positionToDeleteAt >= mAnswers.Count)
                throw new ArgumentException("The position must be between 0 and the answers list count minus one");

            mAnswers.RemoveAt(positionToDeleteAt);

            if (positionToDeleteAt <= CorrectAnswerIndex && CorrectAnswerIndex != 0)
                CorrectAnswerIndex--;
        }

        public int Evaluate(int toEvaluate)
        {
            return toEvaluate == CorrectAnswerIndex ? 100 : 0;    
        }
    }
}
