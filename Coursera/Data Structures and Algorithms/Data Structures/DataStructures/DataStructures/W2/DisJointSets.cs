using System;
using System.Linq;

namespace DataStructures.W2
{
    public class DisJointSets
    {
        private int[] _parent;
        private int[] _rank;

        public DisJointSets(int size)
        {
            var range = Enumerable.Range(0, size);
            _rank = range.Select(_ => 0).ToArray();
            _parent = range.Select(i => i).ToArray();
        }

        public int Find(int i)
        {
            //Root found
            if (i == _parent[i]) return i;

            //Non Root - Path Compression
            _parent[i] = Find(_parent[i]);
            return _parent[i];
        }

        public void Union(int a, int b)
        {
            if (a == b) return;

            //smaller depth tree under larger depth tree
            if (_rank[a] > _rank[b])
            {
                _parent[b] = a;
            }
            else
            {
                _parent[a] = b;
                if (_rank[a] == _rank[b]) _rank[b] += 1;
            } 
        }
    }
}
