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

        private void Explore(int start)
        {
            _distance.SetValue(start,0);

            //Make Prioirty Queue
            var pq = new PriorityQueue(_graph.Size());
            foreach (var d in _distance.Values)
            {
                pq.Insert(d);
            }

            while (!pq.IsEmpty())
            {
                var current = pq.Extract();
                foreach (var neighbor in _graph.Neighbors(current))
                {
                    var dc = _distance.GetValue(current);
                    var dn = _distance.GetValue(neighbor);

                }
            }
        }
    }
}
