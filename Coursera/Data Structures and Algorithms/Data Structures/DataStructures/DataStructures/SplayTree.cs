using System;

namespace DataStructures
{
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
            return (node.Key == key) ? Delete(node, root) : node;
        }

        private static BinarySearchTreeNode Delete(BinarySearchTreeNode node, BinarySearchTreeNode root)
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



        private static Tuple<BinarySearchTreeNode, BinarySearchTreeNode> Split(long key, BinarySearchTreeNode root)
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

        public static void Splay(BinarySearchTreeNode node)
        {
            if (node == null || node.Parent == null) return; //bad

            var nodesSideOfParent = Side(node.Parent, node);
            if (node.Parent.Parent == null)
            {
                Zig(node, nodesSideOfParent);
            }
            else if (nodesSideOfParent == Side(node.Parent.Parent, node.Parent))
            {
                ZigZig(node, nodesSideOfParent);
            }
            else
            {
                ZigZag(node, nodesSideOfParent);
            }

            if (node.Parent != null)
                Splay(node);
        }

        private static void Zig(BinarySearchTreeNode node, int side)
        {
            var p = node.Parent;
            ReplaceChild(p.Parent, p, node);
            
            if (side == LEFT)
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

        private static void ZigZig(BinarySearchTreeNode node, int side)
        {
            var p = node.Parent;
            var q = node.Parent.Parent;
            
            ReplaceChild(q.Parent, q, node);
            if (side == LEFT)
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

        private static void ZigZag(BinarySearchTreeNode node, int side)
        {
            var p = node.Parent;
            var q = node.Parent.Parent;

            node.Parent = q.Parent;
            ReplaceChild(q.Parent, q, node);
            p.Parent = node;
            q.Parent = node;

            var nl = node.Left;
            var nr = node.Right;
            if (side == LEFT)
            {
                node.Left = q;
                node.Right = p;
                p.Left = nr;
                q.Right= nl;
            }
            else
            {
                node.Left = p;
                node.Right = q;
                p.Right = nl;
                q.Left = nr;
            }
        }

        const int NOMATCH = 0;
        const int LEFT = -1;
        const int RIGHT = 1;
        private static int Side(BinarySearchTreeNode parent, BinarySearchTreeNode child)
        {
            if (child == null || child.Parent == null) return NOMATCH;
            return (parent.Right == child) ? RIGHT
                : (parent.Left == child) ? LEFT
                : NOMATCH;
        }

        private static void ReplaceChild(BinarySearchTreeNode parent, BinarySearchTreeNode currentChild, BinarySearchTreeNode newChild)
        {
            switch (Side(parent,currentChild))
            {
                case LEFT:
                    parent.Left = newChild;
                    break;
                case RIGHT:
                    parent.Right = newChild;
                    break;
                default:
                    newChild.Parent = null;
                    break;
            }
        }
    }
}
