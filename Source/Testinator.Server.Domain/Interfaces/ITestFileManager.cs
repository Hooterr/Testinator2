using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Handles reading and saving <see cref="Test"/>s to a file
    /// </summary>
    public interface ITestFileManager
    {
        /// <summary>
        /// Gets test context from an absolute path
        /// </summary>
        /// <param name="absolutePath">Absolute path to the test file</param>
        /// <returns>Test file context</returns>
        TestFileContext GetTestContext(string absolutePath);

        /// <summary>
        /// Gets test file context from an application data folder
        /// </summary>
        /// <param name="folder">The folder in which the test should be</param>
        /// <param name="fileName">The name of the file</param>
        /// <returns>Test file context</returns>
        TestFileContext GetTestContext(ApplicationDataFolders folder, string fileName);

        /// <summary>
        /// Gets test contexts of all test in a given directory
        /// </summary>
        /// <param name="absoluteDirectoryPath">Absolute path to the director</param>
        /// <returns>Test files contexts</returns>
        TestFileContext[] GetTestContexts(string absoluteDirectoryPath);
        
        /// <summary>
        /// Gets test context of all test in an application data folder
        /// </summary>
        /// <param name="folder">The folder to look for test in</param>
        /// <returns>Test files contexts</returns>
        TestFileContext[] GetTestContexts(ApplicationDataFolders folder);

        /// <summary>
        /// Save a test to file
        /// </summary>
        /// <param name="test">The test to save</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="absoluteFolderPath">Absolute path to the file</param>
        /// <returns>True if successful, otherwise false</returns>
        bool Save(Test test, string fileName, string absoluteFolderPath);

        // TODO Save(Test,ApplicationDataFolder,name)
        // and more
    }
}
