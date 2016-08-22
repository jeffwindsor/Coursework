using System.Collections.Generic;

namespace AlgorithmsOnGraphs
{
    public interface IGraph
    {
        int Size();

        IEnumerable<int> Neighbors(int i);
    }
}