using System;

namespace BZ.Core.Collections
{
    public class PriorityQueue<T> : IDisposable
    {
        public static PriorityQueue<T> Create<T> (int size = 10) where T : IComparable
        {
            return new PriorityQueue<T>((x, y) => { return x.CompareTo(y); }, size);
        }

        public static PriorityQueue<T> Create (Func<T, T, int> compareFunction, int size = 10)
        {
            return new PriorityQueue<T>(compareFunction, size);
        }

        private T[] queue;

        private readonly Func<T, T, int> compareFunction;

        public int Count { get; private set; }


        private PriorityQueue (Func<T, T, int> compareFunction, int size = 10)
        {
            this.compareFunction = compareFunction;
            this.queue = new T[size];
            this.Count = 0;
        }

        public void Clear ()
        {
            this.queue = new T[10];
            this.Count = 0;
        }

        public PriorityQueue<T> Clone ()
        {
            var newVector = new T[this.queue.Length];

            var newQueue = new PriorityQueue<T>(this.compareFunction, this.queue.Length);

            for ( var i = 1; i <= this.Count; i++ )
            {
                newVector[i] = this.queue[i];
            }

            newQueue.queue = newVector;
            newQueue.Count = this.Count;

            return newQueue;
        }

        public T[] ToArray ()
        {
            var cloned = Clone();
            var newVector = new T[Count];
            var clonedLength = cloned.Count;
            for ( var i = 0; i < clonedLength; i++ )
            {
                newVector[i] = cloned.Dequeue();
            }

            return newVector;
        }

        public T Peek ()
        {
            return this.queue[1];
        }

        public T Dequeue ()
        {
            if ( this.Count == 0 )
                return default(T);

            var min = this.queue[1];

            this.queue[1] = this.queue[Count--];
            this.Sink(1);

            return min;
        }

        public void Sort ()
        {
            this.Sink(1);
        }

        private void Sink (int position)
        {
            var aux = this.queue[position];

            var son = ( position * 2 );
            var isHeap = false;

            while ( son <= this.Count && !isHeap )
            {
                if ( son != this.Count && compareFunction(this.queue[son + 1], this.queue[son]) < 0 ) son++;

                if ( compareFunction(this.queue[son], aux) < 0 )
                {
                    this.queue[position] = this.queue[son];
                    position = son;
                    son = ( position * 2 );
                }
                else
                {
                    isHeap = true;
                }
            }
            this.queue[position] = aux;
        }

        public PriorityQueue<T> EnqueueArray (T[] values)
        {
            var localLength = values.Length;
            for ( int n = 0; n < localLength; n++ ) Enqueue(values[n]);
            return this;
        }

        public PriorityQueue<T> Enqueue (T value)
        {
            if ( this.Count == this.queue.Length - 1 ) DuplicateQueueSize();

            var hole = ++this.Count;

            while ( hole > 1 && compareFunction(value, this.queue[hole / 2]) < 0 )
            {
                this.queue[hole] = this.queue[hole / 2];
                hole = hole / 2;
            }

            this.queue[hole] = value;

            return this;
        }

        private void DuplicateQueueSize ()
        {
            var newQueue = new T[this.Count + 1 * 2];
            for ( int i = 1; i <= this.Count; i++ )
            {
                newQueue[i] = queue[i];
            }
            this.queue = newQueue;
        }

        public bool IsEmpty ()
        {
            return this.Count == 0;
        }

        public override string ToString ()
        {
            PriorityQueue<T> clonedQueue = this.Clone();
            String res = "[";
            while ( clonedQueue.Count > 1 )
            {
                res += clonedQueue.Dequeue();
                res += ",";
            }
            res += clonedQueue.Dequeue() + "]";

            return res;
        }

        public void Dispose ()
        {
            this.queue = null;
        }
    }
}
