using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class GraphInput
    {
        private int _lineCursor;
        private readonly IList<string> _inputs;

        public GraphInput(IList<string> inputs)
        {
            _inputs = inputs;
        }

        public class Edge
        {
            public int Left { get; set; }
            public int Right { get; set; }
        }

        public ISearchableGraph ToUndirectedAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, l, r) =>
            {
                g.AddDirectedEdge(l, r);
                g.AddDirectedEdge(r,l);
            });
        }
        public ISearchableGraph ToDirectedAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, l, r) => g.AddDirectedEdge(l, r));
        }

        public ISearchableGraph ToDirectedReverseAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, l, r) => g.AddDirectedEdge(r, l));
        }

        private ISearchableGraph ToAdjacencyGraph(Action<AdjacencyListGraph, int, int> addEdge)
        {
            var line0 = ParseIntPair(_inputs[0]);
            var verticeCount = line0.Item1;
            var edgeCount = line0.Item2;
            var graph = new AdjacencyListGraph(verticeCount);
            foreach (var x in Enumerable.Range(1, edgeCount).Select(i => ParseEdge(_inputs[i])))
            {
                addEdge(graph, x.Left, x.Right);
            }
            _lineCursor = edgeCount + 1;

            return graph;
        }

        public Edge NextAsEdge()
        {
            return ParseEdge(_inputs[_lineCursor++]);
        }


        private static char[] Splits = { ' ' };
        public static Tuple<int, int> ParseIntPair(string line)
        {
            var s = line.Split(Splits);
            return new Tuple<int, int>(int.Parse(s[0]), int.Parse(s[1]));
        }
        public static Edge ParseEdge(string line)
        {
            //Index given in 1 convert to 0
            var linePath = line.Split(Splits, StringSplitOptions.RemoveEmptyEntries);
            return new Edge {Left=ParseIndex(linePath[0]), Right = ParseIndex(linePath[1])};
        }
        public static int ParseIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }
        private static string ParseSource(int index)
        {
            return (index + 1).ToString(); //input is 1 based return zero based
        }
    }
}
