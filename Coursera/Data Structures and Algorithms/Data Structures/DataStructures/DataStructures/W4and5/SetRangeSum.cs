using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.W4and5
{
    public class SetRangeSum
    {
        //public static void Main(string[] args)
        //{
        //    string s;
        //    var inputs = new List<string>();
        //    while ((s = Console.ReadLine()) != null)
        //        inputs.Add(s);

        //    foreach (var result in Answer(inputs.ToArray()))
        //        Console.WriteLine(result);
        //}

        private const string Add = "+";
        private const string Del = "-";
        private const string Find = "?";
        private const string Sum = "s";
        private const long M = 1000000001;

        public static IList<string> Answer(IList<string> inputs)
        {
            var n = int.Parse(inputs[0]);
            var queries = Enumerable.Range(1, n)
                .Select(i =>
                {
                    var items = inputs[i].Split(new[] {' '});
                    return new Query
                    {
                        Action = items[0],
                        Value = long.Parse(items[1]),
                        RangeValue = (items.Length == 3) ? long.Parse(items[2]) : 0
                    };
                });


            
            var o = new SetRangeSum();
            var results = queries.Select(q => o.ProcessQuery(q));
            return results.ToArray();
        }

        private long _total = 0;
        public string ProcessQuery(Query q)
        {
            //switch (q.Action)
            //{
            //    case Add:
            //        Insert(q.Value);
            //        return 
            //    case Del:
            //        var f = Search(q.Value);
            //        if(f.Key == q.Value) f.Delete();
            //    case Find:
            //        var f = Search(q.Value);
            //        return (f.Key == q.Value) ? "Found" : "Not Found";
            //    case Sum:
            //    default:
            //        throw new ArgumentException("Action Unknown");
            //}
            throw new NotImplementedException();
        }
        
        public class Query
        {
            public string Action { get; set; }
            public long Value { get; set; }
            public long RangeValue { get; set; }
        }

    }
}

