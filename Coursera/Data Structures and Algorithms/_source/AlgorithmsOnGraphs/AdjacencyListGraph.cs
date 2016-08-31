using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class AdjacencyListGraphInput
    {
        private int _lineCursor;
        private readonly IList<string> _inputs;

        public AdjacencyListGraphInput(IList<string> inputs)
        {
            _inputs = inputs;
        }

        public class Edge
        {
            public int Left { get; set; }
            public int Right { get; set; }
        }

        public AdjacencyListGraph ToUndirectedAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, l, r) =>
            {
                g.AddDirectedEdge(l, r);
                g.AddDirectedEdge(r, l);
            });
        }
        public AdjacencyListGraph ToDirectedAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, l, r) => g.AddDirectedEdge(l, r));
        }

        public AdjacencyListGraph ToDirectedReverseAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, l, r) => g.AddDirectedEdge(r, l));
        }

        private AdjacencyListGraph ToAdjacencyGraph(Action<AdjacencyListGraph, int, int> addEdge)
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
            return new Edge { Left = ParseIndex(linePath[0]), Right = ParseIndex(linePath[1]) };
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
    public class AdjacencyListGraph
    {
        private readonly AdjacencyListArray _lists;
        public AdjacencyListGraph(int size)
        {
            _lists = new AdjacencyListArray(size);
        }

        public int Size()
        {
            return _lists.Length;
        }

        public IEnumerable<int> Neighbors(int i)
        {
            return _lists[i];
        }
        
        public void AddDirectedEdge(int left, int right)
        {
            _lists[left].Add(right);
        }

        public override string ToString()
        {
            return _lists.ToString();
        }
    }

    public class AdjacencyList : HashSet<int>
    {
        public bool IsSink { get { return !this.Any(); } }
    }

    public class AdjacencyListArray
    {
        private readonly AdjacencyList[] _hashSets;
        public AdjacencyListArray(int size)
        {
            _hashSets = new AdjacencyList[size];
        }
        public AdjacencyList this[int i]
        {
            get
            {
                return _hashSets[i] ?? (_hashSets[i] = new AdjacencyList());
            }
        }

        public int Length { get { return _hashSets.Length; } }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                _hashSets.Select(
                    (item, i) =>  string.Format("[{0}]: {1}", i,
                        item == null
                            ? ""
                            : string.Join(",", item.Select(e => e.ToString()))))
                );
        }
    }
}