using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    /// <summary>
    /// Uses Hash and Chaining
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Set<TValue>
        where TValue : class, IEquatable<TValue>
    {
        private readonly List<TValue>[] _lists;
        private readonly Func<TValue, int> _hashFunction;
        public Set(int size, Func<TValue, int> hashFunction)
        {
            _lists = new List<TValue>[size];
            _hashFunction = hashFunction;
        }

        public bool Find(TValue value)
        {
            return (FindValue(value) != null);
        }

        public void Add(TValue value)
        {
            var l = GetList(value);
            if(FindValue(value,l) == null)
                l.Add(value);
        }

        public void Remove(TValue value)
        {
            var l = GetList(value);
            if (FindValue(value, l) == null)
                l.Remove(value);
        }


        private List<TValue> GetList(int i)
        {
            return _lists[i] ?? (_lists[i] = new List<TValue>());
        }
        private List<TValue> GetList(TValue value)
        {
            return GetList(_hashFunction(value));
        }
        private TValue FindValue(TValue value)
        {
            return FindValue(value, GetList(value));
        }
        private static TValue FindValue(TValue value, IEnumerable<TValue> list)
        {
            return list.FirstOrDefault(o => o.Equals(value));
        }
    }
}
