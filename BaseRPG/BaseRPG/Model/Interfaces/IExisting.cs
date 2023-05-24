using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces
{
    public interface IExisting
    {
        public event Action OnCeaseToExist;
        bool Exists { get; }
    }
}
