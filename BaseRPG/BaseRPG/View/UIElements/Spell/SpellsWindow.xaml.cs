using BaseRPG.Controller.UnitControl;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.UIElements.DrawingArgsFactory;
using BaseRPG.View.UIElements.Initialization;
using Microsoft.UI.Xaml;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
//Assets\image\icons\spell\invincibility-outlined.png
//Assets\image\icons\spell\meteor.png
//Assets\image\icons\spell\stun.png
//Assets\image\icons\spell\dash.png
namespace BaseRPG.View.UIElements.Spell
{
    public sealed partial class SpellsWindow : CustomWindow
    {
        private IImageProvider imageProvider;
        private readonly SpellControl spellControl;
        private List<SingleSpellUI> spellMapping = new();
        public SpellsWindow(IImageProvider imageProvider, SpellControl spellControl)
        {
            this.InitializeComponent();
            this.imageProvider = imageProvider;
            this.spellControl = spellControl;
            Update();
            spellControl.SkillManager.SkillPointsChanged += (points) => DispatcherQueue.TryEnqueue(Update);
            _fillSpellMapping();
            new GridFillStrategy().Fill(spellsGrid, CreateSpell, 2, 4);
            foreach (var def in spellsGrid.ColumnDefinitions) { 
                def.Width = GridLength.Auto;
            }
           
        }
        public void Update() {
            this.skillPointsText.Text = spellControl.SkillManager.SkillPoints.ToString();
        }
        private void _fillSpellMapping()
        {
            spellMapping.Add(new SingleSpellUI(spellControl.GetSpellByName("meteor"),spellControl,
                new DrawingImage(@"Assets\image\icons\spell\meteor.png", imageProvider)));

            spellMapping.Add(new SingleSpellUI(spellControl.GetSpellByName("invincibility"), spellControl,
                new DrawingImage(@"Assets\image\icons\spell\invincibility-outlined.png", imageProvider)));

            spellMapping.Add(new SingleSpellUI(spellControl.GetSpellByName("stun"), spellControl,
                new DrawingImage(@"Assets\image\icons\spell\stun.png", imageProvider)));

            spellMapping.Add(new SingleSpellUI(spellControl.GetSpellByName("dash"), spellControl,
                new DrawingImage(@"Assets\image\icons\spell\dash.png", imageProvider)));
        }

        private SingleSpellUI CreateSpell(int index) {
            var spellUI = spellMapping[index];
            spellUI.Button.DrawingArgsFactory = new ImageButtonDrawingArgsFactory(spellUI.CanvasSize);

            return spellUI;
        }
        public static string WindowName => "spells";
        
    }
}
