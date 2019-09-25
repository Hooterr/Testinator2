using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Files
{
    internal static class FileInfoExtensions
    {
        internal static FileHeader GenerateHeader(this FileContext info, MetadataEncoder encoder)
        {
            var metadataBytes = encoder.Encode(info.Metadata);
            var signature = new FileSignature((ushort)info.Version, (ushort)metadataBytes.Length);
            var header = new FileHeader(signature, metadataBytes);
            return header;
        }
    }
}
