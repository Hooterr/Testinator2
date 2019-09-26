using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Default implementation of <see cref="IFileAccessService"/>
    /// </summary>
    public class FileAccessService : IFileAccessService
    {
        #region Private Members
        
        /// <summary>
        /// Root folder for application data
        /// </summary>
        private readonly string mDataRootFolder;

        private readonly MetadataEncoder mMetadataEncoder;

        public string DataFolderRootPath => mDataRootFolder;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileAccessService()
        {
            // TODO maybe get this from settings
            mDataRootFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Testinator";

            // Ensure folders are created
            // TODO handle the case when we got no permission to do this
            Directory.CreateDirectory(mDataRootFolder);

            // Make sure all the directories exist
            foreach (var folder in typeof(ApplicationDataFolders).GetEnumValues())
            {
                Directory.CreateDirectory($"{mDataRootFolder}\\{folder.ToString()}");
            }

            mMetadataEncoder = MetadataEncoder.Default; 
        }

        #endregion

        #region Interface Methods

        public FileContext GetFileInfo(string absolutePath)
        {
            var fileInfo = new FileContext();

            var signatureBytes = new byte[8];
            FileSignature signature;
            byte[] metadataBytes;
            
            using(var fs = new FileStream(absolutePath, FileMode.Open))
            { 
                signatureBytes = new byte[8];
                fs.Read(signatureBytes, 0, 8);

                var signatureULong = BitConverter.ToUInt64(signatureBytes, 0);
                signature = new FileSignature(signatureULong);
                metadataBytes = new byte[signature.MetadataLength];
                fs.Read(metadataBytes, 0, signature.MetadataLength);
            }

            fileInfo.Version = signature.Version;
            fileInfo.Metadata = mMetadataEncoder.Parse(metadataBytes);
            return fileInfo;
        }

        public void SaveFile(string absolutePath, FileContext info, byte[] data)
        {
            var fileHeader = info.GenerateHeader(mMetadataEncoder);
            var headerBytes = fileHeader.GetAllBytes();

            using (var fs = new FileStream(absolutePath, FileMode.Create))
            {
                // Here goes the header
                fs.Write(headerBytes, 0, headerBytes.Length);
                // And right after that the actual file content
                fs.Write(data, 0, data.Length);
            }
        }

        public byte[] ReadFileContents(string absolutePath)
        {
            byte[] wholeFile;
            using (var fs = new FileStream(absolutePath, FileMode.Open))
            {
                using (var ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    wholeFile = ms.ToArray();
                }
            }

            var signatureULong = BitConverter.ToUInt64(wholeFile.Take(8).ToArray(), 0);
            var signature = new FileSignature(signatureULong);
            return wholeFile.Skip(8 + signature.MetadataLength).ToArray();
        }

        #endregion
    }
}
