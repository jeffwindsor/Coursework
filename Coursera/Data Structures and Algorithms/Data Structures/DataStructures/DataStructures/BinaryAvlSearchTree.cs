using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class BinaryAvlSearchTree  //<long> where long : IComparable<long>
    {
        public class Node
        {
            public long Key { get; set; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
            
            public void Insert(long key)
            {
                InsertInternal(key).Rebalance();
            }

            public void Delete()
            {
                DeleteInternal().Rebalance();
            }

            public Tuple<Node,Node> Split(long x)
            {
                if (x < Right.Key)
                {
                    var s = Left.Split(x);
                    var three = MergeAsRootAvl(s.Item2, Right);
                    return new Tuple<Node, Node>(s.Item1, three);
                }
                else
                {
                    var s = Right.Split(x);
                    var three = MergeAsRootAvl(s.Item2, Left);
                    return new Tuple<Node, Node>(s.Item1, three);
                }
            }

            private Node Merge(Node with)
            {
                var max = Find(long.MaxValue);
                max.Delete();
                max.MergeAsRootAvl(this,with);
                return max;
            }

            private Node MergeAsRootAvl(Node left, Node right)  //AVLTreeMergeWithRoot
            {
                if (Math.Abs(left.Height - right.Height) <= 1)
                {
                    MergeAsRoot(left, right);
                    AdjustHeight();
                    //return this;
                }
                else if (left.Height > right.Height)
                {
                    var rightPrime = MergeAsRootAvl(left.Right, right);
                    left.Right = rightPrime;
                    rightPrime.Parent = left;
                    left.Rebalance();
                    //return this;
                }
                else if (left.Height < right.Height)
                {
                    var leftPrime = MergeAsRootAvl(left, right.Right);
                    right.Right = leftPrime;
                    leftPrime.Parent = right;
                    right.Rebalance();
                    //return this;
                }
                return this;
            }

            private void MergeAsRoot(Node left, Node right)
            {
                Left = left;
                Right = right;
                left.Parent = this;
                right.Parent = this;
            }

            private void Rebalance()
            {
                if(Left.Height > Right.Height + 1)
                    RebalanceRight();
                if (Right.Height > Left.Height + 1)
                    RebalanceLeft();

                AdjustHeight();

                if(Parent != null)
                    Parent.Rebalance();
            }

            private void RebalanceLeft()
            {
                if (Left.Left.Height > Left.Right.Height)
                    Left.RotateRight();
                RotateLeft();
            }

            private void RebalanceRight()
            {
                if (Left.Right.Height > Left.Left.Height)
                    Left.RotateLeft();
                RotateRight();
            }

            private void RotateRight()
            {
                var currentParent = Parent;
                var currentGrandParent = currentParent.Parent;
                var currentRight = Right;
                
                //Rotate
                Parent = currentGrandParent;
                Right = currentParent;
                currentParent.Parent = this;
                currentParent.Left = currentRight;
                
                //Adjust Heights - Bottom to Top
                Right.AdjustHeight();
                AdjustHeight();
                Parent.AdjustHeight();
            }

            private void RotateLeft()
            {
                var currentParent = Parent;
                var currentGrandParent = currentParent.Parent;
                var currentLeft = Left;

                //Rotate
                Parent = currentGrandParent;
                Left = currentParent;
                currentParent.Parent = this;
                currentParent.Right = currentLeft;

                //Adjust Heights - Bottom to Top
                Left.AdjustHeight();
                AdjustHeight();
                Parent.AdjustHeight();
            }

            private void AdjustHeight()
            {
                Height = 1 + Math.Max(Left.Height, Right.Height);
            }
            
            private Node InsertInternal(long key)
            {
                var parent = Find(key);
                if (parent.Key == key)
                    return parent;

                var node = new Node {Key = key, Parent = parent};
                if (parent.Key > key) parent.Left = node;
                else parent.Right = node;

                return node;
            }

            private Node DeleteInternal()
            {
                if (Right == null)
                {
                    Replace(Left);
                    return Left;
                }
                else
                {
                    var next = Next();
                    Key = next.Key;
                    next.Replace(next.Right);
                    return next.Right;
                }
            }

            private void Replace(Node with)
            {
                if (Parent == null) return;
                if (Parent.Left == this) Parent.Left = with;
                else Parent.Right = with;
            }

            private Node Find(long key)
            {
                if (Key == key) return this;
                var child = (Key > key) ? Left : Right;
                return (child == null) ? this : child.Find(key);
            }

            private Node Next()
            {
                return (Right == null) ? RightAncestor(this) : LeftDescendant(Right);
            }

            private IEnumerable<Node> RangeSearch(long leftKey, long rightKey, Node root)
            {
                var result = new List<Node>();
                var leftNode = Find(leftKey);
                while (leftNode.Key <= rightKey)
                {
                    if (leftNode.Key >= leftKey) result.Add(leftNode);
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
                    : (node.Key < node.Parent.Key) ? node.Parent : RightAncestor(node.Parent);
            }
        }
    }
}
