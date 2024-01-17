namespace DesignPatterns
{
    public class Singleton_EagerSafeStatic
    {
        // Eager mode: the single object is initialized even if not needed.
        // Readonly: assignment can only happen here or in the constructor.
        private static readonly Singleton_EagerSafeStatic? _instance = new Singleton_EagerSafeStatic()
        {
            DatabaseName = "mockDatabaseName",
            Host = "localhost",
            Port = "5432"
        };


        // Accessible properties.
        public string DatabaseName { get; private set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;

        // Single point of entry (no explicit setter).
        public static Singleton_EagerSafeStatic Instance
        {
            get
            {
                return _instance;
            }
        }

        // Static constructor: it guarantees only one object is created.
        static Singleton_EagerSafeStatic()
        {
        }

        private Singleton_EagerSafeStatic()
        {
        }
    }
}