using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.FileFormats
{
    internal static class FileInfoExtensions
    {
        internal static FileHeader GenerateHeader(this FileInfo info)
        {
            var metadataBytes = new MetadataEncoder().Encode(info.Metadata);
            var signature = new FileSignature((ushort)info.Version, (ushort)metadataBytes.Length);
            var header = new FileHeader(signature, metadataBytes);
            return header;
        }
    }
}
