namespace DesignPatterns
{
    public class MockData
    {
        // Details.
        public string DatabaseName { get; private set; } = "mockedDatabaseName";
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
    }

    public class Singleton_LazySafe
    {
        // This implementation eliminates the need for a static constructor.
        // As the default mode of Lazy<T> is thread-safe.
        private static readonly Lazy<Singleton_LazySafe> _lazyInstance = new Lazy<Singleton_LazySafe>(
            () => new Singleton_LazySafe(new MockData()) { });

        // Accessible properties.
        public MockData Data { get; private set; }

        // Single point of entry (no explicit setter).
        public static Singleton_LazySafe Instance { get { return _lazyInstance.Value; } }

        private Singleton_LazySafe(MockData input)
        {
            Data = input;
            Data.Host = "localhost";
            Data.Port = "1234";
        }
    }
}