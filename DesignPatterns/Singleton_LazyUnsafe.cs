namespace DesignPatterns
{
    public class Singleton_LazyUnsafe
    {
        private static Singleton_LazyUnsafe? _instance;

        // Accessible properties.
        public string DatabaseName { get; private set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;

        // Single point of entry (no explicit setter).
        public static Singleton_LazyUnsafe Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Singleton_LazyUnsafe()
                    {
                        DatabaseName = "mockDatabaseName",
                        Host = "localhost",
                        Port = "5432"
                    };
                }
                return _instance;
            }
        }

        private Singleton_LazyUnsafe()
        {
        }
    }
}