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

        private const string COMMAND_ADD = "+";
        private const string COMMAND_DEL = "-";
        private const string COMMAND_FIND = "?";
        private const string COMMAND_SUM = "s";
        private const long M = 1000000001;

        private BinarySearchTreeNode _root;
        private long _lastSum = 0;

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


            var outputLine = 0;
            var o = new SetRangeSum();
            var results = queries
                .Select( (q,i) =>
                {
                    i += 2;  //for line number align in input file
                    if (q.Action == COMMAND_SUM)
                    {
                        outputLine++;
                        Console.WriteLine("[{5}:{6}] {0} [{1}:{2}] => [{3}:{4}]", q.Action, q.Value, q.RangeValue,
                            o.GetAdjustedValue(q.Value), o.GetAdjustedValue(q.RangeValue), i, outputLine);
                    }
                    else if (q.Action == COMMAND_FIND)
                    {
                        outputLine++;
                        Console.WriteLine("[{3}:{4}] {0} [{1}] => [{2}]", q.Action, q.Value, o.GetAdjustedValue(q.Value),
                            i, outputLine);
                    }
                    else
                    {
                        Console.WriteLine("[{3}:-] {0} [{1}] => [{2}]", q.Action, q.Value, o.GetAdjustedValue(q.Value),
                            i);
                    }
                    Console.WriteLine(BinarySearchTreeNodePrinter.Print(o._root));


                    switch (q.Action)
                    {
                        case COMMAND_ADD:
                            o.Insert(o.GetAdjustedValue(q.Value));
                            return string.Empty;
                        case COMMAND_DEL:
                            o.Delete(o.GetAdjustedValue(q.Value));
                            return string.Empty;
                        case COMMAND_FIND:
                            return o.Exists(o.GetAdjustedValue(q.Value)) ? "Found" : "Not found";
                        case COMMAND_SUM:
                            return o.Sum(o.GetAdjustedValue(q.Value), o.GetAdjustedValue(q.RangeValue)).ToString();
                        default:
                            throw new ArgumentException("Action Unknown");
                    }
                })
                .Where(r => !string.IsNullOrEmpty(r));


            return results.ToArray();
        }

        public long GetAdjustedValue(long source)
        {
            return (_lastSum + source)%M;
        }
        
        public void Insert(long key)
        {
            if (_root == null)
            {
                _root = new BinarySearchTreeNode {Key = key};
            }
            else
            {
                SplayTree.Insert(key, _root);
                _root = SplayTree.FindRoot(_root);
            }
        }

        public void Delete(long key)
        {
            if (_root == null) return;
            var with = SplayTree.Delete(key,_root);
            _root = SplayTree.FindRoot(with);
        }

        public bool Exists(long key)
        {
            return SplayTree.Exists(key, _root);
        }

        public long Sum(long leftKey, long rightKey)
        {
            _lastSum = SplayTree.Sum(leftKey,rightKey,_root);
            return _lastSum;
        }

        public class Query
        {
            public string Action { get; set; }
            public long Value { get; set; }
            public long RangeValue { get; set; }
        }
    }
}

