using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using System.Collections.Generic;

namespace DataStructures
{
    public abstract class BaseTests
    {
        const string input_ext = ".i";
        const string answer_ext = ".a";

        private static string CurrentPath()
        {
            return string.Format(@"{0}\..\..\..\..", System.AppDomain.CurrentDomain.BaseDirectory);
        }
        private static string CurrentTestPath(string location)
        {
            return string.Format(@"{0}\{1}\tests", CurrentPath(), location);
        }
        private static string CurrentTest(int id, string location)
        {
            return string.Format(@"{0}\{1:D2}", CurrentTestPath(location), id);
        }



        protected static void TestDirectory(string location, Func<string[], string[]> getActual)
        {
            var dir = CurrentTestPath(location);
            var files = Directory.GetFiles(dir)
                .Select(f => f.Replace(input_ext,"").Replace(answer_ext,""))
                .Distinct();
            foreach (var file in files)
            {
                TestFromFile(file, getActual);
            }            
        }

        protected static void TestFromFiles(int start, int end, string location, Func<string[], string[]> getActual)
        {
            for (var i = start; i <= end; i++)
            {
                TestFromFile(CurrentTest(i, location), getActual);
            }
        }
        protected static void TestFromFile(string test, Func<string[], string[]> getActual)
        {
            var input = File.ReadAllLines(test + input_ext);
            var expected = File.ReadAllLines(test + answer_ext);
            Console.WriteLine("[File {0}]", test);

            var actual = getActual(input);
            actual.Length.Should().Be(expected.Length);

            //Output
            for (int a = 0; a < actual.Length; a++)
            {
                Console.WriteLine("{0} : {1}", actual[a], expected[a]);
            }
            //Validate
            for (int a = 0; a < actual.Length; a++)
            {
                actual[a].Should().Be(expected[a]);
            }
        }


        protected static void WriteTestFiles(string name, string location, IEnumerable<string> lines, IEnumerable<string> answerLines)
        {
            var l = CurrentTestPath(location);
            File.WriteAllLines(l + @"\" + name + input_ext, lines);
            File.WriteAllLines(l + @"\" + name + answer_ext, answerLines);
        }


    }
}
