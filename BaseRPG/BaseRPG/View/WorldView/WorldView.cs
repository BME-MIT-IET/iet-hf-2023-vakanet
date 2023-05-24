using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Worlds;
using BaseRPG.View.Animation;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.WorldView
{
    public class WorldView
    {
        private World world;
        private IImageRenderer backgroundImageRenderer;
        private Camera2D camera;
        private LayerHandler layerHandler = new();
        private object _lock = new object();
        public WorldView(World world, ICanvasImage background, Tuple<double, double> sizeOfImage, Camera2D camera)
        {
            this.world = world;
            this.backgroundImageRenderer = new DefaultImageRenderer(background, sizeOfImage);
            this.camera = camera;
        }

        public Camera2D CurrentCamera { get { return camera; } set { camera = value; } }

        public void AddView(IDrawable gameObjectView, int layer = 0)
        {
            if (gameObjectView == null) return;
            layerHandler.AddToLayer(layer, gameObjectView);
        }

        // this function will always draw all of the drawables,
        // even if a new one was added mid-way, BUT this makes it much slower,
        // therefore the sometimes (very rarely) inaccurate
        // (as in sometimes it doesn't draw an image for a frame),
        // but much less costly  simple for loop is used for now
        public void alwaysDraw(DrawingArgs drawingArgs) {
            try
            {
                foreach (var drawable in layerHandler.Drawables)
                {
                    drawable.Render(drawingArgs, camera);
                }
            }
            catch (InvalidOperationException e) { 
                alwaysDraw(drawingArgs);
            }
        }
        public void Render(DrawingArgs drawingArgs)
        {
            var backgoundPos = camera.CalculatePositionOnScreen(new Vector2D(0, 0));
            drawingArgs.PositionOnScreen = backgoundPos;
            backgroundImageRenderer.Render(drawingArgs);
            camera.Update();
            lock (_lock)
            {
                var drawables = layerHandler.Drawables.ToList();
                // uncomment this code (and comment out the for loop) for the slower but surer way of drawing
                // alwaysDraw(drawingArgs);

                // Because of multiple threads, the list can change during the process of drawing,
                // so normal foreach couldn't be used (locking the adding operation causes deadlock
                // in the current state of the code, so don't do that)
                for (int i = 0; i < drawables.Count; i++)
                {
                    if (drawables.Count > i)
                    {
                        drawables[i].Render(drawingArgs, camera);
                    }
                }
                layerHandler.RemoveAll(d => !d.Exists);
            }
            
            

        }

        internal void RemoveView(IDrawable drawable)
        {
            lock (_lock)
            {
                layerHandler.Remove(drawable);
            }
        }
    }
}
