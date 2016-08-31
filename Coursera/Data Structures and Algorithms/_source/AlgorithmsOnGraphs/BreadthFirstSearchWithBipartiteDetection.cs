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
        private readonly AdjacencyListGraph _graph;
        private readonly SearchData _color;

        public BreadthFirstSearchWithBipartiteDetection(AdjacencyListGraph g)
        {
            _graph = g;
            _color = new SearchData(g.Size());
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
            _color.SetValue(start, RED);

            var queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Any())
            {
                var current = queue.Dequeue();
                var neighborColor = GetNeighborColor(current);
                foreach (var neighbor in _graph.Neighbors(current).Where(i => i != current))
                {
                    if (_color.Visited(neighbor))
                    {
                        //Check Visited neighbor is the correct color
                        if(_color.GetValue(neighbor) != neighborColor)
                            throw new NonBipartiteException();

                        continue;
                    }

                    queue.Enqueue(neighbor);
                    _color.SetValue(neighbor, neighborColor);
                }
            }
        }

        private int GetNeighborColor(int index)
        {
            switch (_color.GetValue(index))
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
