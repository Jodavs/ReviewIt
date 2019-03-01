using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA.ReviewIt.Server.StorageLayer.Values
{
    public class StringValue : IValue
    {
        public readonly string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string GetString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            var bv = obj as StringValue;
            return bv != null && _value.Equals(bv._value);
        }

        protected bool Equals(StringValue other)
        {
            return string.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        public bool Contains(StringValue other) => _value.Contains(other._value);

        public static bool operator ==(StringValue a, StringValue b) => a.Equals(b);
        public static bool operator !=(StringValue a, StringValue b) => !a.Equals(b);
    }
}
