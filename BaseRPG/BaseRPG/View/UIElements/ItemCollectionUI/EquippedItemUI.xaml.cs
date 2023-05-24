using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.ItemView;
using BaseRPG.View.UIElements.DrawingArgsFactory;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
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
    public sealed partial class EquippedItemUI : UserControl
    {
        private InventoryControl inventoryControl;
        private IDrawable inventoryWeaponView;
        public EquippedItemUI()
        {
            this.InitializeComponent();
            
            
        }
        public void Init(InventoryControl inventoryControl, IDrawableProvider drawableProvider) {
            InventoryControl = inventoryControl;
            InventoryWeaponView = drawableProvider.GetDrawable(inventoryControl.EquippedWeapon,"inventory");

            inventoryControl.OnChanged += () => DispatcherQueue.TryEnqueue(()=>Update(drawableProvider));
            inventoryControl.OnChanged += () => DispatcherQueue.TryEnqueue(() => Update(drawableProvider));
            Update(inventoryControl.DrawableProvider);
            equippedWeaponButton.EquippedArmor = InventoryWeaponView;
            equippedWeaponButton.DrawingArgsFactory = new ImageButtonDrawingArgsFactory(new(EquippedWeaponCanvas.Width,EquippedWeaponCanvas.Height));
        }
        public IDrawable InventoryWeaponView { get => inventoryWeaponView; set => inventoryWeaponView = value; }
        public InventoryControl InventoryControl { get => inventoryControl; set => inventoryControl = value; }
        public CanvasControl EquippedWeaponCanvas { get => 
                equippedWeaponButton.Canvas;
        }
        private void Update(IDrawableProvider drawableProvider) {
            equippedWeaponButton.Drawable = drawableProvider.GetDrawable(inventoryControl.EquippedWeapon, "inventory");
            equippedShoeButton.Drawable = drawableProvider.GetDrawable(inventoryControl.EquippedArmor, "inventory");
            equippedArmorButton.Drawable = drawableProvider.GetDrawable(inventoryControl.EquippedShoe, "inventory");
            EquippedWeaponCanvas.Invalidate();
        }
        #region callback functions
        private void equippedWeaponCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {

        }
        private void equippedWeaponButton_Click(object sender, RoutedEventArgs e)
        {
            inventoryControl.UnEquipItem(inventoryControl.Inventory.EquippedWeapon);
        }

        private void equippedArmorCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {

        }
        private void equippedArmorButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void equippedShoeButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void equippedShoeCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {

        }
        #endregion
    }
}
