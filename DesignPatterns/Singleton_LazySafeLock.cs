namespace DesignPatterns
{
    public class Singleton_LazySafeLock
    {
        private static Singleton_LazySafeLock? _instance;

        // Locker field.
        private static readonly object _instanceLock = new object();

        // Accessible properties.
        public string DatabaseName { get; private set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;

        // Single point of entry (no explicit setter).
        public static Singleton_LazySafeLock Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton_LazySafeLock()
                        {
                            DatabaseName = "mockDatabaseName",
                            Host = "localhost",
                            Port = "5432"
                        };
                    }
                    return _instance;
                }
            }
        }

        private Singleton_LazySafeLock()
        {
        }
    }
}