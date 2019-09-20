using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Testinator.Files
{
    public class FileService
    {
        private readonly MetadataEncoder mMetadataEncoder;
        public FileInfo GetFileInfo(string absolutePath)
        {
            var fileInfo = new FileInfo()
            {
                AbsolutePath = absolutePath,
            };
            var signatureBytes = new byte[8];
            FileSignature signature;
            byte[] metadataBytes;
            using (var fs = new FileStream(absolutePath, FileMode.Open))
            {
                try
                { 
                    signatureBytes = new byte[8];
                    fs.Read(signatureBytes, 0, 8);

                    var signatureULong = BitConverter.ToUInt64(signatureBytes, 0);
                    signature = new FileSignature(signatureULong);
                    metadataBytes = new byte[signature.MetadataLength];
                    fs.Read(metadataBytes, 0, signature.MetadataLength);
                }
                catch
                {
                    fs.Close();
                    throw;
                }
            }

            fileInfo.Version = signature.Version;
            var metadata = mMetadataEncoder.Parse(metadataBytes);
            fileInfo.Metadata = new ReadOnlyDictionary<string, string>(metadata);
            return fileInfo;
        }

        public void SaveFile(FileInfo info, byte[] data)
        {
            using (var fs = new FileStream(info.AbsolutePath, FileMode.Create))
            {
                var fileHeader = info.GenerateHeader();
                var headerBytes = fileHeader.GetAllBytes();
                // Here goes the header
                fs.Write(headerBytes, 0, headerBytes.Length);
                // And right after that the actual file content
                fs.Write(data, 0, data.Length);
            }
        }

        public FileService()
        {
            mMetadataEncoder = new MetadataEncoder();
        }
    }
}
