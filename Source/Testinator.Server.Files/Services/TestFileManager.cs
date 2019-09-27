using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Testinator.Server.Domain;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Implementation;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Default implementation of <see cref="ITestFileManager"/>
    /// </summary>
    public class TestFileManager : ITestFileManager
    {
        #region Private Members

        // All needed services
        private readonly IFileAccessService mFileAccess;
        private readonly ISerializer<Test> mSerializer;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fileAccess">File access service</param>
        /// <param name="fileService">TBA</param>
        public TestFileManager(IFileAccessService fileAccess)
        {
            mFileAccess = fileAccess;
            mSerializer = SerializerFactory.New<Test>();
        }

        #endregion

        #region Interface Methods

        public TestFileContext GetTestContext(Action<GetFileOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFileOptions();
            configureOptions.Invoke(options);

            var adapter = new GetFileOptionsAdapter(options, mFileAccess.DataFolderRootPath);

            var filePath = adapter.GetAbsolutePath();
            return GetFileContext(filePath);
        }

        public TestFileContext[] GetTestContexts(Action<GetFilesFromDirectoryOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFilesFromDirectoryOptions();
            configureOptions.Invoke(options);
            if (string.IsNullOrEmpty(options.SearchPatter))
                options.UseSearchPattern($"*.{FileExtensions.Test}");

            var adapter = new GetFilesFromDirectoryOptionsAdapter(options, mFileAccess.DataFolderRootPath);
            adapter.GetPath(out var path, out var searchPattern);

            return GetFileContexts(path, searchPattern);
        }

        public ITest Read(Action<GetFileOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFileOptions();
            configureOptions.Invoke(options);
            var adapter = new GetFileOptionsAdapter(options, mFileAccess.DataFolderRootPath)
                .WithExtension(FileExtensions.Test);

            var path = adapter.GetAbsolutePath();
            var bytes = mFileAccess.ReadFileContents(path);
            return mSerializer.Deserialize(new MemoryStream(bytes));
        }

        public bool Save(Action<GetFileOptions> configureOptions, Test test)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFileOptions();
            configureOptions.Invoke(options);

            var adapter = new GetFileOptionsAdapter(options, mFileAccess.DataFolderRootPath)
                .WithExtension(FileExtensions.Test);

            var absolutePath = adapter.GetAbsolutePath();

            var tags = new StringBuilder();
            var currCat = test.Info.Category;
            while (currCat != null)
            {
                tags.Append($"#{currCat.Name}");
                currCat = currCat.SubCategory;
            }

            var fileContext = new FileContext()
            {
                // TODO get this from somewhere
                Version = 1,
                Metadata = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()
                {
                    { "Name", test.Info.Name },
                    { "Tags", tags.ToString() },

                }),
            };

            var testBytes = mSerializer.Serialize(test);

            mFileAccess.SaveFile(absolutePath, fileContext, testBytes);
            return true;
        }

        #endregion

        #region Private Methods

        private TestFileContext GetFileContext(string absolutePath)
        {
            var fileInfo = mFileAccess.GetFileInfo(absolutePath);
            var testFileContext = new TestFileContext
            {
                // write a wrapper around meta-data collection
                FilePath = absolutePath,
                TestName = fileInfo.Metadata.ContainsKey("Name") ? fileInfo.Metadata["Name"] : null,
                Author = fileInfo.Metadata.ContainsKey("Author") ? fileInfo.Metadata["Author"] : null,
                Categories = fileInfo.Metadata.ContainsKey("Tags") ? fileInfo.Metadata["Tags"].Split('#') : null
            };

            return testFileContext;
        }

        private TestFileContext[] GetFileContexts(string absoluteDirectoryPath, string searchPattern)
        {
            var fileNames = Directory.GetFiles(absoluteDirectoryPath, searchPattern);
            var fileContexts = new TestFileContext[fileNames.Length];

            for (var i = 0; i < fileNames.Length; i++)
                fileContexts[i] = GetFileContext(fileNames[i]);

            return fileContexts;
        }

        #endregion
    }
}
