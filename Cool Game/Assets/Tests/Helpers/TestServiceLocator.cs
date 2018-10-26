using System;
using System.CodeDom;
using System.Collections.Generic;
using Helpers.Classes;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Helpers
{
    public class TestServiceLocator
    {
        [Test]
        public void Test()
        {
            try
            {
                ServiceLocator.Reset();
                Assert.IsTrue(AddService<IA>(new TestA(), false));
                Assert.IsTrue(AddService<IB>(new TestB(), false));
                Assert.IsNotNull(ServiceLocator.GetService<IA>());
                Assert.IsNotNull(ServiceLocator.GetService<IB>());
                ServiceLocator.Reset();
                Assert.IsTrue(AddService<IB>(new TestAB(), false));
                Assert.IsNotNull(ServiceLocator.GetService<IA>());
                Assert.IsNotNull(ServiceLocator.GetService<IB>());

                ServiceLocator.Reset();
                Assert.IsTrue(AddService<IA>(new TestA(), false));
                Assert.IsTrue(AddService<IB>(new TestB(), false));
                Assert.IsTrue(AddOrReplaceService<IA>(new TestA(), false));
                Assert.IsTrue(AddOrReplaceService<IB>(new TestB(), false));

                // TestAB implements IA and IB so replacing fails;
                Assert.IsTrue(AddOrReplaceService<IB>(new TestAB(), true));
                Assert.IsTrue(AddOrReplaceService<IA>(new TestAB(), true));

                ServiceLocator.RemoveService<IA>();
                // TestB exists, but TestAB also implements IB                
                //so that gets replaced, eventhough TestB doesn't implement IA;
                Assert.IsTrue(AddOrReplaceService<IA>(new TestAB(), true));
                Assert.IsNotNull(ServiceLocator.GetService<IA>());
                Assert.IsNotNull(ServiceLocator.GetService<IB>());

                Assert.IsTrue(AddService<IA>(new TestA(), true));
                Assert.IsTrue(AddService<IB>(new TestB(), true));
                ServiceLocator.RemoveService<IA>();
            }
            catch (Exception ex)
            {
                if (ex is AssertionException)
                {
                    Assert.Fail();
                }
                Debug.Log(ex);
                Assert.Fail();
            }
            finally
            {
                ServiceLocator.Reset();
            }
        }

        /// <returns>failed</returns>
        private static bool AddService<T>(T service, bool expectException)
        {
            try
            {
                ServiceLocator.AddService(service);
                if (!expectException)
                    return true;
                Debug.Log("Expected exception, but service got added anyway");
                return false;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case KeyNotFoundException _:
                        if (!expectException)
                        {
                            Debug.Log(ex);
                            return false;
                        }
                        Debug.Log("Correctly failed to remove " + typeof(T).Name);
                        return true;
                    case ServiceLocator.ServiceAlreadyExistsException _:
                        if (!expectException)
                        {
                            Debug.Log(ex);
                            return false;
                        }
                        Debug.Log($"Correctly failed to add {service} because {typeof(T).Name} already exsits");
                        return true;
                    case ServiceLocator.MultipleServicesAlreadyExistsException _:
                        if (!expectException)
                        {
                            Debug.Log(ex);
                            return false;
                        }
                        Debug.Log(
                            $"Correctly failed to add {service} because multiple {typeof(T).Name} already exsits");
                        return true;
                }
                Debug.Log(ex);
                return false;
            }
        }

        /// <returns>failed</returns>
        private static bool AddOrReplaceService<T>(T service, bool expectException) where T : class
        {
            try
            {
                ServiceLocator.AddOrReplaceService<T>(service);
                if (!expectException)
                    return true;
                Debug.Log("Expected exception, but service got added anyway");
                return false;
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                {
                    if (!expectException)
                    {
                        Debug.Log(ex);
                        return false;
                    }
                    Debug.Log("Correctly failed to add " + typeof(T).Name);
                    return true;
                }
                Debug.Log(ex);
                return false;
            }
        }
    }
}