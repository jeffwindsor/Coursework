using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class NonBipartiteException: Exception { }
    public class BreadthFirstSearchWithBipartiteDetection
    {
        private const int RED = 0;
        private const int BLUE = 1;
        private readonly ISearchableGraph _graph;
        private readonly SearchData _searchData;

        public BreadthFirstSearchWithBipartiteDetection(ISearchableGraph g)
        {
            _graph = g;
            _searchData = new SearchData(g.Size());
        }

        public bool IsBipartite()
        {
            try
            {
                Explore(0);
                return true;
            }
            catch (NonBipartiteException)
            {
                return false;
            }
        }
        private void Explore(int start)
        {
            _searchData.SetValue(start, RED);

            var queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Any())
            {
                var current = queue.Dequeue();
                var neighborColor = GetNeighborColor(current);
                foreach (var neighbor in _graph.Neighbors(current).Where(i => i != current))
                {
                    if (_searchData.Visited(neighbor))
                    {
                        //Check Visited neighbor is the correct color
                        if(_searchData.GetValue(neighbor) != neighborColor)
                            throw new NonBipartiteException();

                        continue;
                    }

                    queue.Enqueue(neighbor);
                    _searchData.SetValue(neighbor, neighborColor);
                }
            }
        }

        private int GetNeighborColor(int index)
        {
            switch (_searchData.GetValue(index))
            {
                case RED:
                    return BLUE;
                case BLUE:
                    return RED;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
