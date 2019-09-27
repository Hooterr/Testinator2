namespace Testinator.Server.Serialization
{
    /// <summary>
    /// Creates serializers
    /// </summary>
    public static class SerializerFactory
    {
        /// <summary>
        /// Creates a new serializer
        /// </summary>
        /// <typeparam name="T">The type of object the serializer will be working with</typeparam>
        /// <returns>Configured serializer</returns>
        public static ISerializer<T> New<T>()
            where T : class
        {
            return new DefaultSerializer<T>();
        }
    }
}
