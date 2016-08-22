using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W1
{
    public class Reachability
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
            var chars = new[] { ' ' };
            var line0 = inputs[0].Split(chars);
            var verticeCount = int.Parse(line0[0]);
            var edgeCount = int.Parse(line0[1]);

            var xs = Enumerable.Range(1, edgeCount)
                .Select(i =>
                {
                    var items = inputs[i].Trim().Split(chars, StringSplitOptions.RemoveEmptyEntries);
                    return new
                    {
                        Left = GetIndex(items[0]),
                        Right = GetIndex(items[1])
                    };
                });

            var graph = new GraphAdjacentyList(verticeCount);
            foreach (var x in xs)
            {
                graph.AddUndirectedEdge(x.Left, x.Right);
            }

            //Console.WriteLine(graph.ToPrettyString());

            var linePath = inputs[edgeCount + 1].Split(chars);
            var left = GetIndex(linePath[0]);
            var right = GetIndex(linePath[1]);

            var dsf = new DepthFirstSearch(graph);
            dsf.Explore(left);

            //Console.WriteLine(dsf.ToPrettyString());

            var hasPath = dsf.Visited(right) ? "1" : "0";
            return new[] { hasPath };
        }

        private static int GetIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }

        #region Classes

        #endregion
    }
}
