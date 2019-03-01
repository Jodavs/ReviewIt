using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Values;
using Microsoft.AspNetCore.Builder;

namespace BDSA.ReviewIt.Server.StorageLayer.Conditions {
    public static class IntConditions //: ICondition<IntValue> 
    {
        public static bool LessThan(IntValue a, IntValue b) => a < b;
        public static bool LessThanOrEqual(IntValue a, IntValue b) => a <= b;
        public static bool GreaterThan(IntValue a, IntValue b) => a > b;
        public static bool GreaterThanOrEqual(IntValue a, IntValue b) => a >= b;
        public static bool Equal(IntValue a, IntValue b) => a == b;
        public static bool NotEqual(IntValue a, IntValue b) => a != b;

        /*private readonly ValueComparator<IntValue> _valueComparator;
        private readonly IntValueValue _value;
        public IntValueConditions(ValueComparator<IntValue> valueComparator, IntValueValue value) {
            _valueComparator = valueComparator;
            _value = value;
        }
        public bool Apply<V>(V a) where V : IntValueValue
        {
            return _valueComparator(a._value, _value._value);
        }*/
    }
}
