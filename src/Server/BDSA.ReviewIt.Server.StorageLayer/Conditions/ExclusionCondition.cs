using BDSA.ReviewIt.Server.StorageLayer.Values;
using Remotion.Linq.Clauses;

namespace BDSA.ReviewIt.Server.StorageLayer.Conditions
{
    public class ExclusionCondition<T> : IExclusionCondition<T> where T : IValue
    {
        
        public ValueComparator<T> Comparator { get; set; }
        public T Operator { get; set; }

        public ExclusionCondition(ValueComparator<T> comparator, IValue v)
        {
            Comparator = comparator;
            Operator = (T)v;
        }

        public bool Apply(T a) => Comparator(a, Operator);
    }
}