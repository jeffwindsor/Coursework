using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnStrings
{
    public class SuffixTree
    {
        private readonly IReadOnlyList<char> _text;
        private readonly Node _root;
        private readonly List<Node> _nodes;
        public SuffixTree(string text)
        {
            _text = text.ToCharArray();
            _root = new Node(0, 0, true, Enumerable.Empty<Node>());
            _nodes = new List<Node> {_root};
        }
        
        public void Merge(int start, int length)
        {
            var node = new Node(start, length, true, Enumerable.Empty<Node>());
            foreach (var child in _root.Children)
            {
                if (Merge(child, node, _text, _nodes))
                    return;
            }
            _root.Children.Add(node);
            _nodes.Add(node);
        }

        private static bool Merge(Node root, Node node, IReadOnlyList<char> text, ICollection<Node> nodes)
        {
            var i = MatchIndex(root, node, text);
            //NO OVERLAP
            if (i == -1) return false;
            //OVERLAP
            Branch(root, i, node, nodes);

            return true;
        }

        private static void Branch(Node root, int i, Node node, ICollection<Node> nodes)
        {
            var remainderThis = CopyAfter(i, root);
            var remainderThisEmpty = remainderThis.Length <= 0;
            var remainderNode = CopyAfter(i, node);
            var remainderNodeEmpty = remainderNode.Length <= 0;

            root.Length = i + 1;
            root.IsEndNode = remainderThisEmpty || remainderNodeEmpty;

            root.Children.Clear();
            if (!remainderThisEmpty)
            {
                root.Children.Add(remainderThis);
                nodes.Add(remainderThis);
            }
            if (!remainderNodeEmpty)
            {
                root.Children.Add(remainderNode);
                nodes.Add(remainderNode);
            }
        }

        private static Node CopyAfter(int i, Node source)
        {
            var l = i + 1;
            return new Node(source.Start + l, source.Length - l, source.IsEndNode, source.Children);
        }
        
        private static int MatchIndex(Node s1, Node s2, IReadOnlyList<char> text)
        {
            int i;
            for (i = 0; i < Math.Min(s1.Length, s2.Length); i++)
            {
                if (!IsSameChar(s1.Start + i, s2.Start + i, text))
                    break;
            }
            return i - 1;
        }

        private static bool IsSameChar(int i, int j, IReadOnlyList<char> text)
        {
            return text[i] == text[j];
        }

        public IEnumerable<string> ToText()
        {
            return _nodes.Select(ToText);
        }

        private string ToText(Node node)
        {
            var v = new string(_text.Skip(node.Start).Take(node.Length).ToArray());
            return node.IsEndNode ? string.Format("{0}$", v) : v;
        }
        

        public class Builder
        {
            private int _lineCursor;
            private readonly IList<string> _inputs;
            public Builder(IList<string> inputs)
            {
                _inputs = inputs;
            }

            public SuffixTree ToSuffixTree()
            {
                return ToSuffixTree(NextAsString());
            }

            public static SuffixTree ToSuffixTree(string text)
            {
                text = text.Replace("$", "");
                var result = new SuffixTree(text);
                for (var i = 0; i < text.Length; i++)
                {
                    result.Merge(i, text.Length - i);
                }
                return result;
            }

            public int NextAsInt()
            {
                return int.Parse(NextAsString());
            }
            public string NextAsString()
            {
                return _inputs[_lineCursor++];
            }
        }

        public class Node
        {
            public int Start { get; set; }
            public int Length { get; set; }
            public bool IsEndNode { get; set; }
            public readonly List<Node> Children;
            
            public Node(int start, int length, bool isEndNode, IEnumerable<Node> children)
            {
                Start = start;
                Length = length;
                IsEndNode = isEndNode;
                Children = children.ToList();
            }
            
        }
    }
}
