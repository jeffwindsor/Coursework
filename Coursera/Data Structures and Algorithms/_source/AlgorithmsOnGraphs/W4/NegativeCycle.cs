using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W4
{
    public class NegativeCycle
    {
        //public static void Main(string[] args)
        //{
        //    string s;
        //    var inputs = new List<string>();
        //    while ((s = Console.ReadLine()) != null)
        //        inputs.Add(s);

        //    foreach (var result in Answer(inputs.ToArray()))
        //        Console.WriteLine(result);
        //}

        public static IList<string> Answer(IList<string> inputs)
        {
            var gis = new AdjacencyListGraphInput(inputs);
            //Convert exchange rate to negative weight
            gis.WeightParseFunction = x => -Math.Log(x,2);
            var g = gis.ToEdges();

            var s = new NegativeCycle(g.Item1,g.Item2);
            var answer = s.HasNegativeCycle() ? "1" : "0";
            
            return new[] { answer };
        }

        private readonly List<Edge> _edges;
        private readonly int _size;
        private const double MaxDistance = double.PositiveInfinity;

        public NegativeCycle(int vertices, IEnumerable<Edge> edges)
        {
            _edges = edges.OrderBy(e=>e.Left).ToList();
            _size = vertices;
        }

        public bool HasNegativeCycle()
        {
            var result = BellmanFord(0, _size);
            return _edges.Any(e => Relax(e, result));
        }

        private class BellmanFordResult
        {
            public BellmanFordResult(int size)
            {
                Size = size;
                VisitedFrom = new SearchData<int>(size, -1);
                Distance = new SearchData<double>(size, MaxDistance);
            }
            public int Size { get; set; }
            public SearchData<double> Distance { get; set; }
            public SearchData<int> VisitedFrom { get; set; }
        }

        private BellmanFordResult BellmanFord(int start, int size)
        {
            var result = new BellmanFordResult(size);
            result.Distance.SetValue(start, 0);

            IEnumerable<Edge> workingEdges = _edges;
            for (var i = 0; i < _size; i++)
            {
                workingEdges = workingEdges.Where(e => Relax(e, result) || IsMaxDistance(e.Weight));
            }
            return result;
        }

        private static bool IsMaxDistance(double d)
        {
            return double.IsPositiveInfinity(d);
        }

        private static bool Relax(Edge e, BellmanFordResult r)
        {
            return Relax(e.Left, e.Right, e.Weight, r);
        }

        private static bool Relax(int left, int right, double weight, BellmanFordResult r)
        {
            var leftDistance = r.Distance.GetValue(left);
            var relaxedDistance = IsMaxDistance(leftDistance)
                ? leftDistance
                : leftDistance + weight;
            var currentDistance = r.Distance.GetValue(right);

            if (currentDistance <= relaxedDistance) return false;

            r.Distance.SetValue(right, relaxedDistance);
            r.VisitedFrom.SetValue(right, left);
            return true;
        }
    }
}
