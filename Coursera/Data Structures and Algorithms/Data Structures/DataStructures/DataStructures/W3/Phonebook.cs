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
        const string NOT_FOUND = "not found";
        const string ADD = "add";
        const string FIND = "find";
        const string DEL = "del";

        public static string[] Process(string[] inputs)
        {
            var chars = new[] {' '};
            var queryCount = int.Parse(inputs[0]);
            var queries = inputs.Skip(1).Take(queryCount).Select(l =>
            {
                var splits = l.Split(chars);
                return new Query(splits[0], splits[1], (splits.Length == 3) ? splits[2] : string.Empty);
            });

            return Process(queries)
                .ToArray();
        }

        public static IEnumerable<string> Process(IEnumerable<Query> queries)
        {
            var pb = new Phonebook();
            var resutls = new List<string>();
            foreach (var query in queries)
            {
                switch (query.Command)
                {
                    case ADD:
                        pb.Add(query.Number, query.Name);
                        break;
                    case FIND:
                        pb.FindName(query.Number);
                        break;
                    case DEL:
                        break;
                    default:
                        throw new ArgumentException("Command Not Known");
                }
            }
        }


        
        private readonly Dictionary<string, int> _names = new Dictionary<string, int>();
        private readonly Dictionary<int, string> _numbers = new Dictionary<int, string>();

        public void Add(int number, string name)
        {
            _names[name] = number;
            _numbers[number] = name;
        }

        public void Remove(int number)
        {
            if (!_numbers.ContainsKey(number)) return;

            var name = _numbers[number];
            _numbers.Remove(number);
            _names.Remove(name);
        }

        public string FindNumber(string name)
        {
            return (_names.ContainsKey(name))
                ? _names[name].ToString()
                : NOT_FOUND;
        }

        public string FindName(int number)
        {
            return (_numbers.ContainsKey(number))
                ? _numbers[number]
                : NOT_FOUND;
        }


        public class Query
        {
            public Query(string c, string num, string name)
            {
                Command = c.ToLower();
                Number = num;
                Name = name;
            }

            public string Command;
            public string Number;
            public string Name;

            public override string ToString()
            {
                return $"{Command} {Number} {Name}";
            }
        }
    }
}
