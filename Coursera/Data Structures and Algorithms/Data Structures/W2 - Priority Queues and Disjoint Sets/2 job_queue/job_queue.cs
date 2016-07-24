using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.W2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process(JobQueue.Process);
        }
        private static void Process(Func<string[], string[]> process)
        {
            var input = new List<string>();
            string s;
            while ((s = Console.ReadLine()) != null)
            {
                input.Add(s);
            }

            foreach (var item in process(input.ToArray()))
            {
                Console.WriteLine(item);
            }
        }
    }
    public class JobQueue
    {
        public static string[] Process(string[] inputs)
        {
            var chars = new[] { ' ' };
            var splits0 = inputs[0].Split(chars);
            var threadCount = int.Parse(splits0[0]);
            var jobCount = int.Parse(splits0[1]);
            var jobs = inputs[1].Split(chars).Select(int.Parse).ToArray();

            if (jobs.Length != jobCount) throw new ArgumentOutOfRangeException();

            return AssignJobs(threadCount, jobs)
                .Select(pj => string.Format("{0} {1}", pj.ThreadId, pj.StartOfProcessing))
                .ToArray();
        }

        public static IEnumerable<Job> AssignJobs(int threadCount, int[] jobs)
        {
            var currentTime = 0;

            //Create Priority Queue
            var intialProcessingJobs = jobs
                .Take(threadCount)
                .Select((processingTime, id) => new Job(id, currentTime, processingTime))
                .ToArray();
            var queue = new MinBinaryHeap<Job>(intialProcessingJobs);

            //Process Results
            var results = new List<Job>();
            foreach (var processingTime in jobs.Skip(threadCount))
            {
                var job = queue.ExtractMax();
                results.Add(job);
                currentTime = job.EndOfProcessing;
                queue.Insert(new Job(job.ThreadId, currentTime, processingTime));
            }

            //Empty Queue
            while (queue.Size > 0)
            {
                results.Add(queue.ExtractMax());
            }
            return results;
        }

        //ThreadCount
        //Jobs
        //List ThreadId(0 based) - Start of Processing

        public class Job : IComparable<Job>
        {
            public Job(int id, int currentTime, int procesingTime)
            {
                ThreadId = id;
                StartOfProcessing = currentTime;
                ProcessingTime = procesingTime;
                EndOfProcessing = StartOfProcessing + ProcessingTime;
            }
            public int ThreadId { get; private set; }
            public int StartOfProcessing { get; private set; }
            public int ProcessingTime { get; private set; }
            public int EndOfProcessing { get; private set; }

            public int CompareTo(Job other)
            {
                //EndOfProcessing of processin then thread id
                return (EndOfProcessing == other.EndOfProcessing)
                    ? ThreadId.CompareTo(other.ThreadId)
                    : EndOfProcessing.CompareTo(other.EndOfProcessing);
            }
        }

        private class MinBinaryHeap<T> : BinaryHeap<T> where T : IComparable<T>
        {
            public MinBinaryHeap(T[] source) : base(source)
            {
                for (int i = source.Length / 2; i >= 0; i--)
                {
                    SiftDown(i);
                }
            }

            public override int SiftUp(int i)
            {
                var swapId = i;
                while (i > 0 && Value(ParentId(i)).CompareTo(Value(i)) > 0)
                {
                    swapId = ParentId(i);
                    Swap(i, swapId);
                    i = swapId;
                }
                return swapId;
            }

            public override int SiftDown(int i)
            {
                var maxIndex = i;

                var r = RightChildId(i);
                if (r < _size && Value(r).CompareTo(Value(maxIndex)) < 0) maxIndex = r;

                var l = LeftChildId(i);
                if (l < _size && Value(l).CompareTo(Value(maxIndex)) < 0) maxIndex = l;

                if (i != maxIndex)
                {
                    Swap(i, maxIndex);
                    return SiftDown(maxIndex);
                }
                return maxIndex;
            }
        }

        private abstract class BinaryHeap<T>
        {
            protected int _maxSize;
            protected int _size;
            private T[] H;

            protected BinaryHeap(T[] source)
            {
                H = source;
                _maxSize = source.Length;
                _size = source.Length;
            }

            protected int ParentId(int i) { return ((i - 1) / 2); }
            protected int LeftChildId(int i) { return 2 * i + 1; }
            protected int RightChildId(int i) { return 2 * i + 2; }

            protected T Value(int i) { return H[i]; }

            public int Size {
                get { return _size; }
            }
            public abstract int SiftUp(int i);

            public abstract int SiftDown(int i);

            public void Insert(T p)
            {
                if (_size == _maxSize) throw new ArgumentOutOfRangeException();

                _size += 1;
                H[_size - 1] = p;
                SiftUp(_size - 1);
            }

            public T ExtractMax()
            {
                var result = H[0];
                H[0] = H[_size - 1];
                _size -= 1;
                SiftDown(0);

                return result;
            }

            //public void Remove(int i)
            //{
            //    H[i] = int.MaxValue;
            //    SiftUp(i);
            //    ExtractMax();
            //}

            //public void ChangePriority(int i, T p)
            //{
            //    var q = H[i];
            //    H[i] = p;
            //    if (p > q)
            //        SiftUp(i);
            //    else
            //        SiftDown(i);

            //}

            protected void Swap(int id1, int id2)
            {
                var temp = H[id2];
                H[id2] = H[id1];
                H[id1] = temp;
            }
        }
    }

}