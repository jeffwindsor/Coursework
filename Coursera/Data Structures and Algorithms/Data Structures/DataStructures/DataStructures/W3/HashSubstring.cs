using System;
using System.Collections.Generic;
using System.Linq;

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
            var pattern = inputs[0].Trim().ToCharArray();
            var text = inputs[1].Trim().ToCharArray();

            var indexes = RabinKarp(pattern, text);
            var results = string.Join(" ", indexes.Select(i => i.ToString()));
                
            return new []{ results };
        }

        public static IEnumerable<int> RabinKarp(char[] pattern, char[] text)
        {
            const long p = 1000000007;
            const long x = 263; //random from 1 ot p-1
            Func<char[], long> hash = s => OptimizedPolyHashFunction(s, p, x);

            var patternHash = hash(pattern);
            var loopLength = text.Length - pattern.Length;
            var substringLength = pattern.Length - 1;
            var substring = new char[substringLength];

            var result = new List<int>();
            for (int i = 0; i < loopLength; i++)
            {
                Array.Copy(text, i, substring, 0, substringLength);
                if (patternHash != hash(substring)) continue;
                if (pattern == substring) result.Add(i);
            }
            return result;
        }
        public static bool AreEqual(char[] a, char[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }

        private static long OptimizedPolyHashFunction(char[] value, long p, long x)
        {
            long hash = 0;
            for (var i = value.Length - 1; i > -1; i--)
            {
                hash = (hash * x + value[i]) % p;
            }
            return hash;
        }
    }
}
