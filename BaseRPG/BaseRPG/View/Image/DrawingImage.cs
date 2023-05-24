using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class DrawingImage:IDrawable
    {
        private string imagePath;
        private IImageProvider imageProvider;
        private readonly Vector2D position;

        public DrawingImage(string imagePath, IImageProvider imageProvider,Vector2D position)
        {
            this.imagePath = imagePath;
            this.imageProvider = imageProvider;
            this.position = position;
        }
        public DrawingImage(string imagePath, IImageProvider imageProvider):this(imagePath,imageProvider,new(0,0))
        {
        }
        public void OnRender(DrawingArgs drawingArgs) {
            Vector2D pos = drawingArgs.PositionOnScreen;
            drawingArgs.DrawingSession.DrawImage(imageProvider.GetByFilename(imagePath),new((float)pos.X,(float)pos.Y));
        }
        public Tuple<double, double> Size {
            get {
                return imageProvider.GetSizeByFilename(imagePath);
            }
        }

        public bool Exists => true;

        public Vector2D ObservedPosition => 
            position;

        public IImageProvider ImageProvider => imageProvider;
        public string ImageName => imagePath;
    }
}
