using System;
using System.IO;
using System.Linq;
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
            TestFromFiles(1, 54, @"W1 - Basic Data Structures\check_brackets_in_code", W1.Brackets.Process);
        }

        [Test]
        public void TreeTests()
        {
            TestFromFiles(1, 24, @"W1 - Basic Data Structures\tree_height", W1.Tree.Process);
        }

        [Test]
        public void Network()
        {
            TestFromFiles(1, 22, @"W1 - Basic Data Structures\network_packet_processing_simulation", W1.Network.Process);
            //TestFromFile(19, @"W1 - Basic Data Structures\network_packet_processing_simulation", W1.Network.Process);
        }

        private static void TestFromFiles(int start, int end, string location, Func<string[], string[]> getActual)
        {
            for (var i = start; i <= end; i++)
            {
                TestFromFile(i, location, getActual);
            }
        }
        private static void TestFromFile(int i, string location, Func<string[], string[]> getActual)
        {
            var name = string.Format(@"C:\Github\Coursework\Coursera\Data Structures and Algorithms\Data Structures\{0}\tests\{1:D2}", location, i);
            var input = File.ReadAllLines(name);
            var expected = File.ReadAllLines(name + ".a");
            Console.WriteLine("[{0:D2}] {1} => {2}", i, input.FirstOrDefault(), expected.FirstOrDefault());
            var actual = getActual(input);

            actual.Length.Should().Be(expected.Length);
            for (int a = 0; a < actual.Length; a++)
            {
                actual[a].Should().Be(expected[a]);
            }
            
        }
    }
}
