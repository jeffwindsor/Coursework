using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.W4and5
{
    public class TreeOrder
    {
        public static void Main(string[] args)
        {
            string s;
            var inputs = new List<string>();
            while ((s = Console.ReadLine()) != null)
                inputs.Add(s);

            foreach (var result in Answer(inputs.ToArray()))
                Console.WriteLine(result);
        }

        //Use this for test input
        public static IEnumerable<string> Answer(string[] inputs)
        {
            var n = int.Parse(inputs[0]);
            var nodes = Enumerable.Range(1, n)
                .Select(i =>
                {
                    var items = inputs[i].Split(new[] {' '});
                    return new Node
                    {
                        Key = long.Parse(items[0]),
                        Left = int.Parse(items[1]),
                        Right = int.Parse(items[2])
                    };
                });

            var results = new List<string>();
            var o = new TreeOrder(nodes);
            results.Add(string.Join(" ", o.InOrder()));
            results.Add(string.Join(" ", o.PreOrder()));
            results.Add(string.Join(" ", o.PostOrder()));
            return results;
        }

        private readonly Node[] _nodes;
        public TreeOrder(IEnumerable<Node> nodes){ _nodes = nodes.ToArray();}

        public IEnumerable<long> InOrder()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<long> PreOrder()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<long> PostOrder()
        {
            throw new NotImplementedException();
        }

        public class Node
        {
            public long Key { get; set; }
            public int Left { get; set; }
            public int Right { get; set; }
        }
    }
}
