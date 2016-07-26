using System.Linq;
using NUnit.Framework;

namespace DataStructures.W2
{
    [TestFixture]
    public class WeekOneTests : BaseTests
    {
        const string path = @"W2 - Priority Queues and Disjoint Sets\";
        const string location_jobqueue = "2 job_queue";
        [Test]
        public void MakeHeapTests()
        {
            TestFromFiles(1, 2, path + "1 make_heap", MakeHeap.Process);
        }

        [Test]
        public void JobQueueTests()
        {
            //JobQueueTestsGenerateLargeNumberOfLargeTimes(4);
            TestFromFiles(1, 5, path + location_jobqueue, JobQueue.Process);

            //Test for 1000 jobs 10x9
        }

        [Test]
        public void MergingTablesTests()
        {
            
            //TestFromFiles(1, 22, path + "3 merging_tables", Network.Process);
        }

        const long JobQueueTests_max_t = 1000000000;
        private void JobQueueTestsGenerateLargeNumberOfLargeTimes(int id)
        {
            const int threadCount = 1;
            const int jobCount = 100000;
            var jobs = Enumerable.Range(0, jobCount)
                .Select(_ => JobQueueTests_max_t)
                .Select(i => i.ToString());
            var joblines = new[] { string.Format("{0} {1}", threadCount, jobCount), string.Join(" ", jobs) };
            var answerlines = Enumerable.Range(0, jobCount)
                .Select( i => i * JobQueueTests_max_t)
                .Select(i => string.Format("0 {0}", i));

            WriteTestFiles(id, path + location_jobqueue, joblines, answerlines);
        }
        
    }
}