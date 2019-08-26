using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// Guidelines for marking questions
    /// </summary>
    public interface IQuestionScoring
    {
        /// <summary>
        /// Score one gets for a fully correct answer
        /// </summary>
        int MaximumScore { get; }

        /// <summary>
        /// Check the answer against scoring guidelines
        /// </summary>
        /// <param name="answer">The answer examinee gave to the question</param>
        /// <returns>Appropriate point score</returns>
        int CheckAnswer(IUserAnswer answer);
    }
}
