namespace Testinator.Server.Domain
{
    public class GetFilesFromDirectoryOptions
    {
        public ApplicationDataFolders? ApplicationFolder { get; private set; }
        public string AbsoluteFolderPath { get; private set; }
        public string SearchPatter { get; private set; }

        public GetFilesFromDirectoryOptions Folder(ApplicationDataFolders folder)
        {
            ApplicationFolder = folder;
            return this;
        }

        public GetFilesFromDirectoryOptions Absolute(string path)
        {
            ApplicationFolder = null;
            AbsoluteFolderPath = path;
            return this;
        }

        public GetFilesFromDirectoryOptions Pattern(string searchPattern)
        {
            SearchPatter = searchPattern;
            return this;
        }

        public GetFilesFromDirectoryOptions()
        {  }
    }
}
