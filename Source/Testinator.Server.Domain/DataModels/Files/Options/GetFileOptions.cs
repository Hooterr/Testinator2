namespace Testinator.Server.Domain
{
    public class GetFileOptions
    {
        public string AbsolutePath { get; private set; }
        public string FileName { get; private set; }
        public ApplicationDataFolders? Folder { get; private set; }

        public GetFileOptions UseAbsolutePath(string path)
        {
            AbsolutePath = path;
            FileName = null;
            Folder = null;
            return this;
        }

        public GetFileOptions InFolder(ApplicationDataFolders folder, string fileName)
        {
            AbsolutePath = null;
            FileName = fileName;
            Folder = folder;
            return this;
        }

        public GetFileOptions InFolder(string folderPath, string fileName)
        {
            AbsolutePath = folderPath + "\\" + fileName;
            FileName = null;
            Folder = null;
            return this;
        }
    }
}
