using System;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
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

        [EditorProperty]
        DateTime CreationDate { get; set; }
    }
}
