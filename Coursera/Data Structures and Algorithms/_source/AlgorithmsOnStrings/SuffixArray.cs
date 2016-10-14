using System;
using System.Linq;

namespace AlgorithmsOnStrings
{
    public class SuffixArray
    {
        public string Text { get; private set; }
        private readonly int[] _order;

        public SuffixArray(string source)
        {
            Text = source;
            var order = SortCharacters(source);
            var classes = ComputeCharClasses(source, _order);
            var l = 1;
            while (l < source.Length)
            {
                order = SortDoubled(source, l, order, classes);
                classes = UpdateClasses(order, classes, l);
                l = 2*l;
            }
            _order = order;
        }

        public static int[] UpdateClasses(int[] order, int[] classes, int l)
        {
            var n = order.Length;
            var newClasses = new int[n];
            for (var i = 1; i < n; i++)
            {
                var cur = order[i];
                var prev = order[i - 1];
                var mid = cur + l;
                var midPrev = (prev + l)%n;
                var x = (classes[cur] != classes[prev] || classes[mid] != classes[midPrev]) ? 1 : 0;
                newClasses[cur] = newClasses[prev] + x;
            }
            return newClasses;
        }

        public static int[] SortDoubled(string source, int l, int[] order, int[] classes)
        {
            var sourceLength = source.Length;
            var count = new int[sourceLength];
            var newOrder = new int[sourceLength];
            for (var i = 0; i < sourceLength; i++)
            {
                count[classes[i]] = count[classes[i]] + 1;
            }
            for (var i = 1; i < sourceLength; i++)
            {
                count[i] = count[i] + count[i - 1];
            }
            for (var n = 1; n <= sourceLength; n++)
            {
                var i = sourceLength - n;
                var start = (order[i] - l + sourceLength) %sourceLength;
                var cl = classes[start];
                count[cl] = count[cl] - 1;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        public static int[] ComputeCharClasses(string source, int[] order)
        {
            var sourceLength = source.Length;
            var classes = new int[sourceLength];
            for (var i = 1; i < sourceLength; i++)
            {
                var x = (source[order[i]] != source[order[i - 1]]) ? 1 : 0;
                classes[order[i]] = classes[order[i - 1]] + x;
            }
            return classes;
        }

        public static int[] SortCharacters(string source)
        {
            var sourceLength = source.Length;
            const int alphaLength = 27;
            var order = new int[sourceLength];
            var count = new int[alphaLength];
            for (var i = 0; i < sourceLength; i++)
            {
                count[AlphaIndex(source[i])] = count[AlphaIndex(source[i])] + 1;
            }
            for (var i = 1; i < alphaLength; i++)
            {
                count[i] = count[i] + count[i - 1];
            }
            for (var n = 1; n <= sourceLength; n++)
            {
                var i = sourceLength - n;
                var ai = AlphaIndex(source[i]);
                count[ai] = count[ai] - 1;
                order[count[ai]] = i;
            }
            return order;
        }

        private static readonly int AlphaFloor = Convert.ToInt32('a') - 1;
        private static int AlphaIndex(char c)
        {
            return (c == '$') ? 0 : Convert.ToInt32(char.ToLower(c)) - AlphaFloor;
        }

    }
}
