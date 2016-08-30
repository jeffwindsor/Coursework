using System.Collections.Generic;

namespace AlgorithmsOnGraphs
{
    public class DepthFirstSearchWithCycleException
    {
        private readonly ISearchableGraph _g;
        
        public DepthFirstSearchWithCycleException(ISearchableGraph g)
        {
            _g = g;
        }

        public virtual void Search()
        {
            for (var v = 0; v < _g.Size(); v++)
                Explore(v, new HashSet<int>());
        }
        
        protected virtual void Explore(int v, HashSet<int> ancestory)
        {
            if (ancestory.Contains(v))
                throw new GraphCycleException();

            ancestory.Add(v);
            foreach (var w in _g.Neighbors(v))
            {
                Explore(w, ancestory);
            }
            ancestory.Remove(v);
        }
    }
}