using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.W4and5
{
    public class Rope
    {
        private Node _root = new Node();

        public char Search(int i)
        {
            var found = Search(_root,i);
            return found.Item1.SubString[found.Item2];
        }
        
        private void Insert(int index, Node node)
        {
            var split = Split(_root, index);
            _root = Concat(Concat(split.Left, node), split.Right);
        }

        private void Delete(int startIndex, int endIndex)
        {
            var mr = Split(_root, endIndex);
            var lm = Split(mr.Left, startIndex);
            _root = Concat(lm.Left, mr.Right);
        }

        private static NodePair Split(Node node, int i)
        {
            var result = Search(node, i);
            var found = result.Item1;
            var foundIndex = result.Item2;

            var foundParent = found.Parent;
            var split = SplitLeaf(found, foundIndex);



            //Rebalance
            throw new NotImplementedException();
        }

        private static Node SplitLeaf(Node found, int i)
        {
            if (found.SubString.Length != i)
            {
                //Mid case
                found.SubString = found.SubString.Substring(0, i);
                found.UpdateSize();

                return new Node
                {
                    Right = found.Right,
                    SubString = found.SubString.Substring(i - 1, found.SubString.Length - i)
                };
            }
            else
            {
                //End Case
                found.Parent.Right = new Node {Parent = found.Parent};
                found.Parent = new Node();
                return found;
            }
        }

        private static Node Concat(Node left,  Node right)
        {
            return new Node { Left = left, Right = right };
        }

        private static Tuple<Node,int> Search(Node node, int i)
        {
            while (true)
            {
                if (node.Weight < i)
                {
                    i = i - node.Weight;
                    node = node.Right;   
                    continue;
                }

                if (node.Left == null)
                {
                    node = node.Left;
                    continue;
                }
                return new Tuple<Node, int>(node, i);
            }
        }


        private void Process(Input input)
        {
            throw new NotImplementedException();
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
            var xs = Enumerable.Range(1, n)
                .Select(i =>
                {
                    var items = inputs[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    return new Input
                    {
                        StartIndex = int.Parse(items[0]),
                        EndIndex = int.Parse(items[1]),
                        InsertIndex = int.Parse(items[2])
                    };
                });

            var rope = new Rope(s);
            foreach (var x in xs)
            {
                rope.Process(x);
            }
            return new [] { rope.ToString()};
        }

        private class Input
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public int InsertIndex { get; set; }
        }
        #endregion

        private class NodePair
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        private class Node
        {   
            public static Node Empty = new Node();
            public int Weight { get { return (!IsLeaf) ? 0 : SubString.Length; } }
            public long Size { get; private set; }

            public void UpdateSize()
            {
                Size = 
                    (IsEmpty) ? 0 : 
                    (IsLeaf) ? Weight :
                    (Left == null ? 0 : Left.Size) + (Right == null ? 0 : Right.Size);
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
            

            public bool IsLeaf { get { return Left == null && Right == null && SubString != null && Weight > 0; }}
            public bool IsEmpty { get { return Left == null && Right == null && SubString == null; } }

            //public override string ToString()
            //{
            //    return Data;
            //}
        }
    }
}
