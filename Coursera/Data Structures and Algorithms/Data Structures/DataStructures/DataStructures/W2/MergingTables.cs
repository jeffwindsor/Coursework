using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.W2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process(MergingTables.Process);
        }
        private static void Process(Func<string[], string[]> process)
        {
            var input = new List<string>();
            string s;
            while ((s = Console.ReadLine()) != null)
            {
                input.Add(s);
            }

            foreach (var item in process(input.ToArray()))
            {
                Console.WriteLine(item);
            }
        }
    }

    public class MergingTables
    {
        public static string[] Process(string[] inputs)
        {
            var chars = new[] { ' ' };
            var splits0 = inputs[0].Split(chars);

            var tableCount = int.Parse(splits0[0]);
            var queryCount = int.Parse(splits0[1]);
            var tableRowCounts = inputs[1].Split(chars)
                .Select(int.Parse)
                .ToArray();

            var queries = inputs.Skip(2).Take(queryCount).Select(l => {
                var splits1 = l.Split(chars);
                return new Query { Destination = int.Parse(splits1[0]) - 1, Source = int.Parse(splits1[1]) - 1 };
                });

            return Process(tableCount, queryCount, tableRowCounts, queries)
                .Select(q => q.ToString())
                .ToArray();
        }

        public static IEnumerable<int> Process(int tableCount, int queryCount, int[] tableRowCounts, IEnumerable<Query> queries)
        {
            var results = new List<int>();
            var set = new DisjointSets(tableRowCounts);
            foreach (var q in queries)
            {
                set.Union(q.Destination, q.Source);
                results.Add(set.Max());
            }
            return results;
        }

        public class Query { public int Destination; public int Source; }

        private class DisjointSets
        {
            private int[] _rank;
            private int[] _parent;
            public DisjointSets(int[] ranks)
            {
                _parent = Enumerable.Range(0, ranks.Length).Select(i => i).ToArray();
                _rank = ranks;
            }

            public void MakeSet(int i) { _parent[i] = i; }
            public int Find(int i)
            {
                if (i != _parent[i])
                    _parent[i] = Find(_parent[i]);
                return _parent[i];
            }
            public int Max() { return _rank.Max(); }
            public void Union(int destination, int source)
            {
                var pdestination = Find(destination);
                var psource = Find(source);
                if (pdestination == psource) return;

                _rank[pdestination] += _rank[psource];
                _rank[psource] = 0;
                _parent[psource] = pdestination;
            }
        }
    }
}
