using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.View.EntityView;
using BaseRPG.View.UIElements.DrawingArgsFactory;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements.Gold
{
    public sealed partial class GoldUI : UserControl
    {
        private Hero hero;
        public Hero Hero { get => hero; set{
                hero = value;
                goldText.Text = hero.Gold.ToString();
                hero.GoldChanged += g => DispatcherQueue.TryEnqueue(() => goldText.Text = g.ToString());
            } }
        public IDrawable GoldIcon { get; set; }
        public CanvasControl Canvas => goldCanvas;
        public GoldUI()
        {
            this.InitializeComponent();
            
        }

        private void goldCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            
            GoldIcon?.OnRender(new ImageButtonDrawingArgsFactory(new(goldCanvas.Width, goldCanvas.Height)).Create(sender, args));
        }
    }
}
