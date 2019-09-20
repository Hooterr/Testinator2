using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Testinator.Server.Serialization
{
    public interface ISerializer<T>
    {
        byte[] Serialize(T @object);

        T Deserialize(Stream serializationStream);
    }
}
