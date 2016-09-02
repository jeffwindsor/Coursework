using System;
using System.Collections.Generic;

namespace AlgorithmsOnGraphs
{
    public class DijkstrasAlgorithm
    {
        private readonly AdjacencyListGraph _graph;
        private readonly SearchData _distance;
        private readonly SearchData _visitedFrom;
        
        public DijkstrasAlgorithm(AdjacencyListGraph g)
        {
            _graph = g;
            _visitedFrom = new SearchData(g.Size());
            _distance = new SearchData(g.Size(), int.MaxValue);
        }


        public ICollection<int> LowestCostPath(int from, int to)
        {
            //Search From to establish visited values
            Explore(from);
            //Return shortest path
            return GetLowestCostPath(from, to);
        }
        private List<int> GetLowestCostPath(int from, int to)
        {
            var result = new List<int>();
            while (to != from)
            {
                //No Path Found
                if (to == _visitedFrom.InitialValue)
                    return new List<int>();

                result.Add(to);
                to = _visitedFrom.GetValue(to);
            }
            result.Reverse();
            return result;
        }

        private void Explore(int start)
        {
            _distance.SetValue(start,0);

            //Make Prioirty Queue
            var pq = new MinPriorityQueue(_graph.Size());
            foreach (var d in _distance.Values)
            {
                pq.Insert(d);
            }

            while (!pq.IsEmpty())
            {
                var currentIndex = pq.Extract();
                foreach (var edge in _graph.Neighbors(currentIndex))
                {
                    
                    var neighborIndex = edge.Right;
                    var distanceCurrent = _distance.GetValue(neighborIndex);
                    var distanceNew = _distance.GetValue(currentIndex) +  edge.Weight;

                    if (distanceCurrent <= distanceNew) continue;

                    //Set New Distance Values
                    _distance.SetValue(neighborIndex, distanceNew);
                    _visitedFrom.SetValue(neighborIndex, currentIndex);
                    pq.ChangePriority(neighborIndex, distanceNew);
                }
            }
        }
    }
    public class MinPriorityQueue<T> where T : IComparable<T>
    {
        private readonly int _maxSize;
        private int _size;
        private readonly T[] _heap;

        public MinPriorityQueue(int size)
        {
            _heap = new T[size];
            _maxSize = size;
            _size = 0;
        }
        protected int ParentId(int i) { return ((i - 1) / 2); }
        protected int LeftChildId(int i) { return 2 * i + 1; }
        protected int RightChildId(int i) { return 2 * i + 2; }

        protected T Value(int i) { return _heap[i]; }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public int Size
        {
            get { return _size; }
        }

        private void SiftUp(int i)
        {
            //var swapId = i;
            while (i > 0 && Value(ParentId(i)).CompareTo(Value(i)) > 0)
            {
                var swapId = ParentId(i);
                Swap(i, swapId);
                i = swapId;
            }
            //return swapId;
        }

        private void SiftDown(int i)
        {
            var maxIndex = i;

            var r = RightChildId(i);
            if (r < _size && Value(r).CompareTo(Value(maxIndex)) < 0) maxIndex = r;

            var l = LeftChildId(i);
            if (l < _size && Value(l).CompareTo(Value(maxIndex)) < 0) maxIndex = l;

            if (i != maxIndex)
            {
                Swap(i, maxIndex);
                //return SiftDown(maxIndex);
            }
            //return maxIndex;
        }

        public void Insert(T p)
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
        public T Extract()
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

        public void ChangePriority(T i, int p)
        {
            var q = _heap[i];
            _heap[i] = p;

            Value(r).CompareTo(Value(maxIndex)) < 0
            if (p < q)
                SiftUp(i);
            else
                SiftDown(i);

        }

        protected void Swap(int id1, int id2)
        {
            var temp = _heap[id2];
            _heap[id2] = _heap[id1];
            _heap[id1] = temp;
        }
    }
}
