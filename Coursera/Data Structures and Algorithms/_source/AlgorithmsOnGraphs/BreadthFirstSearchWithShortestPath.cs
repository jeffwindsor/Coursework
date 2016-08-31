﻿using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class BreadthFirstSearchWithShortestPath
    {
        private readonly SearchData _visitedFrom;
        private readonly ISearchableGraph _graph;
        private readonly SearchData _depth;

        public BreadthFirstSearchWithShortestPath(ISearchableGraph g)
        {
            _visitedFrom = new SearchData(g.Size());
            _graph = g;
            _depth = new SearchData(g.Size());
        }
        
        public ICollection<int> ShortestPath(int from, int to)
        {
            //Search From to establish visited values
            Explore(from);
            //Return shortest path
            return GetShortestPath(from, to);
        }

        private void Explore(int start)
        {
            _depth.SetValue(start, 0);

            var queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Any())
            {
                var current = queue.Dequeue();
                foreach (var neighbor in _graph.Neighbors(current))
                {
                    if (_depth.Visited(neighbor)) continue;

                    queue.Enqueue(neighbor);
                    _depth.SetValue(neighbor, _depth.GetValue(current) + 1);
                    _visitedFrom.SetValue(neighbor, current);
                }
            }
        }
        private List<int> GetShortestPath(int from, int to)
        {
            var result = new List<int>();
            while (to != from)
            {
                //No Path Check
                if(to == SearchData.NOT_VISITED)
                    return new List<int>();
                
                result.Add(to);
                to = _visitedFrom.GetValue(to);
            }
            result.Reverse();
            return result;
        }

    }
}
