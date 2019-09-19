#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0049 // Simplify Names
using System;
using System.IO;

namespace Demo
{
    public class FileHeader : IFileHeader
    { 
        public const UInt32 StartSequence = 0xddccbbaa;
        public const UInt32 EndSequence = 0xaabbccdd;

        public UInt32 HeaderLength { get; private set; }

        private byte[] mHeader;

        public void SetCustomHeader(IFileHeader header)
        {
            mHeader = header.GetBytes();
            HeaderLength = (UInt32)(mHeader.Length);
        }

        public byte[] GetBytes()
        {
            byte[] output;
            using (var ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes(StartSequence), 0, 4);
                ms.Write(BitConverter.GetBytes(HeaderLength), 0, 4);
                ms.Write(mHeader, 0, mHeader.Length);
                ms.Write(BitConverter.GetBytes(EndSequence), 0, 4);
                output = ms.ToArray();
            }
            return output;
        }
    }
}

#pragma warning restore IDE0049 // Simplify Names
#pragma warning restore IDE1006 // Naming Styles
