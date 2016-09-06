using System.Collections.Generic;

namespace AlgorithmsOnGraphs
{
    public class DijkstrasAlgorithm
    {
        private readonly AdjacencyListGraph _graph;
        private readonly SearchData _distance;
        private readonly SearchData _visitedFrom;
        private readonly int MaxDistance = int.MaxValue;
        public DijkstrasAlgorithm(AdjacencyListGraph g)
        {
            _graph = g;
            _visitedFrom = new SearchData(g.Size());
            _distance = new SearchData(g.Size(), MaxDistance);
        }


        public ICollection<int> LowestCostPath(int from, int to)
        {
            //Search From to establish visited values
            Explore(from);
            //Return shortest path
            return GetLowestCostPath(from, to);
        }
        private List<int> GetLowestCostPath(int from, int to)
        {
            var result = new List<int>();
            while (to != from)
            {
                //No Path Found
                if (to == _visitedFrom.InitialValue)
                    return new List<int>();

                result.Add(to);
                to = _visitedFrom.GetValue(to);
            }
            result.Reverse();
            return result;
        }

        private void Explore(int start)
        {
            _distance.SetValue(start,0);

            //Make Prioirty Queue
            var pq = new MinPriorityQueue(_graph.Size());
            for (var i = 0; i < _distance.Length; i++)
            {
                pq.Insert(i,_distance.GetValue(i));
            }

            while (!pq.IsEmpty())
            {
                var currentIndex = pq.ExtractMax();
                foreach (var edge in _graph.Neighbors(currentIndex))
                {
                    var neighborIndex = edge.Right;
                    var dn = _distance.GetValue(neighborIndex);
                    var dcn = 
                        _distance.GetValue(currentIndex) == MaxDistance
                        ? MaxDistance
                        :_distance.GetValue(currentIndex) +  edge.Weight;

                    if (dn <= dcn) continue;

                    //Set New Distance Values
                    _distance.SetValue(neighborIndex, dcn);
                    _visitedFrom.SetValue(neighborIndex, currentIndex);
                    pq.ChangePriority(neighborIndex, dcn);
                }
            }
        }
    }
}
