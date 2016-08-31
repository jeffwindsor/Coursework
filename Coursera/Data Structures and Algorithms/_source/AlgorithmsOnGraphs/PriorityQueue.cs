using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsOnGraphs
{
    public class PriorityQueue
    {
        private readonly int _maxSize;
        private int _size;
        private readonly int[] _heap;

        public PriorityQueue(int size)
        {
            _heap = new int[size];
            _maxSize = size;
            _size = 0;
        }

        protected int ParentId(int i) { return ((i - 1) / 2); }
        protected int LeftChildId(int i) { return 2 * i + 1; }
        protected int RightChildId(int i) { return 2 * i + 2; }

        protected int Value(int i) { return _heap[i]; }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public int Size
        {
            get { return _size; }
        }
        public int SiftUp(int i)
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

        public int SiftDown(int i)
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

        public void Insert(int p)
        {
            if (_size == _maxSize) throw new ArgumentOutOfRangeException();

            _size += 1;
            _heap[_size - 1] = p;
            SiftUp(_size - 1);
        }

        //public int Max()
        //{
        //    return _heap[0];
        //}
        public int Extract()
        {
            var result = _heap[0];
            _heap[0] = _heap[_size - 1];
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

        //public void ChangePriority(int i, int p)
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
            var temp = _heap[id2];
            _heap[id2] = _heap[id1];
            _heap[id1] = temp;
        }
    }
}
