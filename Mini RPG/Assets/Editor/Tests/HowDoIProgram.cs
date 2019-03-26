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

        /// <summary>I thought event would only allow you to subscribe once with the same method</summary>
        [Test]
        public void Actions_vs_Events_MultipleSubscribe()
        {
            EventClass ec = new EventClass();
            int count = 0;
            void TestAction()
            {
                count++;
            }
            ec.SubscribeMeAction += TestAction;
            ec.SubscribeMeAction += TestAction;
            ec.SubscribeMeAction += TestAction;
            ec.SubscribeMeAction.Invoke();
            Assert.AreEqual(3, count);
            count = 0;
            ec.SubscribeMeActionEvent += TestAction;
            ec.SubscribeMeActionEvent += TestAction;
            ec.SubscribeMeActionEvent += TestAction;
            ec.Invoke();
            Assert.AreEqual(3, count);
            count = 0;
            void TestHandler(object sender, EventArgs e)
            {
                count++;
            }
            ec.SubscribeMeEvent += TestHandler;
            ec.SubscribeMeEvent += TestHandler;
            ec.SubscribeMeEvent += TestHandler;
            ec.InvokeEvent();
            Assert.AreEqual(3, count);
        }
        private class EventClass
        {
            public event Action SubscribeMeActionEvent;
            public event EventHandler SubscribeMeEvent;
            public Action SubscribeMeAction;
            public void Invoke() => this.SubscribeMeActionEvent.Invoke();
            public void InvokeEvent() => this.SubscribeMeEvent.Invoke(this, EventArgs.Empty);

        }
        
    }
}
