using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W4
{
    public class Dijkstras
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
            var gi = new AdjacencyListGraphInput(inputs);
            var graph = gi.ToDirectedAdjacencyGraph();
            var points = gi.NextAsEdge();
            //Console.WriteLine(graph);

            var s = new DijkstrasAlgorithm(graph);
            var cost = s.LowestCostPath(points.Left,points.Right);
            
            return new[] { cost.ToString() };
        }
    }
}
