using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Values;

namespace BDSA.ReviewIt.Server.StorageLayer.Conditions
{
    public static class StringConditions //: ICondition<StringValue>
    {
        public static bool Equal(StringValue a, StringValue b) => a.Equals(b);
        public static bool IsContained(StringValue a, StringValue b) => b.Contains(a);

       /* private readonly ValueComparator<string> _valueComparator;
        private readonly StringValue _value;

        public StringConditions(ValueComparator<string> valueComparator, StringValue value)
        {
            _valueComparator = valueComparator;
            _value = value;
        }
        public bool Apply<V>(V a) where V : StringValue
        {
            return _valueComparator(a._value, _value._value);
        }*/
    }
}
