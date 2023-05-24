using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.View.Image;
using BaseRPG.View.UIElements.CustomControl;
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

namespace BaseRPG.View.UIElements.Spell
{
    public sealed partial class SingleSpellUI : UserControl
    {
        private readonly Skill skill;
        private readonly SpellControl spellControl;

        //public string SpellName { get; init; }
        public ButtonWithCanvas Button => button;
        public Size CanvasSize => new((float)button.Canvas.Width,(float)button.Canvas.Height);
        public SingleSpellUI(Skill skill,SpellControl spellControl, DrawingImage drawingImage)
        {
            this.InitializeComponent();
            button.Drawable = drawingImage;
            button.ButtonClick += (o,a)=> _learnSpell();
            button.CornerRadius = new CornerRadius();
            text.Text = skill.Name.ToUpper();
            this.skill = skill;
            this.spellControl = spellControl;
        }
        private void _learnSpell() {
            button.Button.IsEnabled = !spellControl.LearnSpell(skill);
        }
    }
}
