using NUnit.Framework;

namespace DataStructures.W2
{
    [TestFixture]
    public class WeekOneTests : BaseTests
    {
        const string path = @"W2 - Priority Queues and Disjoint Sets\";

        [Test]
        public void MakeHeapTests()
        {
            TestFromFiles(1, 2, path + "make_heap", MakeHeap.Process);
        }

        [Test]
        public void JobQueueTests()
        {
            //TestFromFiles(1, 54, path + "job_queue", JobQueue.Process);
        }

        [Test]
        public void MergingTablesTests()
        {
            //TestFromFiles(1, 22, path + "merging_tables", Network.Process);
        }
    }
}
