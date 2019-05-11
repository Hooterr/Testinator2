using System.Collections.Generic;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// Contains questions that can be attached to test
    /// </summary>
    public class QuestionList
    {
        /// <summary>
        /// The collection of references (as GUID string) to the questions that are in a main poll
        /// </summary>
        public ICollection<string> PoolQuestions { get; set; }

        /// <summary>
        /// The collection of locally-saved questions that are not in a main pool
        /// </summary>
        public ICollection<Question> LocalQuestions { get; set; }
    }
}