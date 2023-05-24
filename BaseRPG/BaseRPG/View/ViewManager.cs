using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.Camera;
using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;


namespace BaseRPG.View
{
    public class ViewManager : IViewManager
    {
        private WorldView.WorldView currentWorldView;
        private IWorldNameImageMapper worldNameImageMapper;
        private CanvasVirtualControl canvas;
        public WorldView.WorldView CurrentWorldView { get { return currentWorldView; } set { currentWorldView = value; } }

        public Camera2D CurrentCamera { get { return currentWorldView.CurrentCamera; } set { currentWorldView.CurrentCamera = value; } }
        public IPositionProvider GlobalMousePositionProvider { get; set; }
        public CanvasVirtualControl Canvas { get => canvas; }
        public Vector2D CameraPosition => CurrentCamera.MiddlePosition;

        public void SetCurrentWorldView(string worldName, World world, IImageProvider imageProvider, Camera2D camera)
        {
            canvas.SizeChanged += camera.OnCanvasSizeChanged;
            CurrentWorldView = new WorldView.WorldView(
                world,
                imageProvider.GetByFilename(worldNameImageMapper.ToImageName(worldName)),
                imageProvider.GetSizeByFilename(worldNameImageMapper.ToImageName(worldName)),
                camera
            );
        }
        public ViewManager(IWorldNameImageMapper worldNameImageMapper, ICanvasProvider canvasProvider)
        {
            this.worldNameImageMapper = worldNameImageMapper;
            this.canvas = canvasProvider.Canvas;
        }

        internal Vector2D CalculatePositionOnScreen(IPositionUnit globalPosition)
        {
            return CurrentCamera.CalculatePositionOnScreen(globalPosition);
        }
        internal Angle CalculateAngle(IPositionUnit globalPosition, Vector2D relativePointOnScreen)
        {
            return (relativePointOnScreen
                - CalculatePositionOnScreen(globalPosition))
                .SignedAngleTo(new(1, 0), true);
        }
        public void Draw(DrawingArgs drawingArgs)
        {
            if (currentWorldView == null)
            {
                throw new ViewUninitializedException();
            }
            currentWorldView.Render(drawingArgs);
        }
    }
}
