using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;

namespace BaseRPG.View.Interfaces
{
    public interface IImageRenderer
    {
        ICanvasImage Image { get; }
        Tuple<double, double> ImageSize {get;}
        public Angle ImageRotation { get; }
        public void Render(DrawingArgs drawingArgs);
        void SetImageRotation(double angle);
        //void SetImageRotation(double angle, Vector2D around);
        void SetImageScale(double xScale, double yScale);
        
    }
}