using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class DepthFirstSearchWithComponents
    {
        public int MaxComponent { get; private set; }
        private readonly ISearchableGraph _graph;
        private readonly SearchData _searchData;

        public DepthFirstSearchWithComponents(ISearchableGraph g)
        {
            _graph = g;
            _searchData = new SearchData(g.Size());
        }

        public bool Visited(int v)
        {
            return _searchData.Visited(v);
        }

        public virtual void Search()
        {
            for (var i = 0; i < _searchData.Values.Count; i++)
                Explore(i);
        }

        public void Explore(int v)
        {
            if (!_searchData.Visited(v))
                Explore(v, ++MaxComponent);
        }

        protected virtual void Explore(int v, int connectedComponent)
        {
            _searchData.SetValue(v, connectedComponent);
            foreach (var w in _graph.Neighbors(v))
            {
                if (!_searchData.Visited(w))
                    Explore(w, connectedComponent);
            }
        }

        public IEnumerable<int> Components
        {
            get { return _searchData.Values.Distinct(); }
        }

        public override string ToString()
        {
            return string.Join(" ", _searchData.Values.Select((item, i) => string.Format("[{0}:{1}]", i, item)));
        }
    }
}