# Design patterns: creational

<!--TOC-->
  - [Singleton](#singleton)
    - [Pros](#pros)
    - [Cons](#cons)
    - [Singleton: lazy unsafe](#singleton-lazy-unsafe)
    - [Singleton: lazy safe (locked)](#singleton-lazy-safe-locked)
    - [Singleton: eager safe (static constructor)](#singleton-eager-safe-static-constructor)
    - [Singleton: lazy safe (universal)](#singleton-lazy-safe-universal)
<!--/TOC-->

Creational patterns deal with situations where object creation is involved.

## Singleton

- One of the best-known patterns.
- A class that can be instantiated only once.
- For this, its constructor is private and parameterless.
- Its members are `static` (only one copy).
- The class can be `sealed` for better performance.
- It relies on a public static property `Instance` for access.

```mermaid
graph TB
Singleton
```

### Pros

- By having a single instance of a class (a single point of access),
- Certain resources can be managed better:
    - E.g. files, databases, logs, configuration settings.

### Cons

- However, by blocking instantiation to one object, a singleton introduces global state.
- Meaning any part of the code can potentially modify the values of the single object.
- This is undesired in general design.
- As it makes the program unpredictable (and unit testing harder).
- Singletons should be used carefully as a result.

### Singleton: lazy unsafe

Threads are the smallest units of execution within a process.

Nowadays, environments are multi-threaded.

Meaning multiple threads can execute concurrently within a single process, sharing the same memory space.

This is not good for a simple singleton as it can lead to the creation of more than one instance of the class.

```csharp
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
```

```csharp
// Unit test.

[TestMethod()]
public void Singleton_NotThreadSafe_Should_Create_Single_Instance_When_Called()
{
    var instance1 = Singleton_NotThreadSafe.Instance;
    var instance2 = Singleton_NotThreadSafe.Instance;
    Console.WriteLine($"{instance1.Host}\n{instance2.Host}");

    instance1.Host = "helloworld";
    Console.WriteLine($"{instance1.Host}\n{instance2.Host}");

    Assert.AreEqual(instance1.Host, instance2.Host);
}
```

```console
// Output

localhost
localhost
helloworld
helloworld
```

### Singleton: lazy safe (locked)

This implementation introduces the `lock` keyword.

This blocks access to one thread at a time.

However, this is memory-intensive.

```csharp
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
```

### Singleton: eager safe (static constructor)

Using a `static` constructor ensures that the single object is instantiated before any thread tries to access it.

This eliminates the need for a lock and guarantees only one object is created.

```csharp
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
```

### Singleton: lazy safe (universal)

In this version, we use `System.Lazy<T>` type, passed to the constructor using a `lambda` expression or nameless method `() => `.

We also encapsulate the data of the singleton in a separate class `MockData` that we pass to the constructor as an argument.

This way, we avoid modifying the Singleton class itself (SOLID Open/Closed principle).

```csharp
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
```