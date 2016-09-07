﻿using AlgorithmsOnGraphs.W4;
using NUnit.Framework;

namespace AlgorithmsOnGraphs.Tests.W4
{
    [TestFixture]
    public class NegativeCycleTests : BaseTests
    {
        [TestCase("Sample1")]
        [TestCase("Sample2")]
        public void TestFiles(string file)
        {
            TestFromRelativeFilePath(@"NegativeCycle\" + file, NegativeCycle.Answer);
        }
    }
}
