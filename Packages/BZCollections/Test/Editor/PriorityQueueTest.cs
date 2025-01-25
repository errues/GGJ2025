using NUnit.Framework;

namespace BZ.Core.Collections.Test
{
    public class PriorityQueueTest
    {
        [Test]
        public void PriorityQueueGenericComparer ()
        {
            var queue = PriorityQueue<int>.Create<int>();

            queue.Enqueue(5);
            queue.Enqueue(3);
            queue.Enqueue(6);
            queue.Enqueue(8);
            queue.Enqueue(2);

            var data = queue.Dequeue();
            var data2 = queue.Dequeue();
            var data3 = queue.Dequeue();
            var data4 = queue.Dequeue();

            Assert.AreEqual(2, data);
            Assert.AreEqual(3, data2);
            Assert.AreEqual(5, data3);
            Assert.AreEqual(6, data4);
        }

        [Test]
        public void PriorityQueueCustomComparer ()
        {
            var queue = PriorityQueue<int>.Create((a, b) => { return a - b; });

            queue.Enqueue(5);
            queue.Enqueue(3);
            queue.Enqueue(6);
            queue.Enqueue(8);
            queue.Enqueue(2);

            var data = queue.Dequeue();
            var data2 = queue.Dequeue();
            var data3 = queue.Dequeue();
            var data4 = queue.Dequeue();

            Assert.AreEqual(2, data);
            Assert.AreEqual(3, data2);
            Assert.AreEqual(5, data3);
            Assert.AreEqual(6, data4);
        }

    }
}
