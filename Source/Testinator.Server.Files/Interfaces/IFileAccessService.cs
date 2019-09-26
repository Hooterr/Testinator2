using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Handles access to files
    /// </summary>
    public interface IFileAccessService
    {
        string DataFolderRootPath { get; }

        /// <summary>
        /// Get a file context from a file
        /// </summary>
        /// <param name="absolutePath">The absolute path to the file</param>
        /// <returns>File context</returns>
        FileContext GetFileInfo(string absolutePath);

        /// <summary>
        /// Saves data to file
        /// </summary>
        /// <param name="absolutePath">The absolute path to a file</param>
        /// <param name="info">Information about that files</param>
        /// <param name="data">The actual files content</param>
        void SaveFile(string absolutePath, FileContext info, byte[] data);
    }
}
