using System.Collections;
using NUnit.Framework;
using Units;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Units
{
    class UnitTests
    {
        [UnityTest]
        public IEnumerator Can_Create_Unit()
        {
            Unit unit = new GameObject().AddComponent<Unit>();
            yield return null;
            Assert.IsNotNull(unit);
        }
    }
}
