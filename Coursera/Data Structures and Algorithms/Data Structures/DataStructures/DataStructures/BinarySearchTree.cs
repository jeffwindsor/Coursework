using System.Collections.Generic;

namespace DataStructures
{
    public class BinarySearchTreeNode
    {
        public long Key { get; set; }
        public BinarySearchTreeNode Parent { get; set; }
        public BinarySearchTreeNode Left { get; set; }
        public BinarySearchTreeNode Right { get; set; }
        public int Rank { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Key); //string.Format("{0}:{1}", Key, Rank);
        }
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

        public static long Sum(long leftKey, long rightKey, BinarySearchTreeNode root)
        {
            if (root == null) return 0;

            long results = 0;
            var node = Find(leftKey, root);
            while (node != null && node.Key <= rightKey)
            {
                if (node.Key >= leftKey)
                    results += node.Key;

                node = Next(node);
            }
            return results;
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
