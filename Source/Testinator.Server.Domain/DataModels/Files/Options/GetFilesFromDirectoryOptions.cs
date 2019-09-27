namespace Testinator.Server.Domain
{
    /// <summary>
    /// Options to configure when accessing a group of files a directory
    /// </summary>
    public class GetFilesFromDirectoryOptions
    {
        /// <summary>
        /// Application folder in which the files are supposed to be
        /// </summary>
        public ApplicationDataFolders? ApplicationFolder { get; private set; }

        /// <summary>
        /// Absolute path to the folder
        /// </summary>
        public string AbsoluteFolderPath { get; private set; }

        /// <summary>
        /// The search pattern for the files. e.g. *.txt, *.png etc
        /// </summary>
        public string SearchPatter { get; private set; }

        /// <summary>
        /// Sets the directory to search 
        /// </summary>
        /// <param name="folder">The application folder to search in</param>
        /// <returns>Fluid interface</returns>
        public GetFilesFromDirectoryOptions InApplicationFolder(ApplicationDataFolders folder)
        {
            ApplicationFolder = folder;
            return this;
        }

        /// <summary>
        /// Sets the absolute path to a folder to search
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public GetFilesFromDirectoryOptions InFolderAbsolute(string path)
        {
            ApplicationFolder = null;
            AbsoluteFolderPath = path;
            return this;
        }

        /// <summary>
        /// Sets the search pattern
        /// </summary>
        /// <param name="searchPattern">The pattern. For instance *.txt, *.png</param>
        /// <returns>Fluid interface</returns>
        public GetFilesFromDirectoryOptions UseSearchPattern(string searchPattern)
        {
            SearchPatter = searchPattern;
            return this;
        }
    }
}
