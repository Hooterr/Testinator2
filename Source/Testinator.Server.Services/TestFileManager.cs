﻿using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Files;
using Testinator.Server.Domain;

namespace Testinator.Server.Services
{
    public class TestFileManager : ITestFileManager
    {
        private readonly IFileManager mFiles;
        private readonly IFileService mFilesEncoder;

        public TestFileManager(IFileManager files, IFileService fileService)
        {
            mFiles = files;
            mFilesEncoder = fileService;
        }

        public TestFileContext GetTestContext(string absolutePath)
        {
            var fs = mFiles.GetFile(options =>
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
    }
}
