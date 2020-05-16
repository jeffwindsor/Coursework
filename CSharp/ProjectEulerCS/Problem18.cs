using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ProjectEulerCSharp
{
    [TestFixture]
    public class Problem18
    {
        private int[][] triangle = new int[][]
                           {
                               new int[] {75},
                               new int[] {95, 64},
                               new int[] {17, 47, 82},
                               new int[] {18, 35, 87, 10},
                               new int[] {20, 04, 82, 47, 65},
                               new int[] {19, 01, 23, 75, 03, 34},
                               new int[] {88, 02, 77, 73, 07, 63, 67},
                               new int[] {99, 65, 04, 28, 06, 16, 70, 92},
                               new int[] {41, 41, 26, 56, 83, 40, 80, 70, 33},
                               new int[] {41, 48, 72, 33, 47, 32, 37, 16, 94, 29},
                               new int[] {53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14},
                               new int[] {70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57},
                               new int[] {91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48},
                               new int[] {63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31},
                               new int[] {04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23}
                           };
        [Test]
        public void FindTheMaximumTotalFromTopToBottomOfTheTriangle()
        {
            var maxSum = GetMaxGraphSum(triangle);
            Trace.WriteLine(string.Format("MaxSum: {0}", maxSum));
            Assert.AreEqual(1074, maxSum);
        }
        
        private static int GetMaxGraphSum(int[][] triangle)
        {
            for (var i = triangle.Length - 1; i > 0; --i)
            {
                triangle[i - 1] = GetMaxGraphSum(triangle[i - 1], triangle[i]);
            }
            return triangle[0][0];
        }

        private static int[] GetMaxGraphSum(int[] n, int[] n1)
        {
            //length check
            for (var i = 0; i < n.Length; ++i)
            {
                n[i] = Math.Max((n[i] + n1[i]), (n[i] + n1[i + 1]));
            }
            return n;
        }
    }
}
