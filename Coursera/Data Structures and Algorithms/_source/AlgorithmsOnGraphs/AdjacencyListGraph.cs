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

        public AdjacencyListGraph ToUndirectedAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, e) =>
            {
                g.AddDirectedEdge(e);
                g.AddDirectedEdge(ReverseEdge(e));
            });
        }
        public AdjacencyListGraph ToDirectedAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, e) => g.AddDirectedEdge(e));
        }

        public AdjacencyListGraph ToDirectedReverseAdjacencyGraph()
        {
            return ToAdjacencyGraph((g, e) => g.AddDirectedEdge(ReverseEdge(e)));
        }

        private AdjacencyListGraph ToAdjacencyGraph(Action<AdjacencyListGraph, Edge> addEdge)
        {
            var line0 = ParseIntPair(_inputs[0]);
            var verticeCount = line0.Item1;
            var edgeCount = line0.Item2;
            var graph = new AdjacencyListGraph(verticeCount);
            foreach (var edge in Enumerable.Range(1, edgeCount).Select(i => ParseEdge(_inputs[i])))
            {
                addEdge(graph, edge);
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
            return new Edge
            {
                Left = ParseIndex(linePath[0]),
                Right = ParseIndex(linePath[1]),
                Weight = (linePath.Length ==3)? int.Parse(linePath[1]) : 0
            };
        }
        public static int ParseIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }
        private static string ParseSource(int index)
        {
            return (index + 1).ToString(); //input is 1 based return zero based
        }

        private static Edge ReverseEdge(Edge e)
        {
            return new Edge
            {
                Right = e.Left,
                Left = e.Right,
                Weight = e.Weight
            };
        }
    }
    public class Edge
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public int Weight { get; set; }
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

        public IEnumerable<Edge> Neighbors(int i)
        {
            return _lists[i].Select(kvp => kvp.Value);
        }
        public IEnumerable<int> NeighborIndexes(int i)
        {
            return _lists[i].Select(kvp => kvp.Value.Right);
        }

        public void AddDirectedEdge(Edge edge)
        {
            _lists[edge.Left][edge.Right] = edge;
        }

        public override string ToString()
        {
            return _lists.ToString();
        }
    }

    public class AdjacencyList : Dictionary<int,Edge>
    {
        public bool IsSink { get { return !this.Any(); } }
    }

    public class AdjacencyListArray
    {
        private readonly AdjacencyList[] _list;
        public AdjacencyListArray(int size)
        {
            _list = new AdjacencyList[size];
        }
        public AdjacencyList this[int i]
        {
            get
            {
                return _list[i] ?? (_list[i] = new AdjacencyList());
            }
        }

        public int Length { get { return _list.Length; } }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                _list.Select(
                    (item, i) =>  string.Format("[{0}]: {1}", i,
                        item == null
                            ? ""
                            : string.Join(",", item.Select(e => e.ToString()))))
                );
        }
    }
}