using System.Collections.Generic;

namespace AlgorithmsOnGraphs.W3
{
    public class FlightSegments
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
            var gi = new GraphInput(inputs);
            var graph = gi.ToDirectedAdjacencyGraph();
            var points = gi.NextAsEdge();

            //Console.WriteLine(graph);
            var s = new BreadthFirstSearch(graph);
            s.Search(points.Left);


            var answer = "";

            return new[] { answer };
        }
    }
}
