using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.W2
{
    public class MakeHeap
    {
        public static string[] Process(string[] inputs)
        {
            var n = int.Parse(inputs[0]);
            var data = inputs[1]
                .Split(new[] { ' ' })
                .Take(n)
                .Select(int.Parse)
                .ToArray();

            return CreateHeap(n, data);
        }
        public static string[] CreateHeap(int n, int[] data)
        {
            throw new NotImplementedException();
        }
    }

    public class BinaryHeap
    {
        private int _maxSize;
        private int _size;
        private int[] H;

        public BinaryHeap(int maxSize)
        {
            H = new int[maxSize];
            _maxSize = maxSize;
            _size = 0;
        }

        public int Parent(int i) { return i / 2; }
        public int LeftChild(int i) { return 2 * i; }
        public int RightChild(int i) { return (2 * i) + 1; }
        
        public void SiftUp(int i)
        {
            while (i > 1 && H[Parent(i)] < H[i])
            {
                var pid = Parent(i);
                Swap(i, pid);
                i = pid;
            }
        }

        public void SiftDown(int i)
        {
            var maxIndex = i;
            var l = LeftChild(i);
            if (l <= _size && H[l] > H[maxIndex]) maxIndex = l;

            var r = RightChild(i);
            if (l <= _size && H[r] > H[maxIndex]) maxIndex = r;

            if (i != maxIndex)
            {
                Swap(i, maxIndex);
                SiftDown(maxIndex);
            }
        }

        public void Insert(int p)
        {
            if (_size == _maxSize) throw new ArgumentOutOfRangeException();

            _size += 1;
            H[_size] = p;
            SiftUp(_size);
        }

        public int ExtractMax()
        {
            var result = H[1];
            H[1] = H[_size];
            _size -= 1;
            SiftDown(1);

            return result;
        }

        public void Remove(int i)
        {
            H[i] = int.MaxValue;
            SiftUp(i);
            ExtractMax();
        }

        public void ChangePriority(int i, int p)
        {
            var q = H[i];
            H[i] = p;
            if (p > q)
                SiftUp(i);
            else
                SiftDown(i);

        }

        private void Swap(int id1, int id2)
        {
            var temp = H[id2];
            H[id2] = H[id1];
            H[id1] = temp;
        }
    }

}
