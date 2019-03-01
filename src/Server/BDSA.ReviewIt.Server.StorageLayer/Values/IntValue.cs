using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.Values
{
    public class IntValue : IValue, IComparable<IntValue>
    {
        public readonly int _value;

        public IntValue(int value)
        {
            _value = value;
        }
        public string GetString()
        {
            return _value.ToString();
        }

        public int CompareTo(IntValue other)
        {
            return _value - other._value;
        }

        public override bool Equals(object obj)
        {
            return _value == (obj as IntValue)?._value;
        }

        protected bool Equals(IntValue other)
        {
            return _value == other._value;
        }

        public override int GetHashCode()
        {
            return _value;
        }

        public static bool operator ==(IntValue a, IntValue b) => a.Equals(b);
        public static bool operator !=(IntValue a, IntValue b) => !a.Equals(b);
        public static bool operator >(IntValue a, IntValue b) => a.CompareTo(b) > 0;
        public static bool operator <(IntValue a, IntValue b) => a.CompareTo(b) < 0;
        public static bool operator >=(IntValue a, IntValue b) => a.CompareTo(b) >= 0;
        public static bool operator <=(IntValue a, IntValue b) => a.CompareTo(b) <= 0;
    }
}
