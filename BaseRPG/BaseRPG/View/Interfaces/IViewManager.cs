using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.Camera;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace BaseRPG.View.Interfaces
{
    public interface IViewManager
    {
        Vector2D CameraPosition { get; }
        CanvasVirtualControl Canvas { get; }
        Camera2D CurrentCamera { get; set; }
        WorldView.WorldView CurrentWorldView { get; set; }
        IPositionProvider GlobalMousePositionProvider { get; set; }

        void Draw(DrawingArgs drawingArgs);
        void SetCurrentWorldView(string worldName, World world, IImageProvider imageProvider, Camera2D camera);
    }
}