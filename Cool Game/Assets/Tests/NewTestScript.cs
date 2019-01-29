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
using Assets.Scripts.SavingAndLoading;
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
            string s = Int32.MaxValue.ToRacePositionText();
            string s2 = ExtensionMethodsGeneral.ToRacePositionText(Int32.MaxValue);
        }

        [Test]
        public void TestExtension()
        {
            string a = "test";
            a.Test();
            Assert.AreEqual("test", a);
            int i = 1;
            i.Test();
            Assert.AreEqual(1, i);
            Vector3 v = Vector3.zero;
            Vector2 xy = v.Xy();
            Vector2 xy2 = ExtensionMethodsUnity.Xy(v);

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
            Debug.Log(expected - q.eulerAngles);
            Assert.AreEqual(expected, q.eulerAngles);


        }
        private class GenericList<T, TY, U> where TY: U
        {
            List<T> list;
        }
        private void tetst()
        {
            GenericList<int, string, object> genericList = new GenericList<int, string, object>();
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
                LogHelper.Log(typeof(Animal), "Nomnomnom");
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

        [Serializable]
        private class A
        {
            public string Hello { get; set; }
        }
        [Serializable]
        private class B : A
        {
            public int Test { get; set; }
        }

        [Test]
        public void TestPolymorphismSaveLoadDisk()
        {
            const string FILE_NAME = "test.dat";
            B b = new B() { Hello = "hello", Test = 1 };
            SaveLoadObject.SaveObject(b, FILE_NAME);
            B loaded = SaveLoadObject.LoadObjectOfType<B>(FILE_NAME);
            if (File.Exists(FILE_NAME))
                File.Delete(FILE_NAME);
            Assert.AreEqual(b.Hello, loaded.Hello);
            List<A> list = new List<A>();
            list.Add(new B() { Hello = " ha" });
            SaveLoadObject.SaveObject(list, FILE_NAME);
            List<A> listLoaded = SaveLoadObject.LoadObjectOfType<List<A>>(FILE_NAME);
            Assert.IsTrue(listLoaded[0] is B);
            if (File.Exists(FILE_NAME))
                File.Delete(FILE_NAME);
        }
    }
}