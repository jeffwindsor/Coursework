using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class MinPriorityQueue
    {
        private class Node
        {
            public int Value { get; set; }
            public int Priority { get; set; }
        }

        private readonly int _maxSize;
        private int _currentSize;
        private readonly Node[] _heap;
        private readonly int[] _valueToHeapIndexMap;

        public MinPriorityQueue(int size)
        {
            _heap = new Node[size];
            _valueToHeapIndexMap = Enumerable.Range(0,size).Select(_ => -1).ToArray();
            _maxSize = size;
        }

        public void Insert(int value, int priority)
        {
            if (_currentSize == _maxSize) throw new ArgumentOutOfRangeException();
            _currentSize += 1;

            _valueToHeapIndexMap[value] = LastIndex;
            _heap[LastIndex] = new Node { Value = value, Priority = priority };
            SiftUp(LastIndex);
        }

        public int ExtractMax()
        {
            var result = _heap[0];

            Swap(0, LastIndex);
            _heap[LastIndex] = null;
            _valueToHeapIndexMap[result.Value] = -1;
            _currentSize -= 1;

            SiftDown(0);
            return result.Value;
        }

        public void ChangePriority(int value, int priority)
        {
            var heapIndex = _valueToHeapIndexMap[value];
            var node = _heap[heapIndex];
            var oldPriority = node.Priority;
            node.Priority = priority;

            if (priority > oldPriority)
                SiftUp(heapIndex);
            else
                SiftDown(heapIndex);
        }

        private static int ParentIndex(int i) { return ((i - 1) / 2); }
        private static int LeftChildIndex(int i) { return 2 * i + 1; }
        private static int RightChildIndex(int i) { return 2 * i + 2; }
        private int LastIndex { get { return _currentSize - 1; }}
        public bool IsEmpty()
        {
            return _currentSize == 0;
        }
        
        private void SiftUp(int index)
        {
            while (index > 0 && _heap[ParentIndex(index)].Priority > _heap[index].Priority)
            {
                var parentIndex = ParentIndex(index);
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void SiftDown(int index)
        {
            var maxIndex = index;

            var rightIndex = RightChildIndex(index);
            if (rightIndex < _currentSize && _heap[rightIndex].Priority < _heap[maxIndex].Priority)
                maxIndex = rightIndex;

            var leftIndex = LeftChildIndex(index);
            if (leftIndex < _currentSize && _heap[leftIndex].Priority < _heap[maxIndex].Priority)
                maxIndex = leftIndex;

            if (index != maxIndex)
                Swap(index, maxIndex);
        }

        private void Swap(int index1, int index2)
        {
            var one = _heap[index1];
            var two = _heap[index2];

            _valueToHeapIndexMap[one.Value] = index2;
            _valueToHeapIndexMap[two.Value] = index1;

            _heap[index2] = one;
            _heap[index1] = two;


        }
    }
}