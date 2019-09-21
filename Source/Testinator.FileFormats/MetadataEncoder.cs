using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Testinator.Files
{
    internal class MetadataEncoder
    {

        /* 
         * 
         * key value pairs
         * 
         * <key> 0x1f <value> 0x1e 
         * 
         */

        private const byte KeyValueSeparator = 0x1f;
        private const byte PairSeparator = 0x1e;

        public IDictionary<string, string> Parse(byte[] bytes)
        {
            var pairs = new Dictionary<string, string>();

            var startIdx = 0;
            var lookingForKey = true;
            var key = "";
            var value = "";
            for(var i = 0; i < bytes.Length; i++)
            {

                if(lookingForKey && bytes[i] == KeyValueSeparator)
                {
                    key = Encoding.UTF8.GetString(bytes.Skip(startIdx).Take(i - startIdx).ToArray());
                    lookingForKey = false;
                    startIdx = i + 1;
                }
                else if (bytes[i] == PairSeparator)
                {
                    value = Encoding.UTF8.GetString(bytes.Skip(startIdx).Take(i - startIdx).ToArray());
                    lookingForKey = true;
                    startIdx = i + 1;
                    pairs.Add(key, value);
                }
            }

            return pairs;
        }

        public byte[] Encode(IReadOnlyDictionary<string, string> metadata)
        {
            byte[] bytes;
            using(var ms = new MemoryStream())
            {
                foreach (var pair in metadata)
                {
                    var keyBytes = Encoding.UTF8.GetBytes(pair.Key);
                    ms.Write(keyBytes, 0, keyBytes.Length);
                    ms.Write(new byte[] { KeyValueSeparator }, 0, 1);
                    var valueBytes = Encoding.UTF8.GetBytes(pair.Value);
                    ms.Write(valueBytes, 0, valueBytes.Length);
                    ms.Write(new byte[] { PairSeparator }, 0, 1);
                }

                bytes = ms.ToArray();
            }

            return bytes;
        }
    }
}
