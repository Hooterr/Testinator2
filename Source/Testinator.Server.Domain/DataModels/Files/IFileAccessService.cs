using System;
using System.IO;

namespace Testinator.Server.Domain
{
    public interface IFileAccessService
    {
        FileStream GetFile(Action<GetFileOptions> configureOptions);
        string[] GetAllFileNames(Action<GetFilesFromDirectoryOptions> configureOptions);
    }
}
