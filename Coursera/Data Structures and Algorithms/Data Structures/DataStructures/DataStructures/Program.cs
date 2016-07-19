using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process(Tree.Process);
        }
        private static void Process(Func<string[],string[]> process)
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
}
