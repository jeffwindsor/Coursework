using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsOnGraphs
{
    public class SearchData
    {
        private readonly int[] _values;
        public readonly int InitialValue;
        public SearchData(int size, int initialValue = -1)
        {
            InitialValue = initialValue;
            _values = Enumerable.Repeat(initialValue, size).ToArray();
        }
        
        public bool Visited(int v)
        {
            return _values[v] != InitialValue;
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