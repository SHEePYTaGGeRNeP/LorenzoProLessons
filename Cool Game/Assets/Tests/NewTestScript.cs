using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Jobs;
using UnityEngine.Jobs;
using UnityEngine.Windows;

namespace Tests
{
    public class NewTestScript
    {
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions.
            Assert.IsTrue(true);
            Assert.AreEqual(1, 1);
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }

        [Test]
        public void TestQuaternion()
        {
            Vector3 expected = new Vector3(0, 10, 0);
            Vector3 expected2 = new Vector3(0, 10, 0);
            Assert.IsTrue(expected.Equals(expected2));
            Assert.AreEqual(expected, expected2);
            Quaternion q = Quaternion.Euler(expected);
            //Debug.Log($"q {q.eulerAngles}");
            //Debug.Log($"e {expected}");
            Assert.AreEqual(expected, q.eulerAngles);
            List<object> ls;
        }


        [Test]
        public void Testttt()
        {
            List<Animal> animals = new List<Animal>
            {
                new Beetle(),
                new Shark(),
                new Duck()
            };
            foreach (Animal a in animals)
                a.Eat();
            List<ISwimmable> swimmables = new List<ISwimmable>()
            {
                new Duck(),
                new Shark()
            };
            foreach (ISwimmable a in swimmables)
                a.Swim();
        }


        private abstract class Animal
        {
            public void Eat()
            {
                LogHelper.Log(typeof(Animal),"Nomnomnom");
            }
        }

        private interface ISwimmable
        {
            int SwimSpeed { get; set; }
            void Swim();
        }

        private interface IFlyable
        {
            void Fly();
        }

        private class Beetle : Animal
        {
        }

        private class Duck : Animal, ISwimmable, IFlyable
        {
            public int SwimSpeed { get; set; }

            public void Swim()
            {
            }

            public void Fly()
            {
                
            }
        }

        private class Shark : Animal, ISwimmable
        {
            public int SwimSpeed { get; set; }

            public void Swim()
            {
            }
        }
    }
}