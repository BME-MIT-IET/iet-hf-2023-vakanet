using BaseRPG.View.Animation;
using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class DefaultImageRenderer : IImageRenderer
    {
        private Transform2DEffect image = new();

        private double rotation;
        private Tuple<double, double> imageSize;

        public DefaultImageRenderer(string imageName, IImageProvider imageProvider)
        {
            image.Source = imageProvider.GetByFilename(imageName);
            imageSize = imageProvider.GetSizeByFilename(imageName);
        }


        public DefaultImageRenderer(ICanvasImage image, Tuple<double, double> sizeOfImage)
        {
            this.image.Source = image;
            imageSize = sizeOfImage;
        }

        public ICanvasImage Image { get => image; }

        public Angle ImageRotation => Angle.FromRadians(rotation);

        public Tuple<double, double> ImageSize => imageSize;

        public Vector2 PositionOnScreen(DrawingArgs drawingArgs) {
            return new(
                    (float)(drawingArgs.PositionOnScreen.X),
                    (float)(drawingArgs.PositionOnScreen.Y)
                    );
        }
        public void Render(DrawingArgs drawingArgs)
        {
            drawingArgs.DrawingSession.DrawImage(image, PositionOnScreen(drawingArgs));
        }

        public void SetImageRotation(double angle)
        {
            rotation = angle;
            image.TransformMatrix = Matrix3x2.CreateRotation(
                        (float)(angle));
        }

        public void SetImageScale(double xScale, double yScale)
        {
            image.TransformMatrix = Matrix3x2.CreateScale((float)xScale, (float)yScale);
        }
    }
}
