using AlgorithmsOnGraphs.W5;
using NUnit.Framework;

namespace AlgorithmsOnGraphs.Tests.W5
{
    [TestFixture]
    public class ConnectingComponentsTests : BaseTests
    {
        [TestCase("Sample1")]
        [TestCase("Sample2")]
        public void TestFiles(string file)
        {
            TestFromRelativeFilePath(@"ConnectingComponents\" + file, ConnectingComponents.Answer);
        }
    }
}
