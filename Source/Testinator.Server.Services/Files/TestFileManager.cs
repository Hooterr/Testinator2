using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Testinator.Files;
using Testinator.Server.Domain;
using Testinator.Server.Serialization;
using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Services
{
    public class TestFileManager : ITestFileManager
    {
        private readonly IFileAccessService mFileAccess;
        private readonly IFileService mFilesEncoder;
        private readonly ISerializer<Test> mSerializer;

        public TestFileManager(IFileAccessService files, IFileService fileService)
        {
            mFileAccess = files;
            mFilesEncoder = fileService;
            mSerializer = SerializerFactory.New<Test>();
        }

        public TestFileContext GetTestContext(string absolutePath)
        {
            var fs = mFileAccess.GetFile(options =>
            {
                options.UseAbsolutePath(absolutePath)
                       .ReadOnlyMode();
            });

            var fileInfo = mFilesEncoder.GetFileInfo(fs);
            var testFileContext = new TestFileContext
            {
                // write a wrapper around meta-data collection
                FilePath = absolutePath,
                TestName = fileInfo.Metadata.ContainsKey("Name") ? fileInfo.Metadata["Name"] : null,
                Author = fileInfo.Metadata.ContainsKey("Author") ? fileInfo.Metadata["Author"] : null,
                Tags = fileInfo.Metadata.ContainsKey("Tags") ? fileInfo.Metadata["Tags"].Split('#') : null
            };

            return testFileContext;
        }

        public TestFileContext GetTestContext(ApplicationDataFolders folder, string fileName)
        {
            throw new NotImplementedException();
        }

        public TestFileContext[] GetTestContexts(string absoluteDirectoryPath)
        {
            throw new NotImplementedException();
        }

        public TestFileContext[] GetTestContexts(ApplicationDataFolders folder)
        {
            throw new NotImplementedException();
        }

        public bool Save(Test test, string fileName, string absoluteFolderPath)
        {
            var fs = mFileAccess.GetFile(options =>
            {
                options.InFolder(absoluteFolderPath, fileName)
                .WriteMode();
            });

            var tags = new StringBuilder();
            var currCat = test.Info.Category;
            while(currCat != null)
            {
                tags.Append($"#{currCat.Name}");
                currCat = currCat.SubCategory;
            }

            var fileContext = new FileContext()
            {
                Version = 1,
                Metadata = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()
                {
                    { "Name", test.Info.Name },
                    { "Tags", tags.ToString() },

                }),
            };

            var testBytes = mSerializer.Serialize(test);

            mFilesEncoder.SaveFile(fs, fileContext, testBytes);
            return true;
        }
    }
}
