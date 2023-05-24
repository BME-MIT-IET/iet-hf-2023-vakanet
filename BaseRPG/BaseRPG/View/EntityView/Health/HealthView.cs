using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRPG.Model.Attribute;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Windows.UI;

namespace BaseRPG.View.EntityView.Health
{
    public delegate void DrawingFunction(float x, float y, float width, float height, float cornerX, float cornerY, Color color);
    public class HealthView
    {
        private Model.Attribute.Health health;
        private readonly Color color;
        private readonly float cornerRadius;
        private float width;
        private float height;
        public float BorderWidth => width/35;
        public float Width { 
            set 
            {
                width = value;
                height = value / 8;
            } 
        }
        public HealthView(Model.Attribute.Health health, float width, Color color, float cornerRadius = 0)
        {
            this.health = health;
            this.Width = width;
            this.color = color;
            this.cornerRadius = cornerRadius;
        }
        public void Render(DrawingArgs drawingArgs)
        {
            CanvasDrawingSession drawingSession = drawingArgs.DrawingSession;
            DrawParams drawParams = new DrawParams(
                (float)drawingArgs.PositionOnScreen.X,
                (float)drawingArgs.PositionOnScreen.Y,
                (float)(width * (health.CurrentValue / health.MaxValue)),
                height,
                cornerRadius,
                color);
            if (cornerRadius <= float.Epsilon) {
                DrawRoundedOrNormal(drawParams, 
                    (x, y, width, height, cornerX, cornerY, color) => drawingSession.FillRectangle(x, y, width, height, color),
                    (x, y, width, height, cornerX, cornerY, color) => drawingSession.DrawRectangle(x, y, width, height, color,Math.Min(width/30,6)));
                return;
            }
            DrawRoundedOrNormal(drawParams, 
                drawingSession.FillRoundedRectangle, 
                (x, y, width, height, cornerX, cornerY, color) => 
                    drawingSession.DrawRoundedRectangle(x, y, width, height,cornerX,cornerY, color, Math.Min(width / 30, 4)));
            
        }

        private void DrawRoundedOrNormal(DrawParams drawParams, DrawingFunction fillDrawingFunction, DrawingFunction borderDrawingFunction) {
            if (health.CurrentValue > 0)
            {
                Draw(drawParams, fillDrawingFunction);
                var drawParams2 = drawParams.Copy();
                drawParams2.Width = (float)(width * (health.CurrentValue / health.MaxValue)) - width / 10;
                drawParams2.Height /= 2;
                drawParams2.PositionX += width / 20;
                drawParams2.PositionY += height / 2;
                drawParams2.Color = Brighten(color, 50);
                if (drawParams2.Width > 0)
                    Draw(drawParams2, fillDrawingFunction);
            }
            drawParams.Color = Color.FromArgb(255, 255, 255, 255);
            drawParams.Width = width;
            Draw(drawParams,borderDrawingFunction);
        }

        private Color Brighten(Color color, byte amount) {
            var result = color;
            
            if (result.R >= 255 - amount)
                result.R = 255;
            else result.R += amount;
            if (result.G >= 255 - amount)
                result.G = 255;
            else result.G += amount;
            if (result.B >= 255 - amount)
                result.B = 255;
            else result.B += amount;
            return result;
        }

  
        private void Draw(DrawParams drawParams, DrawingFunction drawingFunction)
        {
            drawingFunction(
                drawParams.PositionX,
                drawParams.PositionY,
                drawParams.Width,
                drawParams.Height,
                drawParams.CornerRadius,
                drawParams.CornerRadius,
                drawParams.Color);
        }

        private class DrawParams {
            public float PositionX { get; set; }
            public float PositionY { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public float CornerRadius { get; init; }
            public Color Color { get; set; }

            public DrawParams(float positionX, float positionY, float width, float height, float cornerRadius, Color color)
            {
                PositionX = positionX;
                PositionY = positionY;
                Width = width;
                Height = height;
                CornerRadius = cornerRadius;
                Color = color;
            }
            public DrawParams Copy() {
                return new DrawParams(PositionX, PositionY, Width, Height, CornerRadius, Color);
            }
        }
    }
}
