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
    /// Default implementation of <see cref="IGradingPresetFileManager"/>
    /// </summary>
    public class GradingPresetFileManager : IGradingPresetFileManager
    {
        #region Private Members

        // All needed services
        private readonly IFileAccessService mFileAccess;
        private readonly ISerializer<IGradingPreset> mSerializer;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fileAccess">File access service</param>
        public GradingPresetFileManager(IFileAccessService fileAccess)
        {
            mFileAccess = fileAccess;
            mSerializer = SerializerFactory.New<IGradingPreset>();
        }

        #endregion

        #region Interface Methods

        public GradingPresetFileContext GetGradingPresetContext(Action<GetFileOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFileOptions();
            configureOptions.Invoke(options);

            var adapter = new GetFileOptionsAdapter(options, mFileAccess.DataFolderRootPath);

            var filePath = adapter.GetAbsolutePath();
            return GetFileContext(filePath);
        }

        public GradingPresetFileContext[] GetGradingPresetsContexts(Action<GetFilesFromDirectoryOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFilesFromDirectoryOptions();
            configureOptions.Invoke(options);
            if (string.IsNullOrEmpty(options.SearchPatter))
                options.UseSearchPattern($"*.{FileExtensions.GradingPreset}");

            var adapter = new GetFilesFromDirectoryOptionsAdapter(options, mFileAccess.DataFolderRootPath);
            adapter.GetPath(out var path, out var searchPattern);

            return GetFileContexts(path, searchPattern);
        }

        public IGradingPreset Read(Action<GetFileOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFileOptions();
            configureOptions.Invoke(options);
            var adapter = new GetFileOptionsAdapter(options, mFileAccess.DataFolderRootPath)
                .WithExtension(FileExtensions.GradingPreset);

            var path = adapter.GetAbsolutePath();
            var bytes = mFileAccess.ReadFileContents(path);
            return mSerializer.Deserialize(new MemoryStream(bytes));
        }

        public bool Save(Action<GetFileOptions> configureOptions, IGradingPreset gradingPreset)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            var options = new GetFileOptions();
            configureOptions.Invoke(options);
            try
            {
                var adapter = new GetFileOptionsAdapter(options, mFileAccess.DataFolderRootPath)
                    .WithExtension(FileExtensions.Test);

                var absolutePath = adapter.GetAbsolutePath();

                var fileContext = new FileContext()
                {
                    // TODO get this from somewhere
                    Version = 1,
                    Metadata = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()
                    {
                        { "Name", gradingPreset.Name },
                        { "CreatedDate", gradingPreset.CreationDate.ToString() },
                        { "NumberOfGrades", gradingPreset.PercentageThresholds.Count.ToString() },
                    }),
                };

                var presetBytes = mSerializer.Serialize(gradingPreset);

                mFileAccess.SaveFile(absolutePath, fileContext, presetBytes);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Private Methods

        private GradingPresetFileContext GetFileContext(string absolutePath)
        {
            var fileInfo = mFileAccess.GetFileInfo(absolutePath);
            var testFileContext = new GradingPresetFileContext
            {
                // write a wrapper around meta-data collection
                FilePath = absolutePath,
                Name = fileInfo.Metadata.ContainsKey("Name") ? fileInfo.Metadata["Name"] : null,
                CreatedData = fileInfo.Metadata.ContainsKey("CreatedDate") ? DateTime.Parse(fileInfo.Metadata["CreatedDate"]) : default,
                NumberOfGrades = fileInfo.Metadata.ContainsKey("NumberOfGrades") ? int.Parse(fileInfo.Metadata["NumberOfGrades"]) : default,
            };

            return testFileContext;
        }

        private GradingPresetFileContext[] GetFileContexts(string absoluteDirectoryPath, string searchPattern)
        {
            var fileNames = Directory.GetFiles(absoluteDirectoryPath, searchPattern);
            var fileContexts = new GradingPresetFileContext[fileNames.Length];

            for (var i = 0; i < fileNames.Length; i++)
                fileContexts[i] = GetFileContext(fileNames[i]);

            return fileContexts;
        }

        #endregion
    }
}
