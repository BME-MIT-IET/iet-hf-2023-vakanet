using BaseRPG.Model.Tickable.FightingEntity.Hero;
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

namespace BaseRPG.View.UIElements.ExperienceControl
{
    public sealed partial class ExperienceControl : UserControl
    {
        private double originalWidth;
        public ExperienceControl()
        {
            this.InitializeComponent();
            originalWidth = xpProgressBar.Width;
        }

        public Hero Hero { 
            set {
                xpProgressBar.Width = value.ExperienceManager.PercentageToNextLevel * originalWidth;
                value.OnXpChagedCallback(
                    (newXp, xpuntilNextLevel) => DispatcherQueue.TryEnqueue(() => xpProgressBar.Width = originalWidth * newXp/xpuntilNextLevel)
                );
            } 
        }
    }
}
