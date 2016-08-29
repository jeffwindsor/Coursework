using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs.W2
{
    public class Acyclicity
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
            
            var answer = IsCyclic(graph) ? "1" : "0";

            return new[] { answer };
        }
        private static int GetIndex(string source)
        {
            return int.Parse(source) - 1; //input is 1 based return zero based
        }

        private static bool IsCyclic(ISearchableGraph g)
        {
            try
            {
                var dfe = new CyclicSearch(g);
                dfe.Search();
                return false;
            }
            catch (GraphCycleException)
            {
                return true;
            }
        }
    }
    public class CyclicSearch
    {
        private readonly ISearchableGraph _g;
        
        public CyclicSearch(ISearchableGraph g)
        {
            _g = g;
        }

        public virtual void Search()
        {
            for (var v = 0; v < _g.Size(); v++)
                Explore(v, new HashSet<int>());
        }
        
        protected virtual void Explore(int v, HashSet<int> ancestory)
        {
            if (ancestory.Contains(v))
                throw new GraphCycleException();

            ancestory.Add(v);
            foreach (var w in _g.Neighbors(v))
            {
                Explore(w, ancestory);
            }
            ancestory.Remove(v);
        }
    }

}
