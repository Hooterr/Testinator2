using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testinator.Files
{
    /// <summary>
    /// Represents the signature of any file
    /// </summary>
    internal class FileSignature
    {

        #region Private Members

        #region Constants
        /*
         * 0x54, 0x6E, 0x72, Version, metadata length,  0x3D, 0x3D
         *      24 bits      12bits        12bits,        16bits
         * ----------------------- 64 bits ----------------------------
         * 
         */
        private const ulong EmptySignature =    0x546E720000003D3D;
        private const ulong Mask =              0xFFFFFF000000FFFF;
        private const ulong VersionMask =       0x000000FFF0000000;
        private const ulong MetadataMask =      0x000000000FFF0000;

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// The software version this signature was created on
        /// </summary>
        public ushort Version { get; set; }

        /// <summary>
        /// The length of the metadata
        /// </summary>
        public ushort MetadataLength { get; set; } 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new signature
        /// </summary>
        /// <param name="version">The software version</param>
        /// <param name="metadataLength">The length of the metadata</param>
        public FileSignature(ushort version, ushort metadataLength)
        {
            Version = version;
            MetadataLength = metadataLength;
        }

        /// <summary>
        /// Initializes the object based on an existing file signature
        /// </summary>
        /// <param name="signature">The original signature</param>
        public FileSignature(ulong signature)
        {
            if ((signature & Mask) != EmptySignature)
                throw new ArgumentException("This is not a valid file signature", nameof(signature));

            Version = (ushort)((signature & VersionMask) >> 28); // 12+16
            MetadataLength = (ushort)((signature & MetadataMask) >> 16); // 16
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the signature to UInt64
        /// </summary>
        /// <returns>Signature as UInt64</returns>
        public ulong ToUInt64()
        {
            var result = EmptySignature;
            result |= ((ulong)(Version & 0x0FFF)) << (28); // 12 + 16
            result |= ((ulong)(MetadataLength & 0x0FFF)) << (16); // 16
            return result;
        }

        /// <summary>
        /// Converts the signature to bytes
        /// </summary>
        /// <returns>Signature as bytes</returns>
        public byte[] ToBytes()
        {
            return BitConverter.GetBytes(ToUInt64());
        } 

        #endregion
    }
}
