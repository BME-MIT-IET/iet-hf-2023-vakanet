using BaseRPG.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Utility
{
    public class Lockable<T>
    {
        private T value;
        public bool Locked { get; set; } = false;
        public T Value { 
            get 
            {
                return value;
            }
            set 
            {
                if (Locked) throw new ValueLockedException("You can't change the value while it is locked!");
                this.value = value;
            }
        }
        public Lockable(T value)
        {
            this.value = value;
        }
    }
}
