using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Handles metadata encoding and decoding
    /// </summary>
    internal class MetadataEncoder
    {

        /* 
         * key value pairs
         * 
         * <key> 0x1f <value> 0x1e 
         * 
         */

        #region Private Members

        private const byte KeyValueSeparator = 0x1f;
        private const byte PairSeparator = 0x1e;

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses metadata
        /// </summary>
        /// <param name="bytes">Metadata bytes</param>
        /// <returns>Converted key values dictionary</returns>
        public IDictionary<string, string> Parse(byte[] bytes)
        {
            var pairs = new Dictionary<string, string>();

            var startIdx = 0;
            var lookingForKey = true;
            var key = "";
            var value = "";
            for (var i = 0; i < bytes.Length; i++)
            {

                if (lookingForKey && bytes[i] == KeyValueSeparator)
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

        /// <summary>
        /// Converts metadata to bytes
        /// </summary>
        /// <param name="metadata">Key value dictionary containing the metadata</param>
        /// <returns>Metadata converted to bytes</returns>
        public byte[] Encode(IReadOnlyDictionary<string, string> metadata)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
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

        #endregion   

        #region Construction

        /// <summary>
        /// Default constructor
        /// </summary>
        private MetadataEncoder() { }

        /// <summary>
        /// Gets a default encoder
        /// </summary>
        public static MetadataEncoder Default => new MetadataEncoder(); 
        
        #endregion
    }
}
