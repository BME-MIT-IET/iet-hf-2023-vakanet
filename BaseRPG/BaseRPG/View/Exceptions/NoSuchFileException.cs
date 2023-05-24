using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Exceptions
{
    internal class NoSuchFileException : Exception
    {
        public NoSuchFileException(string message = "The file you are trying to acces doesnt exist.") : base(message)
        {
        }
    }
}
