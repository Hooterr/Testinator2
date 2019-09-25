using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.Domain;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Extensions for <see cref="FileContext"/>
    /// </summary>
    internal static class FileContextExtensions
    {
        /// <summary>
        /// Generates a file header from file context
        /// </summary>
        /// <param name="context">The context</param>
        /// <param name="encoder">Encoder for the metadata</param>
        /// <returns>The file header</returns>
        internal static FileHeader GenerateHeader(this FileContext context, MetadataEncoder encoder)
        {
            var metadataBytes = encoder.Encode(context.Metadata);
            var signature = new FileSignature((ushort)context.Version, (ushort)metadataBytes.Length);
            var header = new FileHeader(signature, metadataBytes);
            return header;
        }
    }
}
