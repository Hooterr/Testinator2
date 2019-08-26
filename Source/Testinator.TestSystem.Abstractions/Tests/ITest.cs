using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions.Tests
{
    public interface ITest
    {
        /// <summary>
        /// The name of this test
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The date when this test was initially created
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// The date when this test was last edited
        /// </summary>
        DateTime LastEditionDate { get; }

        /// <summary>
        /// The category of this test
        /// Can contain subcategories
        /// </summary>
        Category Category { get; }

        /// <summary>
        /// The criteria attached to this test
        /// </summary>
        IGrading Grading { get; }

        /// <summary>
        /// The list of questions attached to this test
        /// </summary>
        IQuestionList Questions { get; }

        /// <summary>
        /// The time this test can take at most
        /// </summary>
        TimeSpan CompletionTime { get; }

        /// <summary>
        /// The options for this test to implement
        /// Contains crucial info about how we should handle questions etc.
        /// </summary>
        ITestOptions Options { get; }
    }
}
