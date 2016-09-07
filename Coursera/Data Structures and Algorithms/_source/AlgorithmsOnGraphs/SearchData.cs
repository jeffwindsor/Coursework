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

        public int Length {get { return _values.Length; }}
        public ICollection<int> Values { get { return _values; } }
        public virtual void SetValue(int v, int value)
        {
            _values[v] = value;
        }
        public virtual int GetValue(int v)
        {
            return _values[v];
        }

        public override string ToString()
        {
            var items = _values.Select((v, i) => string.Format("{0}:{1}", i, v));
            return string.Join(", ", items);
        }
    }
}