using System;
using DataStructures.W4and5;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    [TestFixture]
    public class RopeTests
    {

        [TestCase(07,11, "Hello_me_is_Simon", "my_na", TestName = "Remove : Deep Left and Right Disconnected")]
        [TestCase(1, 9, "name_is_Simon", "Hello_my_", TestName = "Remove : left branch")]
        [TestCase(15, 22, "name_is_Simon", "Hello_my_", TestName = "Remove : right branch")]
        public void Remove_WikiExample_Test(int startIndex, int endIndex, string expectedLeft, string expectedRight)
        {
            var result = Rope.Remove(startIndex, endIndex, WikiTree());
            AssertNodePair(expectedLeft, expectedRight, result);
        }
        [TestCase(11, "Hello_my_na", "me_is_Simon", TestName = "Split : End of Left")]
        [TestCase(10, "Hello_my_n", "ame_is_Simon", TestName = "Split : Middle of Left")]
        [TestCase(09, "Hello_my_", "name_is_Simon", TestName = "Split : End of Right")]
        [TestCase(12, "Hello_my_nam", "e_is_Simon", TestName = "Split : Middle of Right")]
        public void Split_WikiExample_Test(int index, string expectedLeft, string expectedRight)
        {
            var result = Rope.Split(WikiTree(), index);
            AssertNodePair(expectedLeft, expectedRight, result);
        }

        [TestCase("111222", 3,"111","222", TestName = "Middle")]
        [TestCase("111222", 6, "111222", "", TestName = "End")]
        [TestCase("111222", 1, "1", "11222", TestName = "Front")]
        [TestCase("111222", 5, "11122", "2", TestName = "Five")]
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
            {
                node.IsLeaf.Should().BeTrue();
                node.SubString.Length.Should().BeGreaterThan(0);
                node.SubString.Should().Be(expectedRight);
            }
        }

        private static void AssertNodePair(string expectedLeft, string expectedRight, Rope.NodePair result)
        {
            Console.WriteLine("{0} => {1}", expectedLeft, result.Left.ToInOrderString());
            Console.WriteLine(result.Left.ToTreeString());
            Console.WriteLine();
            Console.WriteLine("{0} => {1}", expectedRight, result.Right.ToInOrderString());
            Console.WriteLine(result.Right.ToTreeString());

            result.Left.ToInOrderString().Should().Be(expectedLeft);
            result.Right.ToInOrderString().Should().Be(expectedRight);
        }

        private static Rope.Node WikiTree()
        {
            return
                Rope.Node.CreateConcatenation( //22
                    Rope.Node.CreateConcatenation( //9
                        Rope.Node.CreateConcatenation( //6
                            Rope.Node.CreateLeaf("Hello_"), //6
                            Rope.Node.CreateLeaf("my_") //3
                            ),
                        Rope.Node.CreateConcatenation( //6
                            Rope.Node.CreateConcatenation( //2
                                Rope.Node.CreateLeaf("na"), //6
                                Rope.Node.CreateLeaf("me_i") //3
                                ),
                            Rope.Node.CreateConcatenation( //1
                                Rope.Node.CreateLeaf("s"), //1
                                Rope.Node.CreateLeaf("_Simon") //6
                                )
                            )
                        ),
                    null
                    );
        }
    }
}


//var node = Rope.Node.CreateConcatenation(
//                Rope.Node.CreateLeaf("1"),
//                Rope.Node.CreateConcatenation(
//                    Rope.Node.CreateConcatenation(
//                        Rope.Node.CreateConcatenation(
//                            Rope.Node.CreateLeaf("2"),
//                            Rope.Node.CreateLeaf("3")
//                            ),
//                        Rope.Node.CreateLeaf("4")
//                        ),
//                    Rope.Node.CreateLeaf("5")
//                    )
//                );