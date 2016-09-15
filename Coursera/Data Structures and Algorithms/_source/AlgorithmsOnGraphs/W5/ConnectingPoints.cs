using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W5
{
    public class ConnectingPoints
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

        private const long PositiveInfinity = long.MaxValue;
        public static IList<string> Answer(IList<string> inputs)
        {
            var gis = Inputs.AdjacencyListGraphDecimal(inputs);
            var points = gis.ToPoints();
            var verticeCount = points.Item1;
            var vertices = points.Item2.ToArray();

            //make edge for all points to all other points except self
            //  with weight = SQRT( SQR(x1 - x2) + SQR(y1 - y2))
            //  left = index of point
            //  right = index of to point
            var lines = 
                from x in Enumerable.Range(0, verticeCount)
                from y in Enumerable.Range(0, verticeCount)
                where x != y
                let xp = vertices[x]
                let yp = vertices[y]
                select new Edge<decimal> {Left = x, Right = y, Weight = GetDistance(xp,yp)};
            //then build tree from edges
            var g = gis.ToUndirectedAdjacencyGraph(new Tuple<int, IEnumerable<Edge<decimal>>>(verticeCount,lines));
            var primsResult = PrimsAlgorithm(g);
            var answer = primsResult.Cost.Values.Sum();

            return new [] { answer.ToString("0.000000000") };
        }

        private static decimal GetDistance(Point a, Point b)
        {
            var value = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            return Convert.ToDecimal(value);
        }

        private static PrimsResult PrimsAlgorithm(AdjacencyListGraph<decimal> graph)
        {
            var size = graph.Size();

            //Initialize result with 0 as first vertex
            var result = new PrimsResult(size);
            result.Cost.SetValue(0,0);

            //priority queue of costs
            var q = new MinPriorityQueue<decimal>(size, decimal.MaxValue);
            for (var i = 0; i < size; i++)
            {
                q.Enqueue(i,result.Cost.GetValue(i));
            }

            //Walk
            while (!q.IsEmpty())
            {
                //Console.WriteLine("Queue: {0}", q);
                var currentIndex = q.Dequeue();

                //Console.WriteLine("Extract: {0}", currentIndex);
                foreach (var edge in graph.Neighbors(currentIndex))
                {
                    var z = edge.Right;
                    var w = edge.Weight;
                    if (!q.Contains(z) || result.Cost.GetValue(z) <= w) continue;

                    result.Cost.SetValue(z,w);
                    result.Parent.SetValue(z,currentIndex);
                    q.ChangePriority(z,w);
                }
            }
            return result;
        }


        private class PrimsResult
        {
            public PrimsResult(int size)
            {
                Parent = new SearchData<int>(size, -1);
                Cost = new SearchData<decimal>(size, PositiveInfinity);
            }
            public SearchData<decimal> Cost { get; private set; }
            public SearchData<int> Parent { get; private set; }
        }
    }
}
