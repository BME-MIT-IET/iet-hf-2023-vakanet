using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public interface IAnimation<T> where T : class
    {
        public event Action<T> OnAnimationCompleted;
    }
}
