using BaseRPG.View.Interfaces.Providers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;

namespace BaseRPG
{
    public class ShiftedUpImageProvider : IImageProvider
    {
        private IImageProvider imageProvider;

        public ShiftedUpImageProvider(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
        }

        public ICanvasImage GetByFilename(string fileName)
        {
            var translatedImage = new Transform2DEffect {
                Source = imageProvider.GetByFilename(fileName),
                TransformMatrix = Matrix3x2.CreateTranslation(0,
                    (float)imageProvider.GetSizeByFilename(fileName).Item2)
            };
            return translatedImage;
        }

        public Tuple<double, double> GetSizeByFilename(string fileName)
        {
            return imageProvider.GetSizeByFilename(fileName);
        }
    }
}