using System.Collections.Generic;

namespace DataStructures
{
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
                var nl = node.Left;
                BinarySearchTreeNode.ReplaceChild(node.Parent,node, nl);
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
            return (node.Left == null) ? node : LeftDescendant(node.Left);
        }

        private static BinarySearchTreeNode RightAncestor(BinarySearchTreeNode node)
        {
            if (node == null || node.Parent == null) return null;

            return (node.Key < node.Parent.Key) ? node.Parent : RightAncestor(node.Parent);
        }
    }
}
