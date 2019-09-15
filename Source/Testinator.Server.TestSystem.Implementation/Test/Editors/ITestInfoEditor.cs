using System;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface ITestInfoEditor
    {
        /// <summary>
        /// The name of this test
        /// </summary>
        [EditorProperty]
        string Name { get; set; }

        /// <summary>
        /// The date when this test was last edited
        /// </summary>
        [EditorProperty]
        DateTime LastEditionDate { get; set; }

        /// <summary>
        /// The time limit for this test
        /// </summary>
        [EditorProperty]
        TimeSpan TimeLimit { get; set; }

        [EditorProperty]
        Category Category { get; set; }
    }
}
