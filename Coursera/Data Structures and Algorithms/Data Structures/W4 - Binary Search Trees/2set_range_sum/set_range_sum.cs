using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.W4and5
{
    public class SetRangeSum
    {
        public static void Main(string[] args)
        {
            string s;
            var inputs = new List<string>();
            while ((s = Console.ReadLine()) != null)
                inputs.Add(s);

            foreach (var result in Answer(inputs.ToArray()))
                Console.WriteLine(result);
        }

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

            var o = new SetRangeSum();
            var results = queries
                .Select(q =>
                {
                    switch (q.Action)
                    {
                        case COMMAND_ADD:
                            //Console.WriteLine("{0} [{1}] => [{2}]", q.Action, q.Value, o.GetAdjustedValue(q.Value));
                            o.Insert(o.GetAdjustedValue(q.Value));
                            return string.Empty;
                        case COMMAND_DEL:
                            //.WriteLine("{0} [{1}] => [{2}]", q.Action, q.Value, o.GetAdjustedValue(q.Value));
                            o.Delete(o.GetAdjustedValue(q.Value));
                            return string.Empty;
                        case COMMAND_FIND:
                            //Console.WriteLine("{0} [{1}] => [{2}]", q.Action, q.Value, o.GetAdjustedValue(q.Value));
                            return o.Exists(o.GetAdjustedValue(q.Value)) ? "Found" : "Not found";
                        case COMMAND_SUM:
                            //Console.WriteLine("{0} [{1}:{2}] => [{3}:{4}]", q.Action, q.Value,q.RangeValue, o.GetAdjustedValue(q.Value), o.GetAdjustedValue(q.RangeValue));
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

        public class SplayTree
    {

        public static bool Exists(long key, BinarySearchTreeNode root)
        {
            if (root == null) return false;
            return Find(key, root).Key == key;
        }

        public static BinarySearchTreeNode FindRoot(BinarySearchTreeNode node)
        {
            if (node == null) return null;
            while (node.Parent != null)
                node = node.Parent;
            return node;
        }

        public static BinarySearchTreeNode Find(long key, BinarySearchTreeNode root)
        {
            var found = BinarySearchTree.Find(key, root);
            Splay(found);
            return found;
        }

        public static void Insert(long key, BinarySearchTreeNode root)
        {
            BinarySearchTree.Insert(key, root);
            Find(key, root);  //causes splay
        }

        public static BinarySearchTreeNode Delete(long key, BinarySearchTreeNode root)
        {
            if (!Exists(key, root)) return root;

            Splay(BinarySearchTree.Next(root));
            Splay(root);
            return BinarySearchTree.Delete(root);
        }

        public static long Sum(long leftKey, long rightKey, BinarySearchTreeNode root)
        {
            if (root == null) return 0;

            long results = 0;
            var node = Find(leftKey, root);
            while (node != null && node.Key <= rightKey)
            {
                if (node.Key >= leftKey)
                    results += node.Key;

                node = BinarySearchTree.Next(node);
            }
            return results;
        }

        public static Tuple<BinarySearchTreeNode, BinarySearchTreeNode> Split(long key, BinarySearchTreeNode root)
        {
            var node = Find(key, root);
            return new Tuple<BinarySearchTreeNode, BinarySearchTreeNode>(node.Left,node.Right);
        }

        private static BinarySearchTreeNode Merge(BinarySearchTreeNode one, BinarySearchTreeNode two)
        {
            var node = Find(long.MaxValue, one);
            node.Right = two;
            return node;
        }

        public static void Splay(BinarySearchTreeNode root)
        {
            if (root == null) return;  //bad
            var parentSide = Side(root);
            if (parentSide == 0) return; //bad
            var grandParentSide = Side(root.Parent);
            
            if (grandParentSide == 0)
            { //Zig
                //parent => opposite child
                Swap(root, root.Parent, parentSide);
            }
            else if (parentSide == grandParentSide)
            {//Zig Zig
                //gparent => opposite gchild
                Swap(root.Parent, root.Parent.Parent, grandParentSide);
                //parent => opposite child
                Swap(root, root.Parent, parentSide);
            }
            else
            {//Zig Zag
                //parent => opposite child
                Swap(root, root.Parent, parentSide);
                //gparent => opposite child
                Swap(root, root.Parent, grandParentSide);
            }
            
            if (root.Parent != null)
                Splay(root.Parent);
        }
        
        private static void Swap(BinarySearchTreeNode node, BinarySearchTreeNode parent, int parentSide)
        {
            if (parentSide == NOMATCH) return;

            node.Parent = parent.Parent;
            if (parentSide == LEFT)
            {
                parent.Left = node.Right;
                node.Right = parent;
            }
            else
            {
                parent.Right = node.Left;
                node.Left = parent;
            }
            parent.Parent = node;
        }

        const int NOMATCH = 0;
        const int LEFT = -1;
        const int RIGHT = 1;
        private static int Side(BinarySearchTreeNode child)
        {
            if (child == null || child.Parent == null) return NOMATCH;

            return (child.Parent.Right == child) ? RIGHT
                : (child.Parent.Left == child) ? LEFT
                : NOMATCH;
        }
    }


    public class BinarySearchTreeNode
    {
        public long Key { get; set; }
        public BinarySearchTreeNode Parent { get; set; }
        public BinarySearchTreeNode Left { get; set; }
        public BinarySearchTreeNode Right { get; set; }
        public int Rank { get; set; }
    }
    public class BinarySearchTree
    {
        public static BinarySearchTreeNode Find(long key, BinarySearchTreeNode root)
        {
            if (root.Key == key) return root;
            var child = (root.Key > key) ? root.Left : root.Right;
            return (child == null) ? root : Find(key, child);
        }

        public static IEnumerable<BinarySearchTreeNode> RangeSearch(long leftKey, long rightKey, BinarySearchTreeNode root)
        {
            var results = new List<BinarySearchTreeNode>();
            var node = Find(leftKey, root);
            while (node != null && node.Key <= rightKey)
            {
                if (node.Key >= leftKey)
                    results.Add(node);

                node = Next(node);
            }
            return results;
        }

        public static BinarySearchTreeNode Next(BinarySearchTreeNode node)
        {
            return (node.Right != null) ? LeftDescendant(node.Right) : RightAncestor(node);
        }

        public static void Insert(long key, BinarySearchTreeNode root)
        {
            var parent = Find(key, root);
            if (parent.Key == key) return;

            //Add as child
            var node = new BinarySearchTreeNode { Key = key, Parent = parent };
            if (parent.Key > key) parent.Left = node;
            else parent.Right = node;
        }

        public static BinarySearchTreeNode Delete(BinarySearchTreeNode node)
        {
            if (node.Right == null)
            {
                return Replace(node, node.Left);
            }
            else
            {
                var next = Next(node);
                node.Key = next.Key;
                return Replace(next, next.Right);
            }
        }

        private static BinarySearchTreeNode Replace(BinarySearchTreeNode node, BinarySearchTreeNode with)
        {
            if (with == null) return null;
            with.Parent = node.Parent;
            if (node.Parent == null) return with;

            if (node.Parent.Left == node) node.Parent.Left = with;
            else node.Parent.Right = with;

            return with;
        }
        
        private static BinarySearchTreeNode LeftDescendant(BinarySearchTreeNode node)
        {
            return (node.Left == null) ? node : LeftDescendant(node.Left);
        }

        private static BinarySearchTreeNode RightAncestor(BinarySearchTreeNode node)
        {
            if (node == null || node.Parent == null) return null;

            return (node.Key < node.Parent.Key) ? node.Parent : RightAncestor(node.Parent);
        }
    }
}

