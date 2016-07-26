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
            TestFromFiles(1, 2, path + "1 make_heap", MakeHeap.Process);
        }

        [Test]
        public void JobQueueTests()
        {
            TestFromFiles(1, 2, path + "2 job_queue", JobQueue.Process);
        }

        [Test]
        public void MergingTablesTests()
        {
            //TestFromFiles(1, 22, path + "3 merging_tables", Network.Process);
        }
    }
}
