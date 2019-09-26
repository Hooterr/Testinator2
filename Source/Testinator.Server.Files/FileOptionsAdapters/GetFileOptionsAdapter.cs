using System;
using System.IO;
using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    public class GetFileOptionsAdapter
    {
        private readonly string mRootDataFolderPath;
        private readonly GetFileOptions mGetFileOptions;

        public string GetAbsolutePath()
        { 
            if (!string.IsNullOrEmpty(mGetFileOptions.AbsolutePath))
            {
                return mGetFileOptions.AbsolutePath;
            }
            else if(mGetFileOptions.Folder != null)
            {
                if(!string.IsNullOrWhiteSpace(mGetFileOptions.FileName))
                    return $"{mRootDataFolderPath}\\{mGetFileOptions.Folder.Value.GetFolderName()}\\{mGetFileOptions.FileName}";
                else
                    throw new InvalidOperationException("File name cannot be empty.");
            }

            throw new InvalidCastException("You must specify either an absolute path to a file, or choose set folder and file name to look for.");
        }

        public GetFileOptionsAdapter(GetFileOptions options, string rootDataFolderPath)
        {
            mGetFileOptions = options;
            mRootDataFolderPath = rootDataFolderPath;
        }
    }
}
