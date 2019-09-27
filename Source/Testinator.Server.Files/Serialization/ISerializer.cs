using System.IO;

namespace Testinator.Server.Files
{
    /// <summary>
    /// Handles serialization
    /// </summary>
    /// <typeparam name="T">The type of object to serialize</typeparam>
    public interface ISerializer<T>
    {
        /// <summary>
        /// Serializes an object
        /// </summary>
        /// <param name="object">The object to serialize</param>
        /// <returns>Serialization result as bytes</returns>
        byte[] Serialize(T @object);

        /// <summary>
        /// Deserializes an object
        /// </summary>
        /// <param name="serializationStream">Bytes stream</param>
        /// <returns>Recreated object</returns>
        T Deserialize(Stream serializationStream);
    }
}
