using System;
using System.IO;
using Testinator.Server.Domain;

namespace Testinator.Files
{
    public class FileAccessService : IFileAccessService
    {
        private readonly string mDataRootFolder;
        public FileAccessService()
        {
            // TODO maybe get this from settings
            mDataRootFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Testinator";

            // Ensure folders are created
            // TODO handle the case when there we got no permission to do this
            Directory.CreateDirectory(mDataRootFolder);

            foreach(var folder in typeof(ApplicationDataFolders).GetEnumValues())
            {
                Directory.CreateDirectory($"{mDataRootFolder}\\{folder.ToString()}");
            }
        }

        public string[] GetAllFileNames(Action<GetFilesFromDirectoryOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions), "Get files options action cannot be null");

            var options = new GetFilesFromDirectoryOptions();
            configureOptions.Invoke(options);

            var result = new string[0];
            try
            {
                var adaper = new GetFilesFromDirectoryOptionsAdapter(options, mDataRootFolder);
                adaper.GetPath(out var path, out var searchPattern);
                result = Directory.GetFiles(path, searchPattern);
            }
            catch
            {
                // TODO error handling?
            }
            return result;
        }

        public FileStream GetFile(Action<GetFileOptions> configureOptions)
        {
            if(configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions), "Get file options action cannot be null");

            var options = new GetFileOptions();
            configureOptions.Invoke(options);
            FileStream fs = null;
            try
            {
                var adapter = new GetFileOptionsAdapter(options, mDataRootFolder);
                adapter.GetPath(out var path, out var fileMode);
                fs = new FileStream(path, fileMode);
            }
            catch
            {
                // TODO error handling
            }

            return fs;
        }
    }
}
