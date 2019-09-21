using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Testinator.Server.Domain
{
    public class GetFileOptionsAdapter
    {
        private readonly string mRootDataFolderPath;
        private readonly GetFileOptions mGetFileOptions;

        public void GetPath(out string path, out FileMode mode)
        {
            mode = mGetFileOptions.OpenMode ?? throw new InvalidOperationException("File mode must be specified explicitly.");

            if (!string.IsNullOrEmpty(mGetFileOptions.AbsolutePath))
            {
                path = mGetFileOptions.AbsolutePath;
            }
            else if(mGetFileOptions.Folder != null)
            {
                if(!string.IsNullOrWhiteSpace(mGetFileOptions.FileName))
                {
                    path = $"{mRootDataFolderPath}\\{mGetFileOptions.Folder.ToString()}\\{mGetFileOptions.FileName}";
                }
                else
                    throw new InvalidOperationException("File name cannot be empty.");
            }

            throw new InvalidCastException("Just must specify either an absolute path to a file, or choose set folder and file name to look for.");
        }

        public GetFileOptionsAdapter(GetFileOptions options, string rootDataFolderPath)
        {
            mGetFileOptions = options;
            mRootDataFolderPath = rootDataFolderPath;
        }
    }
}
