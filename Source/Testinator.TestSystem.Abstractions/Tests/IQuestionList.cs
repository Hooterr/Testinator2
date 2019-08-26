using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions
{
    public interface IQuestionList
    {
        ICollection<IQuestionProvider> Questions { get; }

        /*
        /// <summary>
        /// The collection of references (as GUID string) to the questions that are in a main poll
        /// </summary>
        ICollection<string> PoolQuestions { get; }

        /// <summary>
        /// The collection of locally-saved questions that are not in a main pool
        /// </summary>
        ICollection<Question> LocalQuestions { get; }
        */
    }
}