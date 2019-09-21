using System.Linq;

namespace Testinator.Files
{
    internal class FileHeader
    {
        public FileSignature Signature { get; private set; }

        public byte[] Metadata { get; private set; }

        public FileHeader(FileSignature signature, byte[] metadataBytes = null)
        {
            Signature = signature;
            Metadata = metadataBytes;
        }

        public byte[] GetAllBytes()
        {
            return Signature.ToBytes().Concat(Metadata).ToArray();
        }
    }
}
