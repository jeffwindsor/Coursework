using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AlgorithmsOnGraphs.W5
{
    public class ConnectingComponents
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
            var gis = new AdjacencyListGraphInput(inputs);

            //read points with index
            //make edge for all points to all other points except self
            //  with weight = SQRT( SQR(x1 - x2) + SQR(y1 - y2))
            //  left = index of point
            //  right = index of to point
            //then build tree from edges

            var g = gis.ToUndirectedAdjacencyGraph();
            var result = PrimsAlgorithm(g);

            throw new NotImplementedException();
        }

        private static PrimsResult PrimsAlgorithm(AdjacencyListGraph graph)
        {
            var size = graph.Size();

            //Initialize result with 0 as first vertex
            var result = new PrimsResult(size);
            result.Cost.SetValue(0,0);

            //priority queue of costs
            var q = new MinPriorityQueue(size);
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
                Cost = new SearchData<long>(size, PositiveInfinity);
            }
            public SearchData<long> Cost { get; private set; }
            public SearchData<int> Parent { get; private set; }
        }
    }
}
