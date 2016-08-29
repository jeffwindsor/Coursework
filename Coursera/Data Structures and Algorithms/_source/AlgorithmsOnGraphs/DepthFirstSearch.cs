using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class DepthFirstSearch
    {
        protected readonly ISearchableGraph Graph;
        protected int[] ConnectedComponent { get; set; }
        public int ConnectedComponents { get; protected set; }

        public DepthFirstSearch(ISearchableGraph g)
        {
            Graph = g;
            ConnectedComponent = new int[g.Size()];
            ConnectedComponents = 0;
        }

        public bool Visited(int v)
        {
            return ConnectedComponent[v] != 0;
        }

        public virtual void Search()
        {
            for (var i = 0; i < ConnectedComponent.Length; i++)
                Explore(i);
        }

        public void Explore(int v)
        {
            if (!Visited(v))
                Explore(v, ++ConnectedComponents);
        }

        protected virtual void Explore(int v, int connectedComponent)
        {
            ConnectedComponent[v] = connectedComponent;
            foreach (var w in Graph.Neighbors(v))
            {
                if (Visited(w) == false)
                    Explore(w, connectedComponent);
            }
        }

        public IEnumerable<int> Components
        {
            get { return ConnectedComponent.Distinct(); }
        }

        public override string ToString()
        {
            return string.Join(" ",
                ConnectedComponent.Select(
                    (item, i) => string.Format("[{0}:{1}]", i, item))
                );
        }
    }
}