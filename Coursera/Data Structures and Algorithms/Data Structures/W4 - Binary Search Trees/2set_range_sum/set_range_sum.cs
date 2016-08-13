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

        private const string CommandAdd = "+";
        private const string CommandDel = "-";
        private const string CommandFind = "?";
        private const string CommandSum = "s";
        private const long M = 1000000001;

        public static IList<string> Answer(IList<string> inputs)
        {
            var n = int.Parse(inputs[0]);
            var queries = Enumerable.Range(1, n)
                .Select(i =>
                {
                    var items = inputs[i].Split(' ');
                    return new Query
                    {
                        Action = items[0],
                        Value = long.Parse(items[1]),
                        RangeValue = (items.Length == 3) ? long.Parse(items[2]) : 0
                    };
                });


            //var outputLine = 0;
            var o = new SetRangeSum();
            var results = queries
                .Select((q, i) =>
                {
                    /*
                    Console.WriteLine("Current Sum: {0}", o._lastSum);
                    Console.WriteLine((new BinarySearchTreeNodePrinter(o.Tree)).Print());

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
                    */

                    switch (q.Action)
                    {
                        case CommandAdd:
                            o.Insert(o.GetAdjustedValue(q.Value));
                            return string.Empty;
                        case CommandDel:
                            o.Delete(o.GetAdjustedValue(q.Value));
                            return string.Empty;
                        case CommandFind:
                            return o.Exists(o.GetAdjustedValue(q.Value)) ? "Found" : "Not found";
                        case CommandSum:
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
            return (_lastSum + source) % M;
        }

        public void Insert(long key)
        {
            Tree = (Tree == null)
                ? new BinarySearchTreeNode { Key = key }
                : SplayTree.Insert(key, Tree);
        }

        public void Delete(long key)
        {
            if (Tree == null)
                return;
            Tree = GetRoot(SplayTree.Delete(key, Tree));
        }

        public bool Exists(long key)
        {
            if (Tree == null)
                return false;

            Tree = SplayTree.Find(key, Tree);
            return Tree.Key == key;
        }

        public long Sum(long leftKey, long rightKey)
        {
            if (Tree == null)
                return 0;

            Tree = SplayTree.Find(leftKey, Tree);
            _lastSum = Sum(leftKey, rightKey, Tree);
            return _lastSum;
        }

        public class Query
        {
            public string Action { get; set; }
            public long Value { get; set; }
            public long RangeValue { get; set; }
        }


        private long _lastSum;
        private BinarySearchTreeNode Tree { get; set; }
        private static long Sum(long leftKey, long rightKey, BinarySearchTreeNode node)
        {
            long results = 0;
            while (node != null && node.Key <= rightKey)
            {
                if (node.Key >= leftKey)
                    results += node.Key;

                node = BinarySearchTree.Next(node);
            }
            return results;
        }

        //private BinarySearchTreeNode _tree;
        //private BinarySearchTreeNode Tree
        //{
        //    get { return _tree; }
        //    set { _tree = GetRoot(value); }
        //}
        //private void ReRootTree() { Tree = Tree; }
        private static BinarySearchTreeNode GetRoot(BinarySearchTreeNode node)
        {
            if (node == null)
                return null;

            while (node.Parent != null)
                node = node.Parent;
            return node;
        }
    }

    public class SplayTree
    {
        public static BinarySearchTreeNode Find(long key, BinarySearchTreeNode root)
        {
            var found = BinarySearchTree.Find(key, root);
            Splay(found);
            return found;
        }

        public static BinarySearchTreeNode Insert(long key, BinarySearchTreeNode root)
        {
            BinarySearchTree.Insert(key, root);
            return Find(key, root);
        }

        public static BinarySearchTreeNode Delete(long key, BinarySearchTreeNode root)
        {
            var node = Find(key, root);
            return (node.Key == key) ? Delete(node) : node;
        }

        private static BinarySearchTreeNode Delete(BinarySearchTreeNode node)
        {
            Splay(BinarySearchTree.Next(node));
            Splay(node);
            return BinarySearchTree.Delete(node);
        }

        public static long Sum(long leftKey, long rightKey, BinarySearchTreeNode root)
        {
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

        public static void Splay(BinarySearchTreeNode node)
        {
            //Recursion Replaced with iteration
            //      if (node.Parent != null)
            //          Splay(node);
            //
            while (true)
            {
                if (node == null || node.Parent == null)
                    return; //bad

                var nodesSideOfParent = BinarySearchTreeNode.SideOf(node.Parent, node);
                if (node.Parent.Parent == null)
                {
                    Zig(node, nodesSideOfParent);
                }
                else if (nodesSideOfParent == BinarySearchTreeNode.SideOf(node.Parent.Parent, node.Parent))
                {
                    ZigZig(node, nodesSideOfParent);
                }
                else
                {
                    ZigZag(node, nodesSideOfParent);
                }

                if (node.Parent != null)
                    continue;
                break;
            }
        }

        private static void Zig(BinarySearchTreeNode node, BinarySearchTreeNode.SideOfParent side)
        {
            var p = node.Parent;
            BinarySearchTreeNode.ReplaceChild(p.Parent, p, node);

            if (side == BinarySearchTreeNode.SideOfParent.LEFT)
            {
                var nr = node.Right;
                node.Right = p;
                p.Left = nr;
            }
            else
            {
                var nl = node.Left;
                node.Left = p;
                p.Right = nl;
            }
        }

        private static void ZigZig(BinarySearchTreeNode node, BinarySearchTreeNode.SideOfParent side)
        {
            var p = node.Parent;
            var q = node.Parent.Parent;

            BinarySearchTreeNode.ReplaceChild(q.Parent, q, node);
            if (side == BinarySearchTreeNode.SideOfParent.LEFT)
            {
                var nr = node.Right;
                var pr = p.Right;
                node.Right = p;
                p.Right = q;
                p.Left = nr;
                q.Left = pr;
            }
            else
            {
                var nl = node.Left;
                var pl = p.Left;
                node.Left = p;
                p.Left = q;
                p.Right = nl;
                q.Right = pl;
            }
        }

        private static void ZigZag(BinarySearchTreeNode node, BinarySearchTreeNode.SideOfParent side)
        {
            var p = node.Parent;
            var q = node.Parent.Parent;

            BinarySearchTreeNode.ReplaceChild(q.Parent, q, node);
            p.Parent = node;
            q.Parent = node;

            var nl = node.Left;
            var nr = node.Right;
            if (side == BinarySearchTreeNode.SideOfParent.LEFT)
            {
                node.Left = q;
                node.Right = p;
                p.Left = nr;
                q.Right = nl;
            }
            else
            {
                node.Left = p;
                node.Right = q;
                p.Right = nl;
                q.Left = nr;
            }
        }

    }

    public class BinarySearchTree
    {
        public static BinarySearchTreeNode Find(long key, BinarySearchTreeNode root)
        {
            //Recursion Replaced with iteration
            //return (child == null) ? root : Find(key, child);
            while (true)
            {
                if (root.Key == key)
                    return root;
                var child = (root.Key > key) ? root.Left : root.Right;
                if ((child == null))
                    return root;
                root = child;
            }
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
            if (parent.Key == key)
                return;

            //Add as child
            var node = new BinarySearchTreeNode { Key = key, Parent = parent };
            if (parent.Key > key)
                parent.Left = node;
            else
                parent.Right = node;
        }

        public static BinarySearchTreeNode Delete(BinarySearchTreeNode node)
        {
            if (node.Right == null)
            {
                var nl = node.Left;
                BinarySearchTreeNode.ReplaceChild(node.Parent, node, nl);
                //node.Orphan();
                return nl;
            }

            var next = Next(node);
            BinarySearchTreeNode.ReplaceChild(next.Parent, next, next.Right);
            BinarySearchTreeNode.ReplaceChild(node.Parent, node, next);
            next.Left = node.Left;
            next.Right = node.Right;
            //node.Orphan();
            return next;
        }

        private static BinarySearchTreeNode LeftDescendant(BinarySearchTreeNode node)
        {
            //Recursion Replaced with iteration
            //return (node.Left == null) ? node : LeftDescendant(node.Left);
            while (true)
            {
                if ((node.Left == null))
                    return node;
                node = node.Left;
            }
        }

        private static BinarySearchTreeNode RightAncestor(BinarySearchTreeNode node)
        {
            //Recursion Replaced with iteration
            //return (node.Key < node.Parent.Key) ? node.Parent : RightAncestor(node.Parent);
            while (true)
            {
                if (node == null || node.Parent == null)
                    return null;

                if ((node.Key < node.Parent.Key))
                    return node.Parent;
                node = node.Parent;
            }
        }
    }

    public class BinarySearchTreeNode
    {
        private BinarySearchTreeNode _left;
        private BinarySearchTreeNode _right;

        public long Key { get; set; }
        public BinarySearchTreeNode Parent { get; set; }

        public BinarySearchTreeNode Left
        {
            get { return _left; }
            set
            {
                _left = value;
                if (value != null)
                    value.Parent = this;
            }
        }
        public BinarySearchTreeNode Right
        {
            get { return _right; }
            set
            {
                _right = value;
                if (value != null)
                    value.Parent = this;
            }
        }

        public int Rank { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Key); //string.Format("{0}:{1}", Key, Rank);
        }

        public enum SideOfParent
        {
            NO = 0,
            LEFT = -1,
            RIGHT = 1,
        }

        public static SideOfParent SideOf(BinarySearchTreeNode parent, BinarySearchTreeNode child)
        {
            if (child == null || child.Parent == null)
                return SideOfParent.NO;

            return (parent.Right == child) ? SideOfParent.RIGHT
                : (parent.Left == child) ? SideOfParent.LEFT
                : SideOfParent.NO;
        }

        public static void ReplaceChild(BinarySearchTreeNode parent, BinarySearchTreeNode currentChild, BinarySearchTreeNode newChild)
        {
            switch (SideOf(parent, currentChild))
            {
                case SideOfParent.LEFT:
                    parent.Left = newChild;
                    return;
                case SideOfParent.RIGHT:
                    parent.Right = newChild;
                    return;
                default:
                    if (newChild != null)
                        newChild.Parent = null;
                    return;
            }
        }
    }
}

