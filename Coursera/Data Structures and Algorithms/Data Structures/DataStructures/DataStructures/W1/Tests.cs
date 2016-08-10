using NUnit.Framework;

namespace DataStructures.W1
{
    [TestFixture]
    public class Tests : BaseTests
    {
        [Test]
        public void CheckBracketsInCodeTests()
        {
            TestFromFiles(1, 54, @"W1 - Basic Data Structures\1 check_brackets_in_code", CheckBracketsInCode.Answer);
        }

        [Test]
        public void TreeHeightTests()
        {
            TestFromFiles(1, 24, @"W1 - Basic Data Structures\2 tree_height", TreeHeight.Answer);
        }

        [Test]
        public void NetworkPacketProcessingSimulationTests()
        {
            TestFromFiles(1, 22, @"W1 - Basic Data Structures\3 network_packet_processing_simulation", NetworkPacketProcessingSimulation.Answer);
            //TestFromFile(19, @"W1 - Basic Data Structures\network_packet_processing_simulation", W1.Network.Answer);
        }
    }
}
