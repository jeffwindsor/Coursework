using System.Collections.Generic;

namespace AlgorithmsOnGraphs
{
    public interface ISearchableGraph
    {
        int Size();

        IEnumerable<int> Neighbors(int i);
    }
}