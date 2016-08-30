using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class SearchData
    {
        private static int NOT_VISITED = -1;
        private int[] _values { get; }

        public SearchData(int size)
        {
            _values = Enumerable.Repeat(NOT_VISITED, size).ToArray();
        }
        
        public bool Visited(int v)
        {
            return _values[v] != NOT_VISITED;
        }

        public ICollection<int> Values { get { return _values; } }
        public virtual void SetValue(int v, int value)
        {
            _values[v] = value;
        }
        public virtual int GetValue(int v)
        {
            return _values[v];
        }
    }
}