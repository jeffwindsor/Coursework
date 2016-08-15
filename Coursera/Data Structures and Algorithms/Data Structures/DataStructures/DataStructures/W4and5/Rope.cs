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

        private NodePair Split(Node node, int index)
        {

            //Rebalance
            throw new NotImplementedException();
        }

        private Node Concat(Node left,  Node right)
        {
            var result = new Node { Left = left, Right = right,  };

            //Rebalance
            throw new NotImplementedException();
        }

        private static NodePair SplitLeaf(Node node, int index)
        {
            while (true)
            {
                if (node.Weight < index)
                {
                    index = index - node.Weight;
                    node = node.Right;   
                    continue;
                }

                if (node.Left == null)
                {
                    var splitIndex = node.Data.Length - index;
                    if(splitIndex == 0)
                }
                return new NodePair {Left = new Node {Data = }, Right = node};

                node = node.Left;
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
            public int Weight { get { return (Data == null)?0:Data.Length; } }
            public long Size { get; private set; }

            private void UpdateSize()
            {
                Size = (Left == null ? 0 : Left.Size) + (Right == null ? 0 : Right.Size);
            }

            public string Data { get; private set; }

            public Node Parent { get; private set; }
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
            

            public bool IsLeaf { get { return Left == null && Right == null && Data != null && Weight > 0; }}
            public bool IsEmpty { get { return Left == null && Right == null && Data == null; } }

            //public override string ToString()
            //{
            //    return Data;
            //}
        }
    }
}
