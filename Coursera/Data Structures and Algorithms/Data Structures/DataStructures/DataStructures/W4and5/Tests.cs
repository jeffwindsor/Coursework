using NUnit.Framework;

namespace DataStructures.W4and5
{
    [TestFixture]
    public class Tests : BaseTests
    {
        const string path = @"W4 - Binary Search Trees\";
        const string location_phonebook = path + "1tree_orders";
        const string location_hash_chains = path + "2set_range_sum";
        const string location_hash_substring = path + "3rope";

        [Test]
        public void TreeOrderTests()
        {
           //TestDirectory(location_phonebook, TreeOrder.Process);
        }

        [Test]
        public void SetRangeSumTests()
        {
            //TestDirectory(location_hash_chains, SetRangeSum.Process);
        }

        [Test]
        public void RopeTests()
        {
            //TestDirectory(location_hash_substring, Rope.Process);
        }
    }
}
