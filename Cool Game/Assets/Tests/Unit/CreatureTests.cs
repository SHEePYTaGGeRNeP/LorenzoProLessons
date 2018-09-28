using NUnit.Framework;
using Unit;

namespace Tests.Unit
{
    public class CreatureTests
    {      
        
        [Test]
        public void Can_Create()
        {
            Creature c = new Creature(10,10);
            Assert.IsNotNull(c);
        }
        
        
    }
}