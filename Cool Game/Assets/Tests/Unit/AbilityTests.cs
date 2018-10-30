using System;
using NUnit.Framework;
using Unit;

namespace Tests.Unit
{
    public class AbilityTests
    {
        [Test]
        public void Can_Create()
        {
            Ability a = new Ability(String.Empty, null);
            Assert.IsNotNull(a);
        }
    }
}