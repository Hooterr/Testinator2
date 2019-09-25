using System.IO;

namespace Testinator.Files
{
    /// <summary>
    /// Handles basic operation on Testinator files
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Get a file context from a file stream
        /// </summary>
        /// <param name="stream">Stream to the file</param>
        /// <returns>File context</returns>
        FileContext GetFileInfo(FileStream stream);

        /// <summary>
        /// Saves data to files
        /// </summary>
        /// <param name="stream">Stream to the files</param>
        /// <param name="info">Information about that files</param>
        /// <param name="data">The actual files content</param>
        void SaveFile(FileStream stream, FileContext info, byte[] data);
    }
}
