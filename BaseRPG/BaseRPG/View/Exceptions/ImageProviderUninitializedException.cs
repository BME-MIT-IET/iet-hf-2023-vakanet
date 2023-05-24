using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Exceptions
{
    internal class ImageProviderUninitializedException : Exception
    {
        public ImageProviderUninitializedException(string message = "The image provider you are using hasnt been initialized") : base(message)
        {
        }
    }
}
