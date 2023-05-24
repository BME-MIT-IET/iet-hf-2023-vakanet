using BaseRPG.Model.Interfaces;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace BaseRPG.View.EntityView
{
    public class ShapeView:IDrawable
    {
        private IShape2D shape;
        private IPositionProvider positionProvider;
        private Color fillColor = Color.FromArgb(100, 255, 0, 0);
        private Color borderColor = Color.FromArgb(100, 255, 0, 0);
        private float borderThickness;

        public ShapeView(IShape2D shape, IPositionProvider positionProvider, Color? fillColor = null, Color? borderColor = null,float borderThickness = 1 )
        {
            this.shape = shape;
            this.positionProvider = positionProvider;
            this.borderThickness = borderThickness;
            if (fillColor.HasValue)
                this.fillColor = fillColor.Value;
            if (borderColor.HasValue)
                this.borderColor = borderColor.Value;
        }

        public bool Exists => shape.Owner.Exists;

        public Vector2D ObservedPosition => positionProvider.Position;

        public void OnRender(DrawingArgs drawingArgs)
        {
            IEnumerable<Point2D> vertices = shape.ToPolygon2D().Vertices;
            Vector2[] verticesArray = vertices.Select(v => new Vector2((float)v.X, (float)v.Y)).ToArray();
            var middle = shape.LastCalculatedMiddle; 
            drawingArgs.DrawingSession.FillGeometry(
                CanvasGeometry.CreatePolygon(drawingArgs.Sender, verticesArray),
                new Vector2(
                    (float)(drawingArgs.PositionOnScreen.X ),
                    (float)(drawingArgs.PositionOnScreen.Y )),
                fillColor);
            drawingArgs.DrawingSession.DrawGeometry(
                CanvasGeometry.CreatePolygon(drawingArgs.Sender, verticesArray),
                new Vector2(
                    (float)(drawingArgs.PositionOnScreen.X),
                    (float)(drawingArgs.PositionOnScreen.Y)),
                borderColor,borderThickness);
            drawingArgs.DrawingSession.FillCircle(
                    (float)(drawingArgs.PositionOnScreen.X+middle.X),
                    (float)(drawingArgs.PositionOnScreen.Y+middle.Y),
                    5, Color.FromArgb(200,255,255,255));
        }
        public bool MouseOver(DrawingArgs drawingArgs) {
            return shape.Shifted(drawingArgs.PositionOnScreen).IsCollidingPoint(drawingArgs.MousePositionOnScreen);
        }
    }
}
