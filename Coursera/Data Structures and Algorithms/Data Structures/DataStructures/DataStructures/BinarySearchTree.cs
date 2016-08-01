using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinarySearchTree<TKey>
        where TKey : IComparable<TKey>
    {

        public void Insert(TKey key, Node root)
        {
            var parent = Find(key, root);
            if (parent.Key.CompareTo(key) > 0)
                parent.Left = new Node {Key = key, Parent = parent};
            else if(parent.Key.CompareTo(key) < 0)
                parent.Right = new Node {Key = key, Parent = parent};
        }

        public void Delete(Node node)
        {
            if (node.Right == null) Replace(node, node.Left);
            else
            {
                var next = Next(node);
                node.Key = next.Key;
                Replace(next, next.Right);
            }
        }

        private static void Replace(Node source, Node target)
        {
            if (source.Parent == null) return;
            if (source.Parent.Left == source) source.Parent.Left = target;
            else source.Parent.Right = target;
        }

        public Node Find(TKey key, Node root)
        {
            if(root.Key.CompareTo(key) == 0) return root;
            var child = (root.Key.CompareTo(key) > 0) ? root.Left : root.Right;
            return (child == null) ? root : Find(key, child);
        }

        public Node Next(Node root)
        {
            return (root.Right == null) ? RightAncestor(root) : LeftDescendant(root.Right);
        }

        public IEnumerable<Node> RangeSearch(TKey leftKey, TKey rightKey, Node root)
        {
            var result = new List<Node>();
            var leftNode = Find(leftKey, root);
            while(leftNode.Key.CompareTo(rightKey) <= 0)
            {
                if(leftNode.Key.CompareTo(leftKey) >= 0) result.Add(leftNode);
                leftNode = Next(leftNode);
            }
            return result;
        }

        private static Node LeftDescendant(Node node)
        {
            return (node.Left == null) ? node : LeftDescendant(node.Left);
        }

        private static Node RightAncestor(Node node)
        {
            return (node == null)
                ? null
                : (node.Key.CompareTo(node.Parent.Key) < 0) ? node.Parent : RightAncestor(node.Parent);
        }

        public class Node
        {
            public TKey Key { get; set; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
    }
}
