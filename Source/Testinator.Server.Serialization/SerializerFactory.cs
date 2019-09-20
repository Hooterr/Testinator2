namespace Testinator.Server.Serialization
{
    public static class SerializerFactory
    {
        public static ISerializer<T> New<T>()
            where T : class
        {
            return new Serializer<T>();
        }
    }
}
