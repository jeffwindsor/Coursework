using System;
using System.Runtime.InteropServices.ComTypes;

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

        public static void Delete(long key, BinarySearchTreeNode node)
        {
            Splay(BinarySearchTree.Next(node));
            Splay(node);
            BinarySearchTree.Delete(node);
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
                Splay(root);
        }

        private static void Zig(BinarySearchTreeNode root, int rotation)
        {
            Swap(root, root.Parent, rotation);
        }

        private static void Swap(BinarySearchTreeNode node, BinarySearchTreeNode parent, int rotation)
        {
            if (rotation == NOMATCH) return;

            node.Parent = parent.Parent;
            if (rotation == LEFT)
                node.Right = parent;
            else
                node.Left = parent;

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
