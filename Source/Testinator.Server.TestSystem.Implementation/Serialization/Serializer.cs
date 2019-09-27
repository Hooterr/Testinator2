using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Testinator.TestSystem.Implementation.Serialization
{
    public static class Serializer
    {
        public static byte[] Serialize(object serializableObject)
        {
            var formatter = new BinaryFormatter();
            byte[] result;
            using (var ms = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(ms, serializableObject);
                }
                catch (SerializationException e)
                {
                    //Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                result = ms.ToArray();
            }
            return result;
        }
    }
}
