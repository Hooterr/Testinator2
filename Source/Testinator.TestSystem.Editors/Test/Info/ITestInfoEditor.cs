using System;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for the info part of a test
    /// </summary>
    public interface ITestInfoEditor : IErrorListener<ITestInfoEditor>
    {
        /// <summary>
        /// The name of this test
        /// </summary>
        [EditorProperty]
        string Name { get; set; }

        /// <summary>
        /// The time limit for this test
        /// </summary>
        [EditorProperty]
        TimeSpan TimeLimit { get; set; }

        /// <summary>
        /// Categories for this test
        /// </summary>
        [EditorProperty]
        Category Category { get; set; }
    }
}
