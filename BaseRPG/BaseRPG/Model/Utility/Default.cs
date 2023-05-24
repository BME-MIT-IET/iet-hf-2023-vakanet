using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Utility
{
    public class Default<T>
    {
        private T defaultValue;
        private T currentValue;

        public T CurrentValue { get => currentValue; set => currentValue = value; }
        public T DefaultValue { get => defaultValue; set => defaultValue = value; }
        public Default(T defaultValue, T currentValue)
        {
            DefaultValue = defaultValue;
            CurrentValue = currentValue;
        }
        public Default(T defaultValue)
        {
            DefaultValue = defaultValue;
            CurrentValue = defaultValue;
        }
        
        public void Reset()
        {
            CurrentValue = DefaultValue;
        }
        public static implicit operator Default<T>(T a) => new Default<T>(a);
    }
    public class DefaultComparable<T> : Default<T> where T : IComparable<T> {
        public DefaultComparable(T defaultValue) : base(defaultValue)
        {
        }
        public DefaultComparable(T defaultValue, T currentValue) : base(defaultValue, currentValue)
        {
        }

        public bool isCurrentValueSmallerOrEqual 
        { 
            get 
            { 
                var result = CurrentValue.CompareTo(DefaultValue) <= 0; 
                return result;
            } 
        }
    }
    public class DefaultRef<T> :Default<T> where T : class
    {
        public DefaultRef(T defaultValue) : base(defaultValue)
        {
        }
        public DefaultRef(T defaultValue, T currentValue) : base(defaultValue, currentValue)
        {
        }

        public bool IsDefault
        {
            get
            {
                return CurrentValue == DefaultValue;
            }
        }
        public static implicit operator DefaultRef<T>(T a) => new DefaultRef<T>(a);
    }

}
