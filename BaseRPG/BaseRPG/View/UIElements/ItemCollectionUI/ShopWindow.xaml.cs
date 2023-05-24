using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Model.Worlds.InteractionPoints;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces.Providers;
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
    public sealed partial class ShopWindow : CustomWindow
    {
        public ShopWindow(IDrawableProvider drawableProvider, ShopControl shopControl)
        {
            this.InitializeComponent();
            itemsGrid.Init(shopControl, drawableProvider);
        }
        public override void OnOpened()
        {
            base.OnOpened();
            itemsGrid.Update();
        }
        public static string WindowName => "shop";
    }
}
