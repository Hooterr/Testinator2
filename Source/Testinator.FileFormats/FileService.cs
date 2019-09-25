using System;
using System.Collections.ObjectModel;
using System.IO;
using Testinator.Server.Domain;

namespace Testinator.Files
{
    /// <summary>
    /// Default implementation of <see cref="IFileService"/>
    /// </summary>
    public class FileService : IFileService
    {
        #region Private Members

        private readonly MetadataEncoder mMetadataEncoder;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructors
        /// </summary>
        public FileService()
        {
            mMetadataEncoder = MetadataEncoder.Default;
        }

        #endregion        
        
        #region Interface Methods

        public FileContext GetFileInfo(FileStream stream)
        {
            var fileInfo = new FileContext();

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
                stream.Dispose();
                throw;
            }


            fileInfo.Version = signature.Version;
            var metadata = mMetadataEncoder.Parse(metadataBytes);
            fileInfo.Metadata = new ReadOnlyDictionary<string, string>(metadata);
            return fileInfo;
        }

        public void SaveFile(FileStream stream, FileContext info, byte[] data)
        {
            var fileHeader = info.GenerateHeader(mMetadataEncoder);
            var headerBytes = fileHeader.GetAllBytes();

            using (stream)
            {
                // Here goes the header
                stream.Write(headerBytes, 0, headerBytes.Length);
                // And right after that the actual file content
                stream.Write(data, 0, data.Length);
            }
        } 

        #endregion
    }
}
