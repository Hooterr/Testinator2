using System;
using System.IO;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Handles access to files
    /// </summary>
    public interface IFileAccessService
    {
        /// <summary>
        /// Gets a specific file
        /// </summary>
        /// <param name="configureOptions">Configure options method</param>
        /// <returns>Stream to the file</returns>
        FileStream GetFile(Action<GetFileOptions> configureOptions);

        /// <summary>
        /// Gets all file names from a given directory
        /// </summary>
        /// <param name="configureOptions">Configure options method</param>
        /// <returns>The file names</returns>
        string[] GetAllFileNames(Action<GetFilesFromDirectoryOptions> configureOptions);
    }
}
