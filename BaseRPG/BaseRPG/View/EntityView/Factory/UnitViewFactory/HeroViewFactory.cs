using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView.Factory.UnitViewFactory
{
    public class HeroViewCreationParams
    {
        private Hero hero;
        private IShape2D shape;
        private IImageProvider imageProvider;
        private string heroImage;

        public Hero Hero { get => hero; init => hero = value; }
        public IShape2D Shape { get => shape; init => shape = value; }
        public IImageProvider ImageProvider { get => imageProvider; init => imageProvider = value; }
        public string HeroImage { get => heroImage; init => heroImage = value; }
    }
    public class HeroViewFactory : IUnitViewFactory
    {

        private HeroViewCreationParams creationParams;
        public HeroViewFactory(HeroViewCreationParams creationParams)
        {
            this.creationParams = creationParams;
        }

        public UnitView Create()
        {
            var image = new DrawingImage(creationParams.HeroImage, creationParams.ImageProvider);
            var heroPositionObserver = new PositionObserver(
                () => PositionUnit2D.ToVector2D(creationParams.Hero.Position));
            var shapeView = new ShapeView(creationParams.Shape, heroPositionObserver);
            var idleAnimation = ImageSequenceAnimation
                .SingleImage(creationParams.ImageProvider, creationParams.HeroImage);
            return new UnitView.Builder(image, creationParams.Hero, shapeView)
                .IdleAnimation(idleAnimation)
                .WithFacingPointAnimation()
                .Build();
        }
    }
}
