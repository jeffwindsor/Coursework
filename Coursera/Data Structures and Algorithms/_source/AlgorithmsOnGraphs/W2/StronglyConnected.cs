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

            var g = new AdjacencyListGraph(verticeCount);
            var rg = new AdjacencyListGraph(verticeCount);
            foreach (var x in xs)
            {
                g.AddDirectedEdge(x.Left, x.Right);
                rg.AddDirectedEdge(x.Right,x.Left);
            }

            //Console.WriteLine(g);
            //Console.WriteLine(rg);

            var s = StronglyConnectedComponents(g,rg);
            
            var answer = s.Count();

            return new[] { answer.ToString() };
        }
        private static int GetIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }
        private static string GetSource(int index)
        {
            return (index + 1).ToString(); //input is 1 based return zero based
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
