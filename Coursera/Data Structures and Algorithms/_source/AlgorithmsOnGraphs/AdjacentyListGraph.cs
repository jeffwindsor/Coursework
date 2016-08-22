using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    /// <summary>
    /// Vertices can be a 
    ///     Source = no incoming sdges
    ///     Sink = no outgoing edges
    /// </summary>
    public class DirectedAcyclicGraph : ISearchableGraph
    {
        public int Size()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> Neighbors(int i)
        {
            throw new NotImplementedException();
        }
    }

    public class AdjacentyListGraph : ISearchableGraph
    {
        private class AdjacencyList : HashSet<int>{}

        private class AdjacencyLists
        {
            private readonly AdjacencyList[] _hashSets;
            public AdjacencyLists(int size)
            {
                _hashSets = new AdjacencyList[size];
            }
            public AdjacencyList this[int i]
            {
                get
                {
                    return _hashSets[i] ?? (_hashSets[i] = new AdjacencyList());
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

        private readonly AdjacencyLists _lists;
        public AdjacentyListGraph(int size)
        {
            _lists = new AdjacencyLists(size);
        }

        public int Size()
        {
            return _lists.Length;
        }

        public IEnumerable<int> Neighbors(int i)
        {
            return _lists[i];
        }

        public void AddDirectedEdge(int left, int right)
        {
            _lists[left].Add(right);
        }

        public void AddEdge(int left, int right)
        {
            AddDirectedEdge(left, right);
            AddDirectedEdge(right, left);
        }

        public override string ToString()
        {
            return _lists.ToString();
        }
    }
}