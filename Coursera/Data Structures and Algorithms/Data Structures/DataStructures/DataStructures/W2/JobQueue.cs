using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return AssignJobs(threadCount, jobCount, jobs)
                .Select((i,time) => string.Format("{0} {1}",i,time))
                .ToArray();
        }

        public static IReadOnlyCollection<Tuple<int,int>> AssignJobs(int threadCount, int jobCount, int[] jobs)
        {
            //var heap = MaxBinaryHeap.Build()
            throw new NotImplementedException();
        }

        private class MinBinaryHeap : BinaryHeap
        {
            public static BinaryHeap Build(int n, int[] data)
            {
                var current = data.Take(n).ToArray();
                var heap = new MinBinaryHeap(current);
                for (int i = n / 2; i >= 0; i--)
                {
                    heap.SiftDown(i);
                }
                return heap;
            }

            public MinBinaryHeap(int[] source) : base(source) { }
            public override int SiftUp(int i)
            {
                var swapId = i;
                while (i > 0 && Value(ParentId(i)) > Value(i))
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
                if (r < _size && Value(r) < Value(maxIndex)) maxIndex = r;

                var l = LeftChildId(i);
                if (l < _size && Value(l) < Value(maxIndex)) maxIndex = l;

                if (i != maxIndex)
                {
                    Swap(i, maxIndex);
                    return SiftDown(maxIndex);
                }
                return maxIndex;
            }
        }

        private abstract class BinaryHeap
        {
            protected int _maxSize;
            protected int _size;
            private int[] _array;

            protected BinaryHeap(int[] source)
            {
                _array = source;
                _maxSize = source.Length;
                _size = source.Length;
            }

            protected int ParentId(int i) { return ((i - 1) / 2); }
            protected int LeftChildId(int i) { return 2 * i + 1; }
            protected int RightChildId(int i) { return 2 * i + 2; }

            protected int Value(int i) { return _array[i]; }

            public abstract int SiftUp(int i);

            public abstract int SiftDown(int i);

            public void Insert(int p)
            {
                if (_size == _maxSize) throw new ArgumentOutOfRangeException();

                _size += 1;
                _array[_size] = p;
                SiftUp(_size);
            }

            public int ExtractMax()
            {
                var result = _array[1];
                _array[1] = _array[_size];
                _size -= 1;
                SiftDown(1);

                return result;
            }

            public void Remove(int i)
            {
                _array[i] = int.MaxValue;
                SiftUp(i);
                ExtractMax();
            }

            public void ChangePriority(int i, int p)
            {
                var q = _array[i];
                _array[i] = p;
                if (p > q)
                    SiftUp(i);
                else
                    SiftDown(i);

            }

            protected void Swap(int id1, int id2)
            {
                var temp = _array[id2];
                _array[id2] = _array[id1];
                _array[id1] = temp;

                _swaps.Add(new Tuple<int, int>(id1, id2));
            }

            private List<Tuple<int, int>> _swaps = new List<Tuple<int, int>>();
            public IReadOnlyCollection<Tuple<int, int>> Swaps { get { return _swaps; } }
        }
    }

}
