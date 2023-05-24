using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Exceptions
{
    internal class ViewUninitializedException : Exception
    {
        public ViewUninitializedException(string message = "The view you are trying to draw is not initialized") : base(message)
        {
        }
    }

}
