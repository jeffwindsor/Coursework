using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnStrings.W1
{
    public class SharedString
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
            var builder = new SuffixTree.Builder(inputs);
            var one = builder.NextAsString();
            var two = builder.NextAsString();
            var text = string.Format("{0}#{1}$", one, two);
            var suffixTree = builder.ToSuffixTree(text);

            var shared = suffixTree.Nodes.Where(IsSharedNode);
            var smallest = shared.OrderBy(n => n.Length).FirstOrDefault();

            var answers = new [] { suffixTree.ToText(smallest)};
            return answers;
        }

        public static bool IsSharedNode(SuffixTree.Node node)
        {
            throw new NotImplementedException();
        }
    }
}
