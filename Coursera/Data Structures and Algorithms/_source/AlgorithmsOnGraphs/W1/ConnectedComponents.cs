using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsOnGraphs.W1
{
    public class ConnectedComponents
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
            
            var dsf = new DepthFirstSearch(graph);
            dsf.Search();

            //Console.WriteLine(dsf.ToPrettyString());
            
            return new[] { dsf.ConnectedComponents.ToString() };
        }

        private static int GetIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }
    }
}
