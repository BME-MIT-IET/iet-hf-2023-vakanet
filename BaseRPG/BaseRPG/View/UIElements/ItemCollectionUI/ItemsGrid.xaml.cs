using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.UIElements.CustomControl;
using BaseRPG.View.UIElements.DrawingArgsFactory;
using BaseRPG.View.UIElements.Initialization;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements.ItemCollectionUI
{
    public sealed partial class ItemsGrid : UserControl
    {
        private IItemCollectionControl itemCollectionControl;
        private IDrawableProvider drawableProvider;
        public List<ButtonWithCanvas> ItemPlaces
        {
            get 
            {
                List<ButtonWithCanvas> result = new();
                foreach (var item in grid.Children)
                {
                    if (item is ButtonWithCanvas)
                        result.Add(item as ButtonWithCanvas);
                }
                return result;
            }
        }


        public ItemsGrid()
        {
            this.InitializeComponent();
            
        }

        public void Update() {
            for (int i = 0; i < itemCollectionControl.Capacity; i++)
            {
                var item = itemCollectionControl.GetItemAt(i);
                ItemPlaces[i].Drawable = drawableProvider.GetDrawable(item, "inventory");
            }
            ItemPlaces.ForEach(i => i.Canvas.Invalidate());
        }

        internal void Init(IItemCollectionControl itemCollectionControl, IDrawableProvider drawableProvider)
        {
            this.drawableProvider = drawableProvider;
            this.itemCollectionControl = itemCollectionControl;
            itemCollectionControl.OnChanged += ()=>DispatcherQueue.TryEnqueue(Update);
            new GridFillStrategy().Fill(grid, CreateButtonWithCanvas, 4, itemCollectionControl.Capacity);

            ItemPlaces.ForEach(i => i.DrawingArgsFactory = new ImageButtonDrawingArgsFactory(new(i.Canvas.Width, i.Canvas.Height)));
            for (int i = 0; i < ItemPlaces.Count; i++)
            {
                int index = i;
                ItemPlaces[i].ButtonClick += (s, a) => ItemLeftClicked(index);
                ItemPlaces[i].ButtonRightClick += (s, a) => ItemRightClicked(index);
            }
            Update();
        }

        private void ItemRightClicked(int index)
        {
            itemCollectionControl.OnItemRightClicked(index);
            Update();
        }

        private ButtonWithCanvas CreateButtonWithCanvas(int index) {
            var buttonWithCanvas = new ButtonWithCanvas();
            buttonWithCanvas.Button.BorderThickness = new Thickness(1);

            return buttonWithCanvas;
        }
        private void ItemLeftClicked(int index) {
            itemCollectionControl.OnItemLeftClicked(index);
            Update();
        }
    }
}
