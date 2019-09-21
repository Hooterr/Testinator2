using System.IO;

namespace Testinator.Files
{
    public interface IFileService
    {
        FileInfo GetFileInfo(FileStream stream);
        void SaveFile(FileStream stream, FileInfo info, byte[] data);
    }
}
