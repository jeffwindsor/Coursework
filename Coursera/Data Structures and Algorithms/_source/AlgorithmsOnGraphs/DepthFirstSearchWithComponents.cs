using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class DepthFirstSearchWithComponents
    {
        public int MaxComponent { get; private set; }
        private readonly AdjacencyListGraph _graph;
        private readonly SearchData _component;

        public DepthFirstSearchWithComponents(AdjacencyListGraph g)
        {
            _graph = g;
            _component = new SearchData(g.Size());
        }

        public bool Visited(int v)
        {
            return _component.Visited(v);
        }

        public virtual void Search()
        {
            for (var i = 0; i < _component.Values.Count; i++)
                Explore(i);
        }

        public void Explore(int v)
        {
            if (!_component.Visited(v))
                Explore(v, ++MaxComponent);
        }

        protected virtual void Explore(int v, int connectedComponent)
        {
            _component.SetValue(v, connectedComponent);
            foreach (var w in _graph.Neighbors(v))
            {
                if (!_component.Visited(w))
                    Explore(w, connectedComponent);
            }
        }

        public IEnumerable<int> Components
        {
            get { return _component.Values.Distinct(); }
        }

        public override string ToString()
        {
            return string.Join(" ", _component.Values.Select((item, i) => string.Format("[{0}:{1}]", i, item)));
        }
    }
}