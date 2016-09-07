using System;

namespace AlgorithmsOnGraphs
{
    public class DijkstrasAlgorithm
    {
        private readonly AdjacencyListGraph _graph;
        private SearchData _distance;
        private SearchData _visitedFrom;
        private const int MaxDistance = int.MaxValue;

        public DijkstrasAlgorithm(AdjacencyListGraph g)
        {
            _graph = g;
        }

        public int LowestCostPath(int from, int to)
        {
            //Search From to establish visited values
            Explore(from);
            //Return lowest cost for path
            var d = _distance.GetValue(to);
            return (d == MaxDistance) ? -1 : d;
        }
        
        private void Explore(int start)
        {
            _visitedFrom = new SearchData(_graph.Size());
            _distance = new SearchData(_graph.Size(), MaxDistance);
            _distance.SetValue(start,0);

            //Make Prioirty Queue
            var pq = new MinPriorityQueue(_graph.Size());
            for (var i = 0; i < _distance.Length; i++)
            {
                pq.Enqueue(i,_distance.GetValue(i));
            }

            while (!pq.IsEmpty())
            {
                //Console.WriteLine("Queue: {0}", pq);

                var currentIndex = pq.Dequeue();

                //Console.WriteLine("Extract: {0}", currentIndex);

                foreach (var edge in _graph.Neighbors(currentIndex))
                {
                    var neighborIndex = edge.Right;
                    var d = _distance.GetValue(neighborIndex);
                    var dFromC = _distance.GetValue(currentIndex) == MaxDistance
                        ? MaxDistance
                        : _distance.GetValue(currentIndex) + edge.Weight;

                    //Console.WriteLine("Edge {1} => {0} : Distance {2} : {3}",neighborIndex,currentIndex,dFromC,d);

                    if (d <= dFromC) continue;

                    //Set New Distance Values
                    _distance.SetValue(neighborIndex, dFromC);
                    _visitedFrom.SetValue(neighborIndex, currentIndex);
                    pq.ChangePriority(neighborIndex, dFromC);
                }
            }
        }
    }
}
