using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces.Providers;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class ScalingImageProvider:IImageProvider
    {
        private float xScaleFactor;
        private float yScaleFactor;
        private IImageProvider imageProvider;
        public float XScaleFactor { get => xScaleFactor; }
        public float YScaleFactor { get => yScaleFactor; }
        public ScalingImageProvider(float scaleFactor, IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
            this.xScaleFactor = scaleFactor;
            this.yScaleFactor = scaleFactor;
        }
        

        public ICanvasImage GetByFilename(string fileName)
        {
            var scaleEffect = new ScaleEffect();
            scaleEffect.Scale = new(XScaleFactor, YScaleFactor);
            scaleEffect.InterpolationMode = CanvasImageInterpolation.NearestNeighbor;
            scaleEffect.Source = imageProvider.GetByFilename(fileName);
            return scaleEffect;
        }

        public Tuple<double, double> GetSizeByFilename(string fileName)
        {
            if (fileName == null) return null;
            Tuple<double, double> result = imageProvider.GetSizeByFilename(fileName);
            return new(result.Item1* XScaleFactor, result.Item2* YScaleFactor);
        }
    }
}
