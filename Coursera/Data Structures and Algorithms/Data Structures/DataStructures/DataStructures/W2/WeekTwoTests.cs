using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.W2
{
    [TestFixture]
    public class WeekOneTests : BaseTests
    {
        [Test]
        public void JobQueueTests()
        {
            //TestFromFiles(1, 54, @"W1 - Basic Data Structures\job_queue", Brackets.Process);
        }

        [Test]
        public void MakeHeapTests()
        {
            //TestFromFiles(1, 24, @"W1 - Basic Data Structures\make_heap", Tree.Process);
        }

        [Test]
        public void MergingTablesTests()
        {
            //TestFromFiles(1, 22, @"W1 - Basic Data Structures\merging_tables", Network.Process);
        }
    }
}
