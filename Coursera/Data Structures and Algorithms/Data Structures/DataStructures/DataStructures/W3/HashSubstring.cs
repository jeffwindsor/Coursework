using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.W3
{
    public class HashSubstring
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                Process(HashSubstring.Process);
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

        public static string[] Process(string[] inputs)
        {
            var pattern = inputs[0];
            var text = inputs[1];

            var results = string.Join(" ", Process(pattern, text).Select(i => i.ToString()));
                
            return new []{ results};
        }

        public static IEnumerable<int> Process(string pattern, string text)
        {
           throw new NotImplementedException();
        }

    }
}
