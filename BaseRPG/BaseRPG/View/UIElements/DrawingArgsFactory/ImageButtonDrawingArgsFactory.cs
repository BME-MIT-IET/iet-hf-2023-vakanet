using BaseRPG.View.Animation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BaseRPG.View.UIElements.DrawingArgsFactory
{
    public class ImageButtonDrawingArgsFactory : IDrawingArgsFactory
    {
        private Size canvasSize;

        public ImageButtonDrawingArgsFactory(Size canvasSize)
        {
            this.canvasSize = canvasSize;
        }

        public DrawingArgs Create(CanvasControl sender, CanvasDrawEventArgs args)
        {
            return new DrawingArgs(sender, 1, new(canvasSize.Width / 2, canvasSize.Height / 2), new(0, 0), args.DrawingSession);
        }

        public DrawingArgs Create(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
