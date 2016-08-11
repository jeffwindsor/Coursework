using System;
using System.Runtime.InteropServices.ComTypes;

namespace DataStructures
{
    public class SplayTree
    {
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

        public static BinarySearchTreeNode Delete(long key, BinarySearchTreeNode node)
        {
            Splay(BinarySearchTree.Next(node));
            Splay(node);
            return BinarySearchTree.Delete(node);
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
}
