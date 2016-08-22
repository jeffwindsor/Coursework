using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class GraphAdjacentyList : IGraph
    {
        private class AdjacentyList
        {
            private readonly HashSet<int>[] _hashSets;
            public AdjacentyList(int size)
            {
                _hashSets = new HashSet<int>[size];
            }
            public HashSet<int> this[int i]
            {
                get
                {
                    return _hashSets[i] ?? (_hashSets[i] = new HashSet<int>());
                }
            }

            public int Length { get { return _hashSets.Length; } }

            public override string ToString()
            {
                return string.Join(Environment.NewLine,
                    _hashSets.Select(
                        (item, i) => item == null
                            ? ""
                            : string.Format("[{0}]: {1}", i, string.Join(",", item.Select(e => e.ToString()))))
                    );
            }
        }

        private readonly AdjacentyList _adjlist;
        public GraphAdjacentyList(int size)
        {
            _adjlist = new AdjacentyList(size);
        }

        public int Size()
        {
            return _adjlist.Length;
        }

        public IEnumerable<int> Neighbors(int i)
        {
            return _adjlist[i];
        }

        public void AddDirectedEdge(int left, int right)
        {
            _adjlist[left].Add(right);
        }
        public void AddUndirectedEdge(int left, int right)
        {
            AddDirectedEdge(left, right);
            AddDirectedEdge(right, left);
        }

        public override string ToString()
        {
            return _adjlist.ToString();
        }
    }
}