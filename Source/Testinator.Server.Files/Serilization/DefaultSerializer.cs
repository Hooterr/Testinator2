using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Testinator.Server.Files
{
    internal class DefaultSerializer<T> : ISerializer<T>
        where T : class
    {
        public T Deserialize(Stream serializationStream)
        {
            T result;
            try
            {
                var formatter = new BinaryFormatter();
                result = (T)formatter.Deserialize(serializationStream);
            }
            catch(Exception ex)
            {
                throw new SerializationException("Could not deserialize the object. For additional check the inner exception.", ex);
            }

            return result;
        }

        public byte[] Serialize(T @object)
        {
            byte[] result = null;
            MemoryStream ms = null;
            Exception exception = null;
            try
            {
                ms = new MemoryStream();          
                var formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(ms, @object);
                    result = ms.ToArray();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }    
                
            }
            finally
            {
                if (ms != null)
                    ms.Dispose();
            }

            return exception == null ? result : throw new SerializationException("This object cannot be serialized. Look at inner exception for more details", exception);
        }
    }
}
