using System;
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
                AddService<IA>(new TestA(), false);
                AddService<IB>(new TestB(), false);
                ServiceLocator.GetService<IA>().A();
                ServiceLocator.GetService<IB>().A();
                AddService<IB>(new TestAB(), true);
                AddOrReplaceService<IA>(new TestAB(), false);
                AddOrReplaceService<IB>(new TestAB(), false);

                ServiceLocator.RemoveService<IA>();
                // TestAB is removed
                AddService<IA>(new TestA(), false);
                ServiceLocator.RemoveService<IA>();
                // TestAB implements IB, which exists
                AddService<IA>(new TestAB(), true);
                ServiceLocator.RemoveService<IA>();
                AddService(new TestAB(), true);
                ServiceLocator.RemoveService<IA>();
                AddService<IB>(new TestAB(), true);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        private static void AddService<T>(T service, bool expectException)
        {
            try
            {
                ServiceLocator.AddService(service);
                if (expectException)
                    Debug.Log("Expected exception, but service got added anyway");
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                {
                    if (!expectException)
                        Debug.Log(ex);
                    else
                        Debug.Log("Correctly failed to add " + typeof(T).Name);
                }
                else
                    Debug.Log(ex);
            }
        }
        private static void AddOrReplaceService<T>(T service, bool expectException)
        {
            try
            {
                ServiceLocator.AddOrReplaceService(service);
                if (expectException)
                    Debug.Log("Expected exception, but service got added anyway");
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                {
                    if (!expectException)
                        Debug.Log(ex);
                    else
                        Debug.Log("Correctly failed to addOrReplace " + typeof(T).Name);
                }
                else
                    Debug.Log(ex);
            }
        }
    }
}