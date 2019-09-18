namespace Testinator.TestSystem.Editors.Test.Builder
{
    /// <summary>
    /// Configures the and builds the editor ready to use
    /// </summary>
    public interface ITestEditorBuilder
    {
        /// <summary>
        /// Finish the process by building
        /// </summary>
        /// <returns>Configured editor</returns>
        ITestEditor Build();

        /// <summary>
        /// Edit an existing test
        /// </summary>
        /// <param name="test">The test to edit</param>
        /// <returns>Fluid interface</returns>
        ITestEditorBuilder SetInitialTest(Implementation.Test test);

        /// <summary>
        /// Create a new test
        /// </summary>
        /// <returns>Fluid interface</returns>
        ITestEditorBuilder NewTest();

        /// <summary>
        /// Set the test system version to use
        /// </summary>
        /// <param name="version">The test system version to use</param>
        /// <returns>Fluid interface</returns>
        ITestEditorBuilder SetVersion(int version);

        /// <summary>
        /// Sets to use the newest version
        /// </summary>
        /// <returns>Fluid interface</returns>
        ITestEditorBuilder UseNewestVersion();
    }
}
