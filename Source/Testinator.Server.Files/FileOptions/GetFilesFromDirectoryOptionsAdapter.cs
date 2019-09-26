using System;
using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    public class GetFilesFromDirectoryOptionsAdapter
    {
        private readonly string mRootDataFolderPath;
        private readonly GetFilesFromDirectoryOptions mGetFilesOptions;

        public GetFilesFromDirectoryOptionsAdapter(GetFilesFromDirectoryOptions mGetFilesOptions, string mRootDataFolderPath)
        {
            this.mRootDataFolderPath = mRootDataFolderPath;
            this.mGetFilesOptions = mGetFilesOptions;
        }

        public void GetPath(out string path, out string searchPattern)
        {
            searchPattern = mGetFilesOptions.SearchPatter;

            if (!string.IsNullOrEmpty(mGetFilesOptions.AbsoluteFolderPath))
                path = mGetFilesOptions.AbsoluteFolderPath;

            else if(mGetFilesOptions.ApplicationFolder != null)
                path = $"{mRootDataFolderPath}\\{mGetFilesOptions.ApplicationFolder.Value.GetFolderName()}";

            throw new InvalidOperationException("You must at least specify application folder or an absolute path to a folder.");
        }
    }
}
