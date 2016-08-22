using System;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class DepthFirstSearch
    {
        private readonly IGraph _graph;
        private readonly int[] _connectedComponent;

        public DepthFirstSearch(IGraph g)
        {
            _graph = g;
            _connectedComponent = new int[g.Size()];
            ConnectedComponents = 0;
        }

        public int ConnectedComponents { get; private set; }

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
                Explore(v, ++ConnectedComponents);
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

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                _connectedComponent.Select(
                    (item, i) => string.Format("[{0}]: {1}", i, item))
                );
        }
    }
}