using System;

namespace Testinator.TestSystem.Abstractions.Tests
{
    public interface ITestInfo
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
        /// The time limit for this test
        /// </summary>
        TimeSpan TimeLimit { get; }
    }
}
