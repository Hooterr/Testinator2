using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Testinator.Files
{
    public class FileService : IFileService
    {
        private readonly MetadataEncoder mMetadataEncoder;
        public FileInfo GetFileInfo(FileStream stream)
        {
            var fileInfo = new FileInfo();

            var signatureBytes = new byte[8];
            FileSignature signature;
            byte[] metadataBytes;
            try
            { 
                signatureBytes = new byte[8];
                stream.Read(signatureBytes, 0, 8);

                var signatureULong = BitConverter.ToUInt64(signatureBytes, 0);
                signature = new FileSignature(signatureULong);
                metadataBytes = new byte[signature.MetadataLength];
                stream.Read(metadataBytes, 0, signature.MetadataLength);
            }
            catch
            {
                stream.Close();
                throw;
            }
            

            fileInfo.Version = signature.Version;
            var metadata = mMetadataEncoder.Parse(metadataBytes);
            fileInfo.Metadata = new ReadOnlyDictionary<string, string>(metadata);
            return fileInfo;
        }

        public void SaveFile(FileStream stream, FileInfo info, byte[] data)
        {
            
            var fileHeader = info.GenerateHeader();
            var headerBytes = fileHeader.GetAllBytes();
            // Here goes the header
            stream.Write(headerBytes, 0, headerBytes.Length);
            // And right after that the actual file content
            stream.Write(data, 0, data.Length);
        }

        public FileService()
        {
            mMetadataEncoder = new MetadataEncoder();
        }
    }
}
