using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Jobs;
using UnityEngine.Jobs;

namespace Tests
{
    public class NewTestScript
    {
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions.
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
            Debug.Log($"q {q.eulerAngles}");
            Debug.Log($"e {expected}");
            Assert.AreEqual(expected, q.eulerAngles);

        }
    }
}