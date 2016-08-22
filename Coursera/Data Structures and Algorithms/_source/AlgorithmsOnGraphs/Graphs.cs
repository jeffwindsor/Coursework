using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public interface IGraph
    {
        int Size();

        IEnumerable<int> Neighbors(int i);
    }

    public class AdjacentyList
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

        public int Length { get { return _hashSets.Length; }}

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

    public class GraphAdjacentyList : IGraph
    {
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

    public class DepthFirstSearch
    {
        private readonly IGraph _graph;
        private readonly int[] _connectedComponent;
        private int _lastConnectedComponent = 0;
        public DepthFirstSearch(IGraph g)
        {
            _graph = g;
            _connectedComponent = new int[g.Size()];
        }

        public bool Visited(int v)
        {
            return _connectedComponent[v] != 0;
        }

        public virtual void Search()
        {
            for (var i = 0; i < _connectedComponent.Length; i++)
                Explore(i);
        }

        public void Explore(int v)
        {
            if (!Visited(v))
                Explore(v, ++_lastConnectedComponent);
        }

        private void Explore(int v, int connectedComponent)
        {
            _connectedComponent[v] = connectedComponent;
            PreVisitHook(v);
            foreach (var w in _graph.Neighbors(v))
            {
                if (Visited(w) == false)
                    Explore(w, connectedComponent);
            }
            PostVisitHook(v);
        }

        protected virtual void PreVisitHook(int v){}
        protected virtual void PostVisitHook(int v){}

        public string ToPrettyString()
        {
            return string.Join(Environment.NewLine,
                _connectedComponent.Select(
                    (item, i) => string.Format("[{0}]: {1}", i, item))
                );
        }
    }

    public class DepthFirstSearchWithOrdering : DepthFirstSearch
    {
        private readonly int[] _pre;
        private readonly int[] _post;

        public DepthFirstSearchWithOrdering(IGraph g) : base(g)
        {
            _pre = new int[g.Size()];
            _post = new int[g.Size()];
        }

        private int _counter;
        public override void Search()
        {
            _counter = 1;
            base.Search();
        }

        protected override void PreVisitHook(int v)
        {
            _pre[v] = _counter++;
        }
        protected override void PostVisitHook(int v)
        {
            _post[v] = _counter++;
        }
    }
}
