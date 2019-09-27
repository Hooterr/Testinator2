using System.IO;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Options to configure when accessing a single file
    /// </summary>
    public class GetFileOptions
    {
        /// <summary>
        /// The absolute path to the file
        /// </summary>
        public string AbsolutePath { get; private set; }

        /// <summary>
        /// The name of the file
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The folder that contains the file if using application data folders
        /// </summary>
        public ApplicationDataFolders? ApplicationFolder { get; private set; }

        /// <summary>
        /// Sets the directory in which the file is supposed to be
        /// </summary>
        /// <param name="absolutePath">Absolute path too the folder</param>
        /// <returns>Fluid interface</returns>
        public GetFileOptions InFolderAbsolute(string absolutePath)
        {
            AbsolutePath = absolutePath;
            FileName = null;
            ApplicationFolder = null;
            return this;
        }

        /// <summary>
        /// Sets the directory in which the file is supposed to be using application data folders
        /// </summary>
        /// <param name="folder">The folder</param>
        /// <returns>Fluid interface</returns>
        public GetFileOptions InApplicationFolder(ApplicationDataFolders folder)
        {
            AbsolutePath = null;
            ApplicationFolder = folder;
            return this;
        }

        /// <summary>
        /// Sets the name of the files
        /// </summary>
        /// <param name="name">The name of the file WITHOUT extension</param>
        /// <returns>Fluid interface</returns>
        public GetFileOptions WithName(string name)
        {
            FileName = name;
            return this;
        }
    }
}
