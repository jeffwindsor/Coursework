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
            var rangeSearch = new List<BinarySearchTreeNode>();
            var node = Find(leftKey, root);
            while (node.Key <= rightKey)
            {
                if (node.Key >= leftKey) rangeSearch.Add(node);
                node = Next(node);
            }
            return rangeSearch;
        }

        public static BinarySearchTreeNode Next(BinarySearchTreeNode node)
        {
            return (node.Right == null) ? RightAncestor(node) : LeftDescendant(node.Right);
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

        public static void Delete(BinarySearchTreeNode node)
        {
            if (node.Right == null)
            {
                Replace(node, node.Left);
            }
            else
            {
                var next = Next(node);
                node.Key = next.Key;
                Replace(next, next.Right);
            }
        }

        private static void Replace(BinarySearchTreeNode node, BinarySearchTreeNode with)
        {
            if (node.Parent == null) return;
            if (node.Parent.Left == node) node.Parent.Left = with;
            else node.Parent.Right = with;
        }
        
        private static BinarySearchTreeNode LeftDescendant(BinarySearchTreeNode node)
        {
            return (node.Left == null) ? node : LeftDescendant(node.Left);
        }

        private static BinarySearchTreeNode RightAncestor(BinarySearchTreeNode node)
        {
            return (node == null)
                ? null
                : (node.Key < node.Parent.Key) ? node.Parent : RightAncestor(node.Parent);
        }
    }
}
