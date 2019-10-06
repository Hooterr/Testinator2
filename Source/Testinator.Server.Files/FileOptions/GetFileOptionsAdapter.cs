using System;
using System.IO;
using System.Linq;
using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    public class GetFileOptionsAdapter
    {
        private readonly string mRootDataFolderPath;
        private readonly GetFileOptions mGetFileOptions;

        private string mFileExtension;

        public string GetAbsolutePath()
        { 
            if (!string.IsNullOrEmpty(mGetFileOptions.AbsolutePath))
            {
                if (!string.IsNullOrWhiteSpace(mGetFileOptions.FileName))
                    return $"{mGetFileOptions.AbsolutePath}\\{mGetFileOptions.FileName}.{mFileExtension}";
                else
                    return mGetFileOptions.AbsolutePath;
            }
            else if(mGetFileOptions.ApplicationFolder != null)
            {
                if(!string.IsNullOrWhiteSpace(mGetFileOptions.FileName))
                    return $"{mRootDataFolderPath}\\{mGetFileOptions.ApplicationFolder.Value.GetFolderName()}\\{mGetFileOptions.FileName}.{mFileExtension}";
                else
                    throw new InvalidOperationException("File name cannot be empty.");
            }

            throw new InvalidCastException("You must specify either an absolute path to a file, or choose set folder and file name to look for.");
        }

        public GetFileOptionsAdapter WithExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension) || extension.Contains('.'))
                throw new ArgumentException(nameof(extension));

            mFileExtension = extension;

            return this;
        }

        public GetFileOptionsAdapter(GetFileOptions options, string rootDataFolderPath)
        {
            mGetFileOptions = options;
            mRootDataFolderPath = rootDataFolderPath;
            mFileExtension = string.Empty;
        }
    }
}
