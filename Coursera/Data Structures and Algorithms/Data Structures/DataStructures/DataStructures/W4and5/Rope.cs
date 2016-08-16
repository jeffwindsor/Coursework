using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.W4and5
{
    public class Rope
    {
        private Node _root;

        public Rope(string s)
        {
            _root = Node.CreateLeaf(s);
        }

        private char Search(int i)
        {
            var r = Search(_root,i);
            return r.Node.SubString[r.StringIndex];
        }

        private void Move(int startIndex, int endIndex, int insertIndex)
        {
            var removed = Remove(startIndex, endIndex);
            Add(insertIndex, removed);
        }

        private void Add(int index, Node node)
        {
            var split = Split(_root, index);
            _root = Node.CreateConcatenation(Node.CreateConcatenation(split.Left, node), split.Right);
        }

        //Removes Section and Returns it
        private Node Remove(int startIndex, int endIndex)
        {
            var mr = Split(_root, endIndex);
            var lm = Split(mr.Left, startIndex);
            _root = Node.CreateConcatenation(lm.Left, mr.Right);

            return mr.Left;
        }

        internal static NodePair Split(Node node, int i)
        {
            //Find leaf at index
            var result = Search(node, i);
            var leaf = result.Node;
            var parent = leaf.Parent;

            //Split found leaf in Node
            var splitLeaf = SplitLeaf(leaf, result.StringIndex);
            Replace(leaf, splitLeaf.Left);
            
            //Transfer all right sides to split
            var right = splitLeaf.Right; 
            while (SideOfParent(parent) == Side.LEFT)
            {
                parent = parent.Parent;
                if (parent == null || parent.Right == null)
                    continue;

                var right1 = parent.Right;
                Replace(parent.Right, null);
                right = Node.CreateConcatenation(right, right1);
            }

            //Rebalance
            var left = splitLeaf.Left == null || splitLeaf.Left.IsRoot ? splitLeaf.Left : node;
            return new NodePair(left, right);
        }

        internal static NodePair SplitLeaf(Node leaf, int stringIndex)
        {
            if (!leaf.IsLeaf)
                throw new ArgumentException("leaf");

            if (stringIndex == 0)
                return new NodePair(null, Node.CreateLeaf(leaf.SubString));
            if (stringIndex == leaf.SubString.Length - 1)
                return new NodePair(Node.CreateLeaf(leaf.SubString), null);
            //Middle
            var i = stringIndex;
            return new NodePair(
                Node.CreateLeaf(leaf.SubString.Substring(0, i)),
                Node.CreateLeaf(leaf.SubString.Substring(i, leaf.SubString.Length - i))
                );
        }

        internal static void Replace(Node current, Node replacement)
        {
            switch (SideOfParent(current))
            {
                case Side.LEFT:
                    current.Parent.Left = replacement;
                    break;
                case Side.RIGHT:
                    current.Parent.Right = replacement;
                    break;
            }
        }

        internal static IndexSearch Search(Node node, int i)
        {
            return (node.Weight < i)
                ? Search(node.Right, i - node.Weight)
                : node.Left != null 
                    ? Search(node.Left, i) 
                    : new IndexSearch {Node = node, StringIndex = i};
        }

        internal enum Side
        {
            NO = 0,
            LEFT = -1,
            RIGHT = 1,
        }

        internal static Side SideOfParent(Node child)
        {
            if (child == null || child.Parent == null)
                return Side.NO;

            return (child.Parent.Right == child) ? Side.RIGHT
                : (child.Parent.Left == child) ? Side.LEFT
                : Side.NO;
        }

        internal static void InOrderTraversal(Node node, StringBuilder results)
        {
            if (node == null) return;
            InOrderTraversal(node.Left, results);
            results.Append(node.SubString);
            InOrderTraversal(node.Right, results);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            InOrderTraversal(_root,sb);
            return sb.ToString();
        }

        public string Print()
        {
            return new NodePrinter(_root).Print();
        }

        internal class Node
        {
            public static Node CreateLeaf(string s)
            {
                var result = new Node
                {
                    SubString = s
                };
                result.UpdateSize();
                return result;
            }

            public static Node CreateConcatenation(Node left, Node right)
            {
                var result = new Node();
                result.Left = left;
                result.Right = right;
                result.UpdateSize();
                return result;
            }

            private Node(){}

            public int Weight { get { return SubString == null ? 0 : SubString.Length; } }

            public long Size { get; private set; }

            public void UpdateSize()
            {
                Size = (IsLeaf) ? Weight : (Left==null?0:Left.Size) + (Right==null?0:Right.Size);
            }

            public string SubString { get; set; }

            public Node Parent { get; set; }
            private Node _left;
            public Node Left
            {
                get { return _left; }
                set
                {
                    _left = value;
                    if (value == null) return;

                    _left.Parent = this;
                    UpdateSize();
                }
            }

            private Node _right;
            public Node Right
            {
                get { return _right; }
                set
                {
                    _right = value;
                    if (value == null) return;

                    _right.Parent = this;
                    UpdateSize();
                }
            }
            
            public bool IsLeaf { get { return Left == null && Right == null && Weight > 0; } }
            public bool IsRoot { get { return Parent == null; } }

            public override string ToString()
            {
                return string.Format("[{0}]",Weight) + (SubString==null ? "": SubString);
            }

            public string Print()
            {
                return new NodePrinter(this).Print();
            }
        }
        
        #region Main
        //public static void Main(string[] args)
        //{
        //    string s;
        //    var inputs = new List<string>();
        //    while ((s = Console.ReadLine()) != null)
        //        inputs.Add(s);

        //    foreach (var result in Answer(inputs.ToArray()))
        //        Console.WriteLine(result);
        //}

        public static IList<string> Answer(IList<string> inputs)
        {
            var s = inputs[0];
            var n = int.Parse(inputs[1]);
            var xs = Enumerable.Range(2, n)
                .Select(i =>
                {
                    var items = inputs[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    return new
                    {
                        StartIndex = int.Parse(items[0]) - 1,
                        EndIndex = int.Parse(items[1]) - 1,
                        InsertIndex = int.Parse(items[2]) - 1
                    };
                });

            var rope = new Rope(s);
            foreach (var x in xs)
            {
                rope.Move(x.StartIndex, x.EndIndex, x.InsertIndex);
                Console.WriteLine(rope);
                Console.WriteLine(rope.Print());
            }
            return new [] { rope.ToString()};
        }
        #endregion

        internal class NodePair
        {
            public NodePair(Node left, Node right)
            {
                Left = left;
                Right = right;
            }

            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        internal class IndexSearch
        {
            public Node Node { get; set; }
            public int StringIndex { get; set; }
        }

        private class NodePrinter
        {
            private readonly Node _original;
            private readonly StringBuilder _sb;

            public NodePrinter(Node node)
            {
                _original = node;
                _sb = new StringBuilder();
            }

            public string Print()
            {
                if (_original == null)
                {
                    PrintNodeValue(_original);
                }
                else
                {
                    Print(GetRoot(_original));
                }
                return _sb.ToString();
            }

            private void Print(Node node)
            {

                if (node != null && node.Right != null)
                {
                    Print(node.Right, true, "");
                }
                PrintNodeValue(node);
                if (node != null && node.Left != null)
                {
                    Print(node.Left, false, "");
                }
            }

            private void PrintNodeValue(Node node)
            {
                _sb.AppendLine(node == null ? "<null>" : node.ToString());
            }

            // use string and not stringbuffer on purpose as we need to change the indent at each recursion
            private void Print(Node node, bool isRight, string indent)
            {
                if (node.Right != null)
                {
                    Print(node.Right, true, indent + (isRight ? "        " : " |      "));
                }
                _sb.Append(indent);
                _sb.Append(isRight ? " /" : " \\");
                _sb.Append("----- ");

                PrintNodeValue(node);
                if (node.Left != null)
                {
                    Print(node.Left, false, indent + (isRight ? " |      " : "        "));
                }
            }

            private static Node GetRoot(Node node)
            {
                if (node == null) return null;

                while (node.Parent != null)
                    node = node.Parent;
                return node;
            }
        }
    }
}
