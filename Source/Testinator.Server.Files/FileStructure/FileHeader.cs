using System.Linq;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Represents a header of any file
    /// </summary>
    internal class FileHeader
    {
        #region Public Properties

        /// <summary>
        /// File signature
        /// </summary>
        public FileSignature Signature { get; private set; }

        /// <summary>
        /// File's metadata
        /// </summary>
        public byte[] Metadata { get; private set; } 

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="signature">A signature for this file</param>
        /// <param name="metadataBytes">File's metadata</param>
        public FileHeader(FileSignature signature, byte[] metadataBytes = null)
        {
            Signature = signature;
            Metadata = metadataBytes;

            if (Metadata == null)
                Metadata = new byte[0];
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the header into bytes
        /// </summary>
        /// <returns>File header as bytes</returns>
        public byte[] GetAllBytes()
        {
            // First the signature, then the metadata
            return Signature.ToBytes().Concat(Metadata).ToArray();
        } 

        #endregion
    }
}
