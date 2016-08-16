using DataStructures.W4and5;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    [TestFixture]
    public class RopeTests
    {
        [TestCase("111222", 3,"111","222", TestName = "Middle")]
        [TestCase("111222", 0, "", "111222", TestName = "Front")]
        [TestCase("111222", 5, "111222", "", TestName = "End")]
        public void SplitLeafTests(string original, int splitIndex, string expectedLeft, string expectedRight)
        {
            var node = Rope.Node.CreateLeaf(original);
            var result = Rope.SplitLeaf(node, splitIndex);
            AssertNodeSubString(result.Left, expectedLeft);
            AssertNodeSubString(result.Right, expectedRight);
        }

        private static void AssertNodeSubString(Rope.Node node, string expectedRight)
        {
            if (node == null)
                expectedRight.Should().Be("");
            else
                node.SubString.Should().Be(expectedRight);
        }
    }
}
