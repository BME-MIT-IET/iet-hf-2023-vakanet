using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BaseRPG.View.Camera
{
    public class Camera2D
    {
        private int width;
        private int height;
        private Vector2D position;
        public Vector2D MiddlePosition
        {
            get 
            {
                return position;
            }
            set
            {
                position = value - new Vector2D(Width / 2.0, Height / 2.0);
            }

        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public Camera2D(Vector2D position, int width, int height)
        {
            
            Width = width;
            Height = height;
            MiddlePosition = position;
        }
        public Camera2D(Vector2D position, int width) :
            this(position, width, (int)Math.Round(9.0 / 16.0 * width, 0))
        {

        }

        public virtual void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            width = (int)Math.Round(e.NewSize.Width, 0);
            height = (int)Math.Round(e.NewSize.Height, 0);
        }

        public Camera2D(Vector2D position, Size size) :
            this(position, (int)Math.Round(size.Width, 0), (int)Math.Round(size.Height, 0))
        {

        }
        public Vector2D CalculatePositionOnScreen(IPositionUnit globalPosition)
        {
            return CalculatePositionOnScreen(PositionUnit2D.ToVector2D(globalPosition));
        }
        public Vector2D CalculatePositionOnScreen(Vector2D globalPosition)
        {
            return globalPosition - MiddlePosition;
        }

        public virtual void Update()
        {
        }
    }
}
