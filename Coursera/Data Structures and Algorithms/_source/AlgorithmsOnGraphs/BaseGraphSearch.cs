namespace AlgorithmsOnGraphs
{
    public class BaseGraphSearch
    {
        public readonly ISearchableGraph Graph;
        public readonly SearchData SearchData;

        public BaseGraphSearch(ISearchableGraph g)
        {
            Graph = g;
            SearchData = new SearchData(g.Size());
        }
    }
}