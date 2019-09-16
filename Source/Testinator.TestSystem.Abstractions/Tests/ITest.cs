using System.Collections.Generic;

namespace Testinator.TestSystem.Abstractions.Tests
{
    public interface ITest
    {
        /// <summary>
        /// Information about this test
        /// </summary>
        ITestInfo Info { get; }

        /// <summary>
        /// The criteria attached to this test
        /// </summary>
        IGrading Grading { get; }

        /// <summary>
        /// The list of questions attached to this test
        /// </summary>
        IList<IQuestionProvider> Questions { get; }

        /// <summary>
        /// The options for this test to implement
        /// Contains crucial info about how we should handle questions etc.
        /// </summary>
        ITestOptions Options { get; }

        int Version { get; }
    }
}
