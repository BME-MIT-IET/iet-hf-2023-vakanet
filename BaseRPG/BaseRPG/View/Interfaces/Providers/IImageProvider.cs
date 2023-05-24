using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces.Providers
{
    public interface IImageProvider
    {
        /// <exception cref="Exceptions.NoSuchFileException"></exception>
        ICanvasImage GetByFilename(string fileName);
        Tuple<double, double> GetSizeByFilename(string fileName);
    }
}
