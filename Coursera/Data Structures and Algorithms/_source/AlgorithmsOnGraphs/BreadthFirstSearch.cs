using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class BreadthFirstSearch : BaseGraphSearch
    {
        private readonly SearchData _visitedFrom;
        public BreadthFirstSearch(ISearchableGraph g) : base(g)
        {
            _visitedFrom = new SearchData(g.Size());
        }
        
        public void Search(int start)
        {
            SearchData.SetValue(start, 0);

            var queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Any())
            {
                var current = queue.Dequeue();
                foreach (var neighbor in Graph.Neighbors(current))
                {
                    if (SearchData.Visited(neighbor)) continue;

                    queue.Enqueue(neighbor);
                    SearchData.SetValue(neighbor, SearchData.GetValue(current) + 1);
                    _visitedFrom.SetValue(neighbor, current);
                }
            }
        }

        public ICollection<int> ShortestPath(int from, int to)
        {
            var result = new List<int>();

            while (from != to)
            {
                result.Add(to);
                to = _visitedFrom.GetValue(to);
            }

            result.Reverse();
            return result;
        }
    }
}
