using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

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
            Quaternion q = Quaternion.Euler(expected);

            Assert.IsTrue(expected.Equals(q.eulerAngles));
        }
        
    }

}