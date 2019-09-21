using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Domain
{
    public interface ITestFileManager
    {
        TestFileContext GetTestContext(string absolutePath);
        TestFileContext GetTestContext(ApplicationDataFolders folder, string fileName);

        TestFileContext[] GetTestContexts(string absoluteDirectoryPath);
        TestFileContext[] GetTestContexts(ApplicationDataFolders folder);

        bool Save(Test test, string fileName, string absoluteFolderPath);
    }
}
