using System.Text;

namespace DataStructures
{
    public static class BinarySearchTreeNodePrinter
    {
        public static string Print(BinarySearchTreeNode node)
        {

            var sb = new StringBuilder();
            Print(node, sb);
            return sb.ToString();
        }

        private static void Print(BinarySearchTreeNode node, StringBuilder sb)
        {

            if (node != null && node.Right != null)
            {
                Print(node.Right, sb, true, "");
            }
            PrintNodeValue(node, sb);
            if (node != null && node.Left != null)
            {
                Print(node.Left, sb, false, "");
            }
        }

        private static void PrintNodeValue(BinarySearchTreeNode node, StringBuilder sb)
        {
            sb.AppendLine((node == null) ? "<null>" : node.ToString());
        }

        // use string and not stringbuffer on purpose as we need to change the indent at each recursion
        private static void Print(BinarySearchTreeNode node, StringBuilder sb, bool isRight, string indent)
        {
            if (node.Right != null)
            {
                Print(node.Right, sb, true, indent + (isRight ? "        " : " |      "));
            }
            sb.Append(indent);
            sb.Append(isRight ? " /" : " \\");
            sb.Append("----- ");

            PrintNodeValue(node, sb);
            if (node.Left != null)
            {
                Print(node.Left, sb, false, indent + (isRight ? " |      " : "        "));
            }
        }

    }
}
