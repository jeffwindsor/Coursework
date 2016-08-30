using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W2
{
    public class StronglyConnected
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
            var graphInputs = new GraphInput(inputs);
            var g = graphInputs.ToDirectedAdjacencyGraph();
            //Console.WriteLine(g);
            var rg = graphInputs.ToDirectedReverseAdjacencyGraph();
            //Console.WriteLine(rg);

            var s = StronglyConnectedComponents(g,rg);
            var answer = s.Count();
            return new[] { answer.ToString() };
        }
        public static IEnumerable<int> StronglyConnectedComponents(ISearchableGraph graph, ISearchableGraph reverse)
        {
            //run dfs of reverse graph
            var srg = new TopologicalSort(reverse);
            srg.Search();

            //look for v in graph in reverse post order
            var sg = new DepthFirstSearch(graph);
            var order = srg.Order;
            foreach (var v in order)
            {
                //if not visited => explore and mark visted vertices as new component    
                sg.Explore(v);
            }
            return sg.Components;
        }
    }
}
