using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Editor.Tests.Helpers
{
    class UtilsTests
    {
        [Serializable]
        private struct TestStruct
        {
            public string Test { get; private set; }
            public TestStruct(string test)
            {
                Test = test;
            }
        }
        [Test]
        public void TestToBytesAndToStruct()
        {
            const string test_string = "Hello";
            TestStruct tc = new TestStruct(test_string);
            byte[] bytes = Utils.CastStructToBytes(tc);
            TestStruct result = Utils.CastBytesToStruct<TestStruct>(bytes);
            Assert.AreEqual(tc.Test, result.Test);
        }
    }
}
