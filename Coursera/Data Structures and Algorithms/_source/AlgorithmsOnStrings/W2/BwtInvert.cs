using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnStrings.W1
{
    public class BwtInvert
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
            var input = inputs.First();
            var answer = BurrowsWheelerInversion(input);
            return new[] { answer };
        }
        public static string BurrowsWheelerInversion(string input)
        {
            var first = string.Concat(input.OrderBy(c=>c));
            var bwi = new BurrowsWheelerInverter(first, input);
            return bwi.ToOriginalString();
        }

        public static string BurrowsWheelerInversionNaive(string input)
        {
            var length = input.Length;
            //Original Matrix = First Column, sorted version of input
            var matrix = input.ToCharArray()
                .OrderBy(c => c)
                .Select(c => c.ToString());
            for (int i = 1; i < length; i++)
            {
                matrix = Enumerable
                    .Zip(matrix, input, (s, c) => string.Format("{0}{1}", c, s))
                    .OrderBy(s => s);
            }
            var result = matrix.First(s => s.EndsWith("$"));
            return result;
        }

        public class BurrowsWheelerInverter
        {
            private readonly Dictionary<int, Pair> _byStringIndex; // index by string index
            private readonly Dictionary<string, Pair> _byCharOccurance; // index by char then occurance
            public BurrowsWheelerInverter(string first, string last)
            {
                var fItems = GetItems(first).ToList();
                var lItems = GetItems(last).ToList();

                //By String Index
                var si = from f in fItems
                         join l in lItems on f.StringIndex equals l.StringIndex
                         select new Pair { First = f, Last = l };
                _byStringIndex = si.ToDictionary(p => p.First.StringIndex);

                //By Char Occurance
                var co = from f in fItems
                         join l in lItems 
                            on new { f.Char, f.Occurance } equals new { l.Char, l.Occurance }
                         select new Pair { First = f, Last = l };
                _byCharOccurance = co.ToDictionary(p => p.First.CharOccurance());
            }
            public string ToOriginalString()
            {
                var sb = new System.Text.StringBuilder();
                var row = _byStringIndex[0];
                while (row.Last.Char != '$')
                {
                    sb.Append(row.First.Char);
                    row = _byCharOccurance[row.Last.CharOccurance()];
                }
                return sb.ToString();
            }
            private static readonly int alphaFloor = Convert.ToInt32('a') - 1;
            private static int GetAlphaIndex(char c)
            {
                return (c == '$') ? 0 : Convert.ToInt32(Char.ToLower(c)) - alphaFloor;
            }
            private static IEnumerable<Item> GetItems(string s)
            {
                char[] chars = s.ToCharArray();
                int[] occurances = Enumerable.Range(0, 27).Select(_ => 0).ToArray();
                return chars.Select((c, stringIndex) =>
                {
                    var alphaIndex = GetAlphaIndex(c);
                    occurances[alphaIndex] += 1;
                    return new Item { Char = c, StringIndex = stringIndex, Occurance = occurances[alphaIndex] };
                });
            }
            
            private class Pair
            {
                public Item First;
                public Item Last;
            }

            private class Item
            {
                public char Char;
                public int Occurance;
                public int StringIndex;
                public string CharOccurance() { return string.Format("{0}{1}", Char, Occurance); }
            }
        }
    }
}
