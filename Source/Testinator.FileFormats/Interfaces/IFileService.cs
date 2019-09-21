using System.IO;

namespace Testinator.Files
{
    public interface IFileService
    {
        FileContext GetFileInfo(FileStream stream);
        void SaveFile(FileStream stream, FileContext info, byte[] data);
    }
}
