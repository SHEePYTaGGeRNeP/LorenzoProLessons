using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Assets.Editor.Tests
{
    class HowDoIProgram
    {
        [Test]
        public void Appels_LINQ_ANY_ALL()
        {
            int[] i = { 5, 7, 9 };
            Assert.IsTrue(i.Any(x => x > 5));
            i = new int[0];
            Assert.IsFalse(i.Any(x => x > 5));
            Assert.IsTrue(i.All(x => x > 5));
        }
    }
}
