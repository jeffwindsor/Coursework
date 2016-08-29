using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsOnGraphs.W2
{
    public class Topsort
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

            var graph = new AdjacencyListGraph(verticeCount);
            foreach (var x in xs)
            {
                graph.AddDirectedEdge(x.Left, x.Right);
            }

            //Console.WriteLine(graph);
            var s = new TopologicalSort(graph);
            s.Search();
            var answer = string.Join(" ", s.Order.Select(GetSource));
            
            return new[] { answer };
        }
        private static int GetIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }
        private static string GetSource(int index)
        {
            return (index + 1).ToString(); //input is 1 based return zero based
        }
    }
}
