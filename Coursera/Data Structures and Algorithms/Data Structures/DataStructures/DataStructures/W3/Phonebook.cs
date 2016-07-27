using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.W3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process(Phonebook.Process);
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

    public class Phonebook
    {
        public static string[] Process(string[] inputs)
        {
            var chars = new[] { ' ' };
            var queryCount = int.Parse(inputs[0]);
            var queries = inputs.Skip(1).Take(queryCount).Select(l =>
            {
                var splits = l.Split(chars);
                return new Query(splits[0], splits[1], (splits.Length == 3) ? splits[2]: string.Empty);
            });

            return Process(queries)
                .ToArray();
        }
        public static IEnumerable<string> Process(IEnumerable<Query> queries)
        {
            throw new NotImplementedException();
        }

        public class Query
        {
            public Query(string c, string num, string name) { Command = c; Number = num; Name = name; }
            public string Command;
            public string Number;
            public string Name;
            public override string ToString()
            {
                return string.Format("{0} {1} {2}", Command, Number, Name);
            }
        }
    }
}
