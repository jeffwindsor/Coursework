using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class DepthFirstSearch : BaseGraphSearch
    {
        public int MaxComponent { get; private set; }

        public DepthFirstSearch(ISearchableGraph g) : base(g) { }

        public virtual void Search()
        {
            for (var i = 0; i < SearchData.Values.Count; i++)
                Explore(i);
        }

        public void Explore(int v)
        {
            if (!SearchData.Visited(v))
                Explore(v, ++MaxComponent);
        }

        protected virtual void Explore(int v, int connectedComponent)
        {
            SearchData.SetValue(v, connectedComponent);
            foreach (var w in Graph.Neighbors(v))
            {
                if (!SearchData.Visited(w))
                    Explore(w, connectedComponent);
            }
        }

        public IEnumerable<int> Components
        {
            get { return SearchData.Values.Distinct(); }
        }

        public override string ToString()
        {
            return string.Join(" ", SearchData.Values.Select((item, i) => string.Format("[{0}:{1}]", i, item)));
        }
    }
}