using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testinator.Files
{
    internal class FileSignature
    {
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

        public ushort Version { get; set; }
        
        public ushort MetadataLength { get; set; }

        public FileSignature() { }

        public FileSignature(ushort version, ushort metadataLength)
        {
            Version = version;
            MetadataLength = metadataLength;
        }

        public FileSignature(ulong signature)
        {
            if ((signature & Mask) != EmptySignature)
                throw new ArgumentException("This is not a valid file signature", nameof(signature));

            Version = (ushort)((signature & VersionMask) >> 28); // 12+16
            MetadataLength = (ushort)((signature & MetadataMask) >> 16); // 16
        }

        public ulong ToUInt64()
        {
            var result = EmptySignature;
            result |= ((ulong)(Version & 0x0FFF)) << (28); // 12 + 16
            result |= ((ulong)(MetadataLength & 0x0FFF)) << (16); // 16
            return result;
        }

        public byte[] ToBytes()
        {
            return BitConverter.GetBytes(ToUInt64());
        }
    }
}
