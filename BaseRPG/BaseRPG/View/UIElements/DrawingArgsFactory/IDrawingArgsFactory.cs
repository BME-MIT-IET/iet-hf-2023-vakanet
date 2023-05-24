using BaseRPG.View.Animation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.UIElements.DrawingArgsFactory
{
    public interface IDrawingArgsFactory
    {
        public DrawingArgs Create(CanvasControl sender, CanvasDrawEventArgs args);
        public DrawingArgs Create(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args);
    }
}
