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
            var gis = Inputs.AdjacencyListGraphDecimal(inputs);
            var pointsWithCount = gis.ToPoints();
            var k = gis.NextAsInt();

            var pointCount = pointsWithCount.Item1;
            var points = pointsWithCount.Item2.ToArray();
            var lines = PrimsAlgorithm.ConnectAllPoints(pointCount, points);

            var value = Calculate(k, pointCount, lines);
            var answer = value.ToString("0.0000000000");
            answer = answer.Remove(answer.Length - 1);  //Simulate truncate at 9
            return new[] { answer };
        }

        //Kruskals
        public static decimal Calculate(int numberOfClusters, int pointCount, IEnumerable<Edge<decimal>> lines)
        {
            const int NO_CLUSTER = -1;
            var clusterId = NO_CLUSTER;
            var clusters = new SearchData<int>(pointCount, NO_CLUSTER);
            var queue = new HashSet<int>(Enumerable.Range(0,pointCount));
            var d = decimal.MinValue;

            foreach (var line in lines.OrderBy(l => l.Weight))
            {
                if (!queue.Any())
                {
                    if (clusters.GetValue(line.Left) == clusters.GetValue(line.Right))
                        continue;
                    else
                        return line.Weight;
                }
                d = line.Weight;
                if (!queue.Contains(line.Left)) continue;
                

                queue.Remove(line.Left);
                if (clusters.GetValue(line.Right) == NO_CLUSTER)
                {
                    if(clusterId < numberOfClusters) clusterId++;

                    clusters.SetValue(line.Right, clusterId);
                    clusters.SetValue(line.Left, clusterId);
                }
                else
                {
                    clusters.SetValue(line.Left, clusters.GetValue(line.Right));
                }
            }

            return d;
        }
    }
}
