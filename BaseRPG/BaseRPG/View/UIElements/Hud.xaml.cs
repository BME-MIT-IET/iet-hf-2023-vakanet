using BaseRPG.Model.Data;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.View.EntityView.Health;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.UIElements.DrawingArgsFactory;
using BaseRPG.View.UIElements.ItemCollectionUI;
using BaseRPG.View.UIElements.Spell;
using Microsoft.Graphics.Canvas.UI.Xaml;
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
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements
{
    public sealed partial class Hud : UserControl
    {
        private IImageProvider imageProvider;
        private Hero hero;
        public CanvasControl SettingsButtonCanvas => settingsButton.Canvas;
        public event Action<string> WindowButtonClicked;
        public Hud()
        {
            this.InitializeComponent();
            
        }
        public void Init(IImageProvider imageProvider, Hero hero) {
            ImageProvider = imageProvider;
            Hero = hero;
            var settingsImage = new DrawingImage(@"Assets\image\icons\settings-outlined.png", ImageProvider);
            settingsButton.SetImageAsDrawable(settingsImage);

            var inventoryImage = new DrawingImage(@"Assets\image\icons\inventory-outlined.png", ImageProvider);
            inventoryButton.SetImageAsDrawable(inventoryImage);

            var spellsImage = new DrawingImage(@"Assets\image\icons\spells-outlined.png", ImageProvider);
            spellsButton.SetImageAsDrawable(spellsImage);

            goldUI.GoldIcon = new DrawingImage(@"Assets\image\icons\gold-outlined.png", ImageProvider);
            goldUI.Hero = hero;

            goldUI.Canvas.Invalidate();
            SettingsButtonCanvas.Invalidate();
            InventoryButtonCanvas.Invalidate();
            spellsButton.Canvas.Invalidate();
        }

        public IImageProvider ImageProvider { get => imageProvider; set => imageProvider = value; }
        public CanvasControl InventoryButtonCanvas => inventoryButton.Canvas;
        public HealthView HealthView {
            set 
            {
                healthViewControl.HealthView = value; 
            }
        }

        public Hero Hero {
            get {
                return hero;
            }
            internal set {
                var heroWasNull = hero == null;
                hero = value;
                HealthView = new HealthView(hero.Health,250, Color.FromArgb(255, 0, 150, 255),4);

                if (heroWasNull)
                {
                    levelText.Text = hero.Level.ToString();
                    hero.OnLevelUpCallback(
                        (newLevel)=> DispatcherQueue.TryEnqueue(() => levelText.Text = newLevel.ToString())
                    );
                    healthPercentText.Text = hero.Health.HealthPercentage.ToString();
                    hero.Health.HealthCanged += () => DispatcherQueue.TryEnqueue(() => healthPercentText.Text = hero.Health.HealthPercentage.ToString());
                   
                    experienceControl.Hero = hero;
                }
            }
        }

        #region callback functions
        public void settingsButton_Draw(CanvasControl sender, CanvasDrawEventArgs args) {
        }


        public void inventoryButton_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowButtonClicked?.Invoke(SettingsWindow.WindowName);
        }

        private void inventoryButton_Click(object sender, RoutedEventArgs e)
        {
            WindowButtonClicked?.Invoke(InventoryWindow.WindowName);
        }

        private void levelText_LayoutUpdated(object sender, object e)
        {

        }
        

        private void spellsButton_ButtonClick(object arg1, RoutedEventArgs arg2)
        {
            WindowButtonClicked?.Invoke(SpellsWindow.WindowName);
        }

        private void spellsButton_CanvasDraw(CanvasControl arg1, CanvasDrawEventArgs arg2)
        {

        }
        #endregion
    }
}
