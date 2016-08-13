namespace DataStructures
{
    public class BinarySearchTreeNode
    {
        private BinarySearchTreeNode _left;
        private BinarySearchTreeNode _right;

        public long Key { get; set; }
        public BinarySearchTreeNode Parent { get; set; }

        public BinarySearchTreeNode Left {
            get { return _left; }
            set
            {
                _left = value;
                if(value != null)
                    value.Parent = this;
            }
        }
        public BinarySearchTreeNode Right {
            get { return _right; }
            set
            {
                _right = value;
                if (value != null)
                    value.Parent = this;
            }
        }

        public int Rank { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Key); //string.Format("{0}:{1}", Key, Rank);
        }
    }
}