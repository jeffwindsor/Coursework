using NUnit.Framework;

namespace DataStructures.W3
{
    [TestFixture]
    public class Tests : BaseTests
    {
        const string path = @"W3 - Hash Tables\";
        const string location_phonebook = path + "1 phone_book";
        const string location_hash_chains = path + "2 hash_chains";
        const string location_hash_substring = path + "3 hash_substring";

        [Test]
        public void PhonebookTests()
        {
            TestDirectory(location_phonebook, Phonebook.Process);
        }

        [Test]
        public void Hash_chainsTests()
        {
            //TestDirectory(location_hash_chains, HashChains.Process);
        }

        [Test]
        public void Hash_substringTests()
        {
            //TestDirectory(location_hash_substring, MergingTables.Process);
        }
    }
}
