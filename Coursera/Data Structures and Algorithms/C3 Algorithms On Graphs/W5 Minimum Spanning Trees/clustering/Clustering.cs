using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W5
{
    public class Clustering
    {
        public static void Main(string[] args)
        {
            string s;
            var inputs = new List<string>();
            while ((s = Console.ReadLine()) != null)
                inputs.Add(s);

            foreach (var result in Answer(inputs.ToArray()))
                Console.WriteLine(result);
        }

        public static IList<string> Answer(IList<string> inputs)
        {
            var gis = Inputs.AdjacencyListGraphDecimal(inputs);
            var pointsWithCount = gis.ToPoints();
            var k = gis.NextAsInt();

            var pointCount = pointsWithCount.Item1;
            var points = pointsWithCount.Item2.ToArray();
            var lines = PrimsAlgorithm.ConnectAllPoints(pointCount, points);

            var value = Calculate(k, pointCount, lines);
            var answer = value.ToString("0.0000000000");
            answer = answer.Remove(answer.Length - 1);  //Simulate truncate at 9
            return new[] { answer };
        }

        public static decimal Calculate(int numberOfClusters, int pointCount, IEnumerable<Edge<decimal>> lines)
        {
            const int NO_CLUSTER = -1;
            var clusterId = NO_CLUSTER;
            var clusters = new SearchData<int>(pointCount, NO_CLUSTER);
            var queue = new HashSet<int>(Enumerable.Range(0,pointCount));
            var d = decimal.MinValue;

            foreach (var line in lines.OrderBy(l => l.Weight))
            {
                if (!queue.Any())
                {
                    if (clusters.GetValue(line.Left) == clusters.GetValue(line.Right))
                        continue;
                    else
                        return line.Weight;
                }
                d = line.Weight;
                if (!queue.Contains(line.Left)) continue;
                

                queue.Remove(line.Left);
                if (clusters.GetValue(line.Right) == NO_CLUSTER)
                {
                    if(clusterId < numberOfClusters) clusterId++;

                    clusters.SetValue(line.Right, clusterId);
                    clusters.SetValue(line.Left, clusterId);
                }
                else
                {
                    clusters.SetValue(line.Left, clusters.GetValue(line.Right));
                }
            }

            return d;
        }
    }

    public class Inputs
    {
        public static AdjacencyListGraphInput<long> AdjacencyListGraphLong(IList<string> inputs)
        {
            return new AdjacencyListGraphInput<long>(inputs, long.Parse);
        }
        public static AdjacencyListGraphInput<decimal> AdjacencyListGraphDecimal(IList<string> inputs)
        {
            return new AdjacencyListGraphInput<decimal>(inputs, decimal.Parse);
        }
        public static AdjacencyListGraphInput<double> AdjacencyListGraphDouble(IList<string> inputs)
        {
            return new AdjacencyListGraphInput<double>(inputs, double.Parse);
        }
    }

    public class AdjacencyListGraphInput<TWeight> where TWeight : IComparable<TWeight>, IEquatable<TWeight>
    {
        private int _lineCursor;
        private readonly IList<string> _inputs;
        private readonly Func<string, TWeight> _parseWeight;
        public AdjacencyListGraphInput(IList<string> inputs, Func<string, TWeight> parseWeight = null)
        {
            _inputs = inputs;
            _parseWeight = parseWeight;
        }

        public Tuple<int, IEnumerable<Point>> ToPoints()
        {
            var pointCount = int.Parse(_inputs[0]);
            var points = Enumerable.Range(1, pointCount).Select(i => ParsePoint(_inputs[i]));
            _lineCursor = pointCount + 1;

            return new Tuple<int, IEnumerable<Point>>(pointCount, points);
        }
        
        public Tuple<int,IEnumerable<Edge<TWeight>>> ToEdges()
        {
            var line0 = ParseIntPair(_inputs[0]);
            var verticeCount = line0.Item1;
            var edgeCount = line0.Item2;

            var edges = Enumerable.Range(1, edgeCount).Select(i => ParseEdge(_inputs[i]));
            _lineCursor = edgeCount + 1;

            return new Tuple<int, IEnumerable<Edge<TWeight>>>(verticeCount, edges);
        }

        public AdjacencyListGraph<TWeight> ToUndirectedAdjacencyGraph(Tuple<int, IEnumerable<Edge<TWeight>>> edges = null)
        {
            return ToAdjacencyGraph(edges ?? ToEdges(), (g, e) =>
            {
                g.AddDirectedEdge(e);
                g.AddDirectedEdge(ReverseEdge(e));
            });
        }

        public AdjacencyListGraph<TWeight> ToDirectedAdjacencyGraph(Tuple<int, IEnumerable<Edge<TWeight>>> edges = null)
        {
            return ToAdjacencyGraph(edges ?? ToEdges(), 
                (g, e) => g.AddDirectedEdge(e));
        }

        public AdjacencyListGraph<TWeight> ToDirectedReverseAdjacencyGraph(Tuple<int, IEnumerable<Edge<TWeight>>> edges = null)
        {
            return ToAdjacencyGraph(edges ?? ToEdges(), 
                (g, e) => g.AddDirectedEdge(ReverseEdge(e)));
        }

        private static AdjacencyListGraph<TWeight> ToAdjacencyGraph(Tuple<int, IEnumerable<Edge<TWeight>>> edges,
            Action<AdjacencyListGraph<TWeight>, Edge<TWeight>> addEdge)
        {
            return ToAdjacencyGraph(edges.Item1, edges.Item2, addEdge);
        }

        private static AdjacencyListGraph<TWeight> ToAdjacencyGraph(int verticeCount, IEnumerable<Edge<TWeight>> edges, Action<AdjacencyListGraph<TWeight>, Edge<TWeight>> addEdge)
        {
            var graph = new AdjacencyListGraph<TWeight>(verticeCount);
            foreach (var edge in edges)
            {
                addEdge(graph, edge);
            }
            return graph;
        }

        public Edge<TWeight> NextAsEdge()
        {
            return ParseEdge(_inputs[_lineCursor++]);
        }

        public int NextAsIndex()
        {
            return ParseIndex(_inputs[_lineCursor++]);
        }

        public int NextAsInt()
        {
            return int.Parse(_inputs[_lineCursor++]);
        }

        private static readonly char[] Splits = { ' ' };
        public static Tuple<int, int> ParseIntPair(string line)
        {
            var s = line.Split(Splits);
            return new Tuple<int, int>(int.Parse(s[0]), int.Parse(s[1]));
        }
        public Edge<TWeight> ParseEdge(string line)
        {
            //Index given in 1 convert to 0
            var linePath = line.Split(Splits, StringSplitOptions.RemoveEmptyEntries);
            return new Edge<TWeight>
            {
                Left = ParseIndex(linePath[0]),
                Right = ParseIndex(linePath[1]),
                Weight = (linePath.Length ==3)? _parseWeight(linePath[2]) : default(TWeight)
            };
        }
        public static Point ParsePoint(string line)
        {
            var s = line.Split(Splits);
            return new Point {X = int.Parse(s[0]), Y = int.Parse(s[1])};
        }
        public static int ParseIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }

        public static decimal NoOpDecimal(decimal source)
        {
            return source;
        }

        private static string ParseSource(int index)
        {
            return (index + 1).ToString(); //input is 1 based return zero based
        }

        private static Edge<TWeight> ReverseEdge(Edge<TWeight> e)
        {
            return new Edge<TWeight>
            {
                Right = e.Left,
                Left = e.Right,
                Weight = e.Weight
            };
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Edge<TWeight>
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public TWeight Weight { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Right, Weight);
        }
    }
    public class AdjacencyListGraph<TWeight>
    {
        private readonly AdjacencyListArray<TWeight> _lists;
        public AdjacencyListGraph(int size)
        {
            _lists = new AdjacencyListArray<TWeight>(size);
        }

        public int Size()
        {
            return _lists.Length;
        }

        public IEnumerable<Edge<TWeight>> Neighbors(int i)
        {
            return _lists[i].Select(kvp => kvp.Value);
        }
        public IEnumerable<int> NeighborIndexes(int i)
        {
            return _lists[i].Select(kvp => kvp.Value.Right);
        }

        public void AddDirectedEdge(Edge<TWeight> edge)
        {
            _lists[edge.Left][edge.Right] = edge;
        }

        public override string ToString()
        {
            return _lists.ToString();
        }
    }

    public class AdjacencyList<TWeight> : Dictionary<int,Edge<TWeight>>
    {
        public bool IsSink { get { return !this.Any(); } }

        public override string ToString()
        {
            return string.Join(", ", this.Select(kvp => kvp.Value.ToString()));
        }
    }

    public class AdjacencyListArray<TWeight>
    {
        private readonly AdjacencyList<TWeight>[] _list;
        public AdjacencyListArray(int size)
        {
            _list = new AdjacencyList<TWeight>[size];
        }
        public AdjacencyList<TWeight> this[int i]
        {
            get
            {
                return _list[i] ?? (_list[i] = new AdjacencyList<TWeight>());
            }
        }

        public int Length { get { return _list.Length; } }

        public override string ToString()
        {
            var listItems = _list.Select((item, i) => string.Format("{0} => {1}", i, item == null ? "" : item.ToString()));
            return string.Join(" || ", listItems);
        }
    }

    public class PrimsAlgorithm
    {
        public static IEnumerable<Edge<decimal>> ConnectAllPoints(int pointCount, Point[] points)
        {
            //make edge for all points to all other points except self
            //  with weight = SQRT( SQR(x1 - x2) + SQR(y1 - y2))
            //  left = index of point
            //  right = index of to point
            var lines =
                from x in Enumerable.Range(0, pointCount)
                from y in Enumerable.Range(0, pointCount)
                where x != y
                let xp = points[x]
                let yp = points[y]
                select new Edge<decimal> { Left = x, Right = y, Weight = GetDistance(xp, yp) };
            return lines;
        }

        private static decimal GetDistance(Point a, Point b)
        {
            var value = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            return Convert.ToDecimal(value);
        }

    }
     public class SearchData<T> where T : IEquatable<T>
    {
        private readonly T[] _values;
        public readonly T InitialValue;
        public SearchData(int size, T initialValue)
        {
            InitialValue = initialValue;
            _values = Enumerable.Repeat(initialValue, size).ToArray();
        }
        
        public bool Visited(int v)
        {
            return !_values[v].Equals(InitialValue);
        }

        public int Length {get { return _values.Length; }}
        public ICollection<T> Values { get { return _values; } }
        public virtual void SetValue(int v, T value)
        {
            _values[v] = value;
        }
        public virtual T GetValue(int v)
        {
            return _values[v];
        }

        public override string ToString()
        {
            var items = _values.Select((v, i) => string.Format("{0}:{1}", i, v));
            return string.Join(", ", items);
        }
    }
}
