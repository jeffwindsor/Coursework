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

            //BellmanFordAlgorithm
            var s = new NegativeCycle(gis.ToEdges());
            var cycle = s.HasNegativeCycle();
            
            return new[] { cycle ? "1" : "0" };
        }

        private readonly List<Edge> _edges;
        private readonly int _size;
        private SearchData<double> _distance;
        private SearchData<int> _visitedFrom;
        private const double MaxDistance = double.PositiveInfinity;

        public NegativeCycle(Tuple<int, IEnumerable<Edge>> edges)
        {
            _edges = edges.Item2.ToList();
            _size = edges.Item1;
        }

        public bool HasNegativeCycle()
        {
            Explore(0);
            return _edges.Any(e => Relax(e.Left, e.Right, e.Weight));
        }

        private void Explore(int start)
        {
            _visitedFrom = new SearchData<int>(_size,-1);
            _distance = new SearchData<double>(_size, MaxDistance);
            _distance.SetValue(start, 0);

            for (var i = 0; i < _size; i++)
            {
                foreach (var e in _edges)
                {
                    Relax(e.Left, e.Right, e.Weight);
                }
            }
        }

        private bool Relax(int left, int right, double weight)
        {
            var relaxedDistance = double.IsPositiveInfinity(_distance.GetValue(left))
                ? MaxDistance
                : _distance.GetValue(left) + weight;
            var currentDistance = _distance.GetValue(right);

            if (currentDistance <= relaxedDistance) return false;

            _distance.SetValue(right, relaxedDistance);
            _visitedFrom.SetValue(right, left);
            return true;
        }
    }
}
