using System;
using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Handles reading and saving <see cref="Test"/>s to a file
    /// </summary>
    public interface ITestFileManager
    {
        /// <summary>
        /// Gets test context from a file
        /// </summary>
        /// <param name="configureOptions">The action to configure options</param>
        /// <returns>Test file context</returns>
        TestFileContext GetTestContext(Action<GetFileOptions> configureOptions);

        /// <summary>
        /// Gets test contexts of all test in a given directory
        /// </summary>
        /// <param name="configureOptions">The action to configure options</param>
        /// <returns>Test files contexts</returns>
        TestFileContext[] GetTestContexts(Action<GetFilesFromDirectoryOptions> configureOptions);

        /// <summary>
        /// Save a test to file
        /// </summary>
        /// <param name="test">The test to save</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="absoluteFolderPath">Absolute path to the file</param>
        /// <returns>True if successful, otherwise false</returns>
        bool Save(Action<GetFileOptions> configureOptions, Test test);

        // TODO Save(Test,ApplicationDataFolder,name)
        // and more
    }
}
