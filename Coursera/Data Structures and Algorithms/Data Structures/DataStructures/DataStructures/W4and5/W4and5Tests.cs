using NUnit.Framework;

namespace DataStructures.W4and5
{
    [TestFixture]
    public class W4and5Tests : BaseTests
    {

        [TestCase("01")]
        [TestCase("02")]
        public void TreeOrderTests(string file)
        {
            TestFromRelativeFilePath(@"W4and5\testfiles\TreeOrders\" + file, TreeOrder.Answer);
        }
        
        [TestCase("01")]
        [TestCase("04")]
        [TestCase("05")]
        [TestCase("20")]
        //[TestCase("36")]
        //[TestCase("83")]
        public void SetRangeSumTests(string file)
        {
            TestFromRelativeFilePath(@"W4and5\testfiles\SetRangeSum\" + file, SetRangeSum.Answer);
        }

        [Test]
        public void RopeTests()
        {
            //TestDirectory(location_hash_substring, Rope.Answer);
        }
    }
}
