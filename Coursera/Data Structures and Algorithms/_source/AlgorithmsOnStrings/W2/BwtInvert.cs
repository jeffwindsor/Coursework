using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnStrings.W2
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
            var fItems = GetItems(first).ToList();
            var lItems = GetItems(input).ToList();

            //By String Index
            var si = from f in fItems
                     join l in lItems on f.StringIndex equals l.StringIndex
                     select new Pair { First = f, Last = l };
            var rows = si.ToDictionary(p => p.First.StringIndex);

            //By Char Occurance
            var co = from f in fItems
                     join l in lItems
                        on new { f.Char, f.Occurance } equals new { l.Char, l.Occurance }
                     select new Pair { First = f, Last = l };
            var charOccuranceTwins = co.ToDictionary(p => p.First.CharOccuranceId());

            //Find Result
            var sb = new System.Text.StringBuilder();
            var rowId = 0;
            do
            {
                var row = rows[rowId];
                sb.Append(row.First.Char);
                //Move Current Row's Last Char Occurance Twin (in First Column)
                rowId = charOccuranceTwins[row.Last.CharOccuranceId()].First.StringIndex;
            }
            while (rowId != 0);

            return new string(sb.ToString().Reverse().ToArray());
        }

        private static readonly int AlphaFloor = Convert.ToInt32('a') - 1;
        private static int GetAlphaIndex(char c)
        {
            return (c == '$') ? 0 : Convert.ToInt32(Char.ToLower(c)) - AlphaFloor;
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
        public class Pair
        {
            public Item First;
            public Item Last;
        }

        public class Item
        {
            public char Char;
            public int Occurance;
            public int StringIndex;
            public string CharOccuranceId() { return string.Format("{0}{1}", Char, Occurance); }
        }

    //public static string BurrowsWheelerInversionNaive(string input)
    //{
    //    var length = input.Length;
    //    //Original Matrix = First Column, sorted version of input
    //    var matrix = input.ToCharArray()
    //        .OrderBy(c => c)
    //        .Select(c => c.ToString());
    //    for (int i = 1; i < length; i++)
    //    {
    //        matrix = Enumerable
    //            .Zip(matrix, input, (s, c) => string.Format("{0}{1}", c, s))
    //            .OrderBy(s => s);
    //    }
    //    var result = matrix.First(s => s.EndsWith("$"));
    //    return result;
    //}

    //public class BurrowsWheelerInverter
    //    {
    //        private readonly Dictionary<int, Pair> _rows; // index by string index
    //        private readonly Dictionary<string, Pair> _charOccuranceTwins; // index by char then occurance
    //        public BurrowsWheelerInverter(string first, string last)
    //        {
    //            var fItems = GetItems(first).ToList();
    //            var lItems = GetItems(last).ToList();

    //            //By String Index
    //            var si = from f in fItems
    //                     join l in lItems on f.StringIndex equals l.StringIndex
    //                     select new Pair { First = f, Last = l };
    //            _rows = si.ToDictionary(p => p.First.StringIndex);

    //            //By Char Occurance
    //            var co = from f in fItems
    //                     join l in lItems 
    //                        on new { f.Char, f.Occurance } equals new { l.Char, l.Occurance }
    //                     select new Pair { First = f, Last = l };
    //            _charOccuranceTwins = co.ToDictionary(p => p.First.CharOccuranceId());
    //        }
    //        public string ToOriginalString()
    //        {
    //            var sb = new System.Text.StringBuilder();
    //            var rowId = 0;
    //            do
    //            {
    //                var row = _rows[rowId];
    //                sb.Insert(0,row.First.Char);
    //                //Find Row Id of Twin in First Column
    //                rowId = _charOccuranceTwins[row.Last.CharOccuranceId()].First.StringIndex;
    //            }
    //            while (rowId != 0);

    //            return  sb.ToString();
    //        }
    //        private static readonly int AlphaFloor = Convert.ToInt32('a') - 1;
    //        private static int GetAlphaIndex(char c)
    //        {
    //            return (c == '$') ? 0 : Convert.ToInt32(Char.ToLower(c)) - AlphaFloor;
    //        }
    //        private static IEnumerable<Item> GetItems(string s)
    //        {
    //            char[] chars = s.ToCharArray();
    //            int[] occurances = Enumerable.Range(0, 27).Select(_ => 0).ToArray();
    //            return chars.Select((c, stringIndex) =>
    //            {
    //                var alphaIndex = GetAlphaIndex(c);
    //                occurances[alphaIndex] += 1;
    //                return new Item { Char = c, StringIndex = stringIndex, Occurance = occurances[alphaIndex] };
    //            });
    //        }
            
    //        private class Pair
    //        {
    //            public Item First;
    //            public Item Last;
    //        }

    //        private class Item
    //        {
    //            public char Char;
    //            public int Occurance;
    //            public int StringIndex;
    //            public string CharOccuranceId() { return string.Format("{0}{1}", Char, Occurance); }
    //        }
    //    }
    }
}
