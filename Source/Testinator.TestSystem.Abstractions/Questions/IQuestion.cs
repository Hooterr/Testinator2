using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Questions
{
    public interface IQuestion
    {
        /// <summary>
        /// The content of this question task as <see cref="IQuestionTask"/>
        /// So it can be text or image or whatever implements this interface
        /// </summary>
        IQuestionTask Task { get; }

        /// <summary>
        /// The scoring guidelines for this question
        /// </summary>
        IQuestionScoring Scoring { get; }

        IQuestionOptions Options { get; }

        /// <summary>
        /// The author of this question
        /// NOTE: It's the person who created this question, not necessarily the one who owns it
        /// </summary>
        string Author { get; }

        /// <summary>
        /// The category of this question
        /// Can contain subcategories
        /// </summary>
        Category Category { get; }

        /// <summary>
        /// Code version of the test system when this question was created
        /// </summary>
        int Version { get; }
    }
}
