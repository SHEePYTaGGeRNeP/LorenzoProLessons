using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Editor.Tests.Helpers
{
    class ExtensionMethodsTests
    {
        [Test]
        public void IsAbout()
        {
            Assert.IsTrue((-1f).IsAbout(-1.000001f, 0.001f));
            Assert.IsTrue((1f).IsAbout(1.000001f, 0.001f));
            Assert.IsFalse(Double.PositiveInfinity.IsAbout(1.000001f));
            Assert.IsFalse(Double.PositiveInfinity.IsAbout(0f));
            Assert.IsTrue(0f.IsAbout(0f));
            Assert.IsTrue(10f.IsAbout(30f, 30f));
        }
        [Test]
        public void MoreThan()
        {
            Assert.IsTrue((-1f).IsMoreThan(-1.000001f, 0.00000000001f));
            Assert.IsFalse((-1f).IsMoreThan(-1.000001f, 0.001f));
            Assert.IsFalse((1f).IsMoreThan(1.000001f, 0.001f));
            Assert.IsTrue(Double.PositiveInfinity.IsMoreThan(1.000001f));
            Assert.IsTrue(Double.PositiveInfinity.IsMoreThan(0f));
            Assert.IsFalse(Double.NegativeInfinity.IsMoreThan(0f));
            Assert.IsFalse(0f.IsMoreThan(0f));
            Assert.IsFalse(40f.IsMoreThan(30f, 30f));
            Assert.IsTrue(40f.IsMoreThan(30f, 3f));
        }

        [Test]
        public void CountDigits()
        {
            Assert.AreEqual(1, 1.NumberOfDigits());
            Assert.AreEqual(1, (-1).NumberOfDigits());
            Assert.AreEqual(1, 0.NumberOfDigits());
            Assert.AreEqual(2, 10.NumberOfDigits());
            Assert.AreEqual(3, 100.NumberOfDigits());
            Assert.AreEqual(3, 313.NumberOfDigits());
        }


        [Serializable]
        private class TestClass
        {
            public string Test { get; set; }
            public TestClass(string test)
            {
                Test = test;
            }
        }

        [Test]
        public void DeepClone()
        {
            const string test_string = "Hello";
            TestClass tc = new TestClass(test_string);
            TestClass result = tc.DeepClone();
            Assert.AreEqual(tc.Test, result.Test);
        }

        [Test]
        public void EnumeratedType()
        {
            List<int> ints = new List<int>();
            Assert.AreEqual(typeof(int), ints.GetType().GetEnumeratedType());
            int[] intsA = new int[1];
            Assert.AreEqual(typeof(int), intsA.GetType().GetEnumeratedType());
        }
        enum TestE { A };
        [Test]
        public void TestEnumFullName()
        {
            TestE e = TestE.A;
            Assert.AreEqual("TestE.A", e.GetFullName());
        }
        [Test]
        public void Enumerable_IsNullOrEmpty()
        {
            Assert.IsTrue(new int[0].IsNullOrEmpty());
            List<int> test = null;
            Assert.IsTrue(test.IsNullOrEmpty());
            Assert.IsFalse(new int[1].IsNullOrEmpty());
            Assert.IsTrue(new List<int>().IsNullOrEmpty());
        }
        [Test]
        public void Bytes_ToStringDecimal()
        {
            byte[] bytes = new byte[] { 0x9, 0x3, 0xF, 0x13, Byte.MaxValue };
            Assert.AreEqual("09 03 15 19 255", bytes.ToStringDecimals());
        }

        [Test]
        public void StringBuilder_Prepend()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("b");
            sb.Prepend("a");
            Assert.AreEqual("ab", sb.ToString());
        }

        [Test]
        public void IEnumerable_FirstIndexOfExt()
        {
            IEnumerable<int> ints = new int[] { 1, 2, 3, 4, 5 }.Where(i => i > 2);
            Assert.AreEqual(1, ints.FirstIndexOfExt((i) => i == 4));
        }

        [Test]
        public void Dictionary_All()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>
            {
                { 1, String.Empty },
                { 2, String.Empty },
                { 3, String.Empty }
            };
            dict.RemoveAll((key, value) => key < 3);
            Assert.AreEqual(1, dict.Count);
            Assert.AreEqual(String.Empty, dict[3]);        
        }

        [Test]
        public void GetDecimalCount()
        {
            Assert.AreEqual(1, (1.1m).GetDecimalCount(true));
            Assert.AreEqual(2, (1.11m).GetDecimalCount(true));
            Assert.AreEqual(1, (-1.1m).GetDecimalCount(true));
            Assert.AreEqual(0, (1.0m).GetDecimalCount(true));
            Assert.AreEqual(1, (1.0m).GetDecimalCount(false));
            Assert.AreEqual(1, (31.0m).GetDecimalCount(false));
        }
        [Test]
        public void Truncate()
        {
            const string test = "Hello world";
            Assert.AreEqual(test, test.Truncate(Int32.MaxValue));
            Assert.AreEqual(test.Substring(0,1), test.Truncate(1));
            Assert.AreEqual(test.Substring(0,5), test.Truncate(5));
            Assert.AreEqual(test, test.Truncate(30));
        }
    }
}
