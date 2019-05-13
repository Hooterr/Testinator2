using System;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The single test that can be created/edited/sent to the clients
    /// </summary>
    public class Test
    {
        /// <summary>
        /// The name of this test
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date when this test was initially created
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The date when this test was last edited
        /// </summary>
        public DateTime LastEditionDate { get; set; }

        /// <summary>
        /// The category of this test
        /// Can contain subcategories
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// The criteria attached to this test
        /// </summary>
        public Criteria Criteria { get; set; }

        /// <summary>
        /// The list of questions attached to this test
        /// </summary>
        public QuestionList Questions { get; set; }

        /// <summary>
        /// The time this test can take at most
        /// </summary>
        public TimeSpan CompletionTime { get; set; }

        /// <summary>
        /// The options for this test to implement
        /// Contains crucial info about how we should handle questions etc.
        /// </summary>
        public TestOptions Options { get; set; }
    }
}
