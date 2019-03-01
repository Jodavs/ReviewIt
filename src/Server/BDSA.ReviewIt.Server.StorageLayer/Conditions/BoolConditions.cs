using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDSA.ReviewIt.Server.StorageLayer.Values;

namespace BDSA.ReviewIt.Server.StorageLayer.Conditions
{
    public static class BoolConditions //: ICondition<BoolValue>
    {
        //private readonly BoolValue _value;

        public static bool Compare(BoolValue a, BoolValue b) => a == b;

        /* public BoolConditions(BoolValue value)
         {
             _value = value;
         }

         public bool Apply<V>(V a) where V : BoolValue
         {
             return a._value == _value._value;
         }*/
    }
}
