using System.Collections;

namespace AlgorithmsOnGraphs
{
    public class DepthFirstSearchWithOrdering : DepthFirstSearch
    {
        private readonly int[] _pre;
        private readonly int[] _post;

        public DepthFirstSearchWithOrdering(ISearchableGraph g) : base(g)
        {
            _pre = new int[g.Size()];
            _post = new int[g.Size()];
        }

        private int _counter;
        public override void Search()
        {
            _counter = 1;
            base.Search();
        }

        protected override void PreVisitHook(int v)
        {
            _pre[v] = _counter++;
        }
        protected override void PostVisitHook(int v)
        {
            _post[v] = _counter++;
        }
    }
}
