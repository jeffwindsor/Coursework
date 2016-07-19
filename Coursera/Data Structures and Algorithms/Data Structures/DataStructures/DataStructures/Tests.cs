using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BracketTests()
        {
            TestFromFiles(1, 54, @"W1 - Basic Data Structures\check_brackets_in_code", Brackets.Process);
        }

        [Test]
        public void TreeTests()
        {
            TestFromFiles(1, 24, @"W1 - Basic Data Structures\tree_height", Tree.Process);
        }

        private static void TestFromFiles(int start, int end, string location, Func<string[], string[]> getActual)
        {
            for (var i = start; i <= end; i++)
            {
                var name =
                    string.Format(
                        @"C:\Github\Coursework\Coursera\Data Structures and Algorithms\Data Structures\{0}\tests\{1:D2}",
                        location, i);
                var input = File.ReadAllLines(name);
                var expected = File.ReadAllLines(name + ".a");
                Console.WriteLine("[{0:D2}] {1} => {2}", i, input[0], expected[0]);

                getActual(input).Should().BeEquivalentTo(expected);
            }
        }
    }
}
