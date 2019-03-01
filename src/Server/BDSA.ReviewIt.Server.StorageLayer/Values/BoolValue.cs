using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.Values
{
    public class BoolValue : IValue
    {
        public readonly bool _value;

        public BoolValue(bool value)
        {
            _value = value;
        }
        public string GetString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            return _value == (obj as BoolValue)?._value;
        }

        protected bool Equals(BoolValue other)
        {
            return _value == other._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(BoolValue a, BoolValue b) => a._value == b._value;
        public static bool operator !=(BoolValue a, BoolValue b) => a._value != b._value;
    }
}
