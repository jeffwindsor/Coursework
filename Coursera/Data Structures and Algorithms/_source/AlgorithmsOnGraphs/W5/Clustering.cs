using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W5
{
    public class Clustering
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
            var gis = Inputs.AdjacencyListGraphLong(inputs);
            var g = gis.ToEdges();
            var start = gis.NextAsIndex();


            throw new NotImplementedException();

        }
    }
}
