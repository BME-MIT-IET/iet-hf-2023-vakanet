using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.Animation;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;


namespace BaseRPG.View.ItemView
{
    public class InventoryItemView : BaseItemView
    {
        private Item item;
        private readonly DrawingImage image;


        public InventoryItemView(Item item, DrawingImage image)
        {
            this.item = item;
            this.image = image;
        }

        public override Vector2D ObservedPosition => new(0,0);


        protected override Item ObservedItem => item;

        public override void OnRender(DrawingArgs drawingArgs)
        {
            image.OnRender(drawingArgs);
        }
    }
}
