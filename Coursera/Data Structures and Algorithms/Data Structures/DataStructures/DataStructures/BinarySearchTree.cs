using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class BinarySearchTree<TKey>
        where TKey : IComparable<TKey>
    {
        public class Node
        {
            public TKey Key { get; set; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public void Insert(TKey key)
            {
                var parent = Find(key);
                if (parent.Key.CompareTo(key) > 0)
                    parent.Left = new Node { Key = key, Parent = parent };
                else if (parent.Key.CompareTo(key) < 0)
                    parent.Right = new Node { Key = key, Parent = parent };
            }

            public void Delete()
            {
                if (Right == null) Replace(this, Left);
                else
                {
                    var next = Next();
                    Key = next.Key;
                    Replace(next, next.Right);
                }
            }

            private static void Replace(Node source, Node target)
            {
                if (source.Parent == null) return;
                if (source.Parent.Left == source) source.Parent.Left = target;
                else source.Parent.Right = target;
            }

            public Node Find(TKey key)
            {
                if (Key.CompareTo(key) == 0) return this;
                var child = (Key.CompareTo(key) > 0) ? Left : Right;
                return (child == null) ? this : child.Find(key);
            }

            public Node Next()
            {
                return (Right == null) ? RightAncestor(this) : LeftDescendant(Right);
            }

            public IEnumerable<Node> RangeSearch(TKey leftKey, TKey rightKey, Node root)
            {
                var result = new List<Node>();
                var leftNode = Find(leftKey);
                while (leftNode.Key.CompareTo(rightKey) <= 0)
                {
                    if (leftNode.Key.CompareTo(leftKey) >= 0) result.Add(leftNode);
                    leftNode = leftNode.Next();
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
        }
    }
}
