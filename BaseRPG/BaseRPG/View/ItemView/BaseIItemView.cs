using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.ItemView
{
    public abstract class BaseItemView : IDrawable
    {
        protected abstract Item ObservedItem { get; }

        public abstract Vector2D ObservedPosition { get; }

        public virtual bool Exists => ObservedItem.Exists;

        public virtual void OnRender(DrawingArgs drawingArgs)
        {
            throw new NotImplementedException();
        }
    }
}
