using Windows.Foundation;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;
using BaseRPG.Model.Interfaces.Movement;
using Windows.Storage;
using BaseRPG.View.Camera;
using BaseRPG.View.Animation;
using BaseRPG.Model.Interfaces;
using BaseRPG.Physics.TwoDimensional.Collision;

namespace BaseRPG.View.EntityView
{
    public interface IDrawable
    {
        bool Exists { get; }
        Vector2D ObservedPosition { get; }
        void Render(DrawingArgs drawingArgs, Camera2D camera) {
            drawingArgs.PositionOnScreen = camera.CalculatePositionOnScreen(ObservedPosition);
            OnRender(drawingArgs);
        }
        void OnRender(DrawingArgs drawingArgs);

    }
}
