using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnStrings.W2
{
    public class BwtInvert
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

        public static IList<string> Answer(IList<string> inputs)
        {
            var input = inputs.First();
            var answer = BurrowsWheelerInversion(input);
            return new[] { answer };
        }

        public static string BurrowsWheelerInversion(string input)
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
    }
}
