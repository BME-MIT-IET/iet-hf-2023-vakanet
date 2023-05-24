using BaseRPG.Controller.Initialization;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Collision.Attacks;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.EntityView.Factory.AttackViewFactory;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;

namespace BaseRPG.View.EntityView.Factory.UnitViewFactory
{
    public class SlimeViewCreationParams
    {
        private string image;
        private Controller.Controller controller;
        private Enemy enemy;
        private IImageProvider imageProvider;
        private IShape2D shape;
        private IEnumerator<string> enumerator;
        private double timeBetweenFrames;


        public string Image { get => image; init => image = value; }
        public Controller.Controller Controller { get => controller; init => controller = value; }
        public Enemy Enemy { get => enemy; init => enemy = value; }
        public IImageProvider ImageProvider { get => imageProvider; init => imageProvider = value; }
        public IShape2D Shape { get => shape; init => shape = value; }
        public IEnumerator<string> Enumerator { get => enumerator; }
        public List<string> AttackAnimation
        {
            init
            {
                List<string>.Enumerator e = value.GetEnumerator();
                e.MoveNext();
                enumerator = e;
            }
        }
        public double TimeBetweenFrames { get => timeBetweenFrames; init => timeBetweenFrames = value; }

    }
    public class SlimeViewFactory : IUnitViewFactory
    {

        private SlimeViewCreationParams creationParams;

        public SlimeViewFactory(SlimeViewCreationParams slimeViewCreationParams)
        {
            creationParams = slimeViewCreationParams;
        }

        public UnitView Create()
        {
            if (isAnyNull(creationParams)) throw new ArgumentNullException("A parameter was null");
            var drawingImage = new DrawingImage(creationParams.Image, creationParams.ImageProvider);
            var shapeView = createShapeView();
            var idleAnimation = createIdleAnimation();
            var attackAnimation = createAttackAnimation();
            return new UnitView.Builder(drawingImage, creationParams.Enemy, shapeView)
                .IdleAnimation(idleAnimation)
                .Animation("attack", attackAnimation)
                .WithFacingPointAnimation()
                .Build();
        }
        private bool isAnyNull(SlimeViewCreationParams slimeViewCreationParams)
        {
            var result = false;
            if (slimeViewCreationParams.Image == null) result = true;
            if (slimeViewCreationParams.Controller == null) result = true;
            if (slimeViewCreationParams.Enemy == null) result = true;
            if (slimeViewCreationParams.ImageProvider == null) result = true;
            if (slimeViewCreationParams.Shape == null) result = true;
            if (slimeViewCreationParams.Enumerator == null) result = true;
            return result;
        }
        private ShapeView createShapeView() {
            var enemyPositionObserver = 
                new PositionObserver(() => PositionUnit2D.ToVector2D(creationParams.Enemy.Position));
            return new ShapeView(creationParams.Shape, enemyPositionObserver);
        }
        private ImageSequenceAnimation createIdleAnimation() {
            return ImageSequenceAnimation
                    .SingleImage(creationParams.ImageProvider, creationParams.Image);
        }
        private ImageSequenceAnimation createAttackAnimation() {
            return new ImageSequenceAnimation(
                creationParams.ImageProvider,
                creationParams.Enumerator,
                onAnimationCompleted,
                creationParams.TimeBetweenFrames
                );
        }
        private void onAnimationCompleted(ImageSequenceAnimation a)
        {
            creationParams.Controller.QueueAction(() =>
            {
                var attack = creationParams.Enemy.Attack("normal");
                if (attack != null)
                    addAttackViewAndShape(attack);
            });

        }
        private void addAttackViewAndShape(Attack a)
        {
            var shape = new AttackShapeBuilder(a)
                .PolygonShape(Polygon.RectangleVertices(new(0, 0), 120, 80))
                .Rotated(true)
                .OwnerPosition(creationParams.Enemy.Position)
                .Create();
            var view = new EnemyAttackViewFactory(a,creationParams.ImageProvider,creationParams.Enemy)
                .Create();
            creationParams.Controller.AddVisibleInstantly(new(shape, view));
        }
    }
}
