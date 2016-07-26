using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using System.Collections.Generic;

namespace DataStructures
{
    public abstract class BaseTests
    {

        private static string CurrentPath()
        {
            return string.Format(@"{0}\..\..\..\..", System.AppDomain.CurrentDomain.BaseDirectory);
        }
        private static string CurrentTestPath(string location)
        {
            return string.Format(@"{0}\{1}\tests", CurrentPath(), location);
        }
        private static string CurrentTestFile(int id, string location)
        {
            return string.Format(@"{0}\{1:D2}.i", CurrentTestPath(location), id);
        }
        private static string CurrentTestAnswerFile(int id, string location)
        {
            return string.Format(@"{0}\{1:D2}.a", CurrentTestPath(location), id);
        }


        protected static void TestFromFiles(int start, int end, string location, Func<string[], string[]> getActual)
        {
            for (var i = start; i <= end; i++)
            {
                TestFromFile(i, location, getActual);
            }
        }
        protected static void TestFromFile(int i, string location, Func<string[], string[]> getActual)
        {
            var name = CurrentTestFile(i, location);            
            var input = File.ReadAllLines(File.Exists(name)?name:name.Replace(".i",""));
            var expected = File.ReadAllLines(CurrentTestAnswerFile(i, location));
            Console.WriteLine("[File {0:D2}]", i);

            var actual = getActual(input);
            actual.Length.Should().Be(expected.Length);
            for (int a = 0; a < actual.Length; a++)
            {
                Console.WriteLine("{0} : {1}", actual[a], expected[a]);
            }
            for (int a = 0; a < actual.Length; a++)
            {
                actual[a].Should().Be(expected[a]);
            }
        }


        protected static void WriteTestFiles(int i, string location, IEnumerable<string> lines, IEnumerable<string> answerLines)
        {
            File.WriteAllLines(CurrentTestFile(i, location), lines);
            File.WriteAllLines(CurrentTestAnswerFile(i, location), answerLines);
        }


    }
}
