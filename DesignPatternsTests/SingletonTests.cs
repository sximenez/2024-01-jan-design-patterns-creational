using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatterns.Tests
{
    [TestClass()]
    public class SingletonTests
    {
        [TestMethod()]
        public void Singleton_NotThreadSafe_Should_Create_Single_Instance_When_Called()
        {
            var instance1 = Singleton_LazyUnsafe.Instance;
            var instance2 = Singleton_LazyUnsafe.Instance;
            Console.WriteLine($"{instance1.Host}\n{instance2.Host}");

            instance1.Host = "helloworld";
            Console.WriteLine($"{instance1.Host}\n{instance2.Host}");

            Assert.AreEqual(instance1.Host, instance2.Host);
        }

        [TestMethod()]
        public void Singleton_ThreadSafeNoLock_Should_Create_Single_Instance_When_Called()
        {
            var instance1 = Singleton_EagerSafeStatic.Instance;
            var instance2 = Singleton_EagerSafeStatic.Instance;

            Console.WriteLine($"{instance1.Host}\n{instance2.Host}");

            instance1.Host = "helloworld";
            Console.WriteLine($"{instance1.Host}\n{instance2.Host}");

            Assert.AreEqual(instance1.Host, instance2.Host);
        }

        [TestMethod()]
        public void Singleton_LazySafe_Should_Create_Single_Instance_When_Called()
        {
            var instance1 = Singleton_LazySafe.Instance;
            var instance2 = Singleton_LazySafe.Instance;
            Console.WriteLine(instance1.Data.Port);

            instance2.Data.Port = "5678";
            Console.WriteLine(instance1.Data.Port);
        }
    }
}