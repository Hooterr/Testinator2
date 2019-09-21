using System;
using System.IO;

namespace Testinator.Server.Domain
{
    public interface IFileManager
    {
        FileStream GetFile(Action<GetFileOptions> configureOptions);
        string[] GetAllFileNames(string absolutePath, string extensionFilter = null);
    }
}
