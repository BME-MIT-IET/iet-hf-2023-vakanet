using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.CollisionDetectors;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.TransformAnimations;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace BaseRPG.Controller.Input.InputActions.Attack
{
    public class MeteorCreatingSkillOnReleaseInputAction : IInputAction
    {
        private readonly Unit unit;
        private readonly IPositionProvider mousePositionProvider;
        private readonly Controller controller;
        private readonly IImageProvider imageProvider;
        private readonly AnimationProvider animationProvider;
        private EmptyCollisionDetector shapeViewOwner;

        public MeteorCreatingSkillOnReleaseInputAction(
            Unit unit,
            IPositionProvider mousePositionProvider,
            Controller controller,
            IImageProvider imageProvider,
            AnimationProvider animationProvider
            )
        {
            this.unit = unit;
            this.mousePositionProvider = mousePositionProvider;
            this.controller = controller;
            this.imageProvider = imageProvider;
            this.animationProvider = animationProvider;
        }

        public void OnPressed()
        {

            if (!_checkLearnt()) return;
            // Add an empty shapeView to display what the skill is going to look like
            IMovementManager movementManager = new PhysicsFactory2D().CreateMovementManager();
            shapeViewOwner = new EmptyCollisionDetector();
            controller.AddView(
                new ShapeView(
                    Polygon.Circle(shapeViewOwner, movementManager, new Vector2D(0, 0), 300, 40),
                    mousePositionProvider, Color.FromArgb(100, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), 5
                )
            );
        }

        public void OnReleased()
        {
            
            if (!_checkLearnt()) return; 
            shapeViewOwner.Exists = false;

            var mousePositionNow = mousePositionProvider.Position;
            AnimationView animationView =
                new AnimationView.Builder(
                    new CustomAnimator(
                        new TranslationAnimation(
                            0.5f,
                            mousePositionProvider.Position + new Vector2D(-500, 500),
                            mousePositionProvider.Position),
                        ImageSequenceAnimation.LoopingAnimation(imageProvider, animationProvider.Get("meteor"), 0.05)
                        )
                    )
                    .WithFixPosition(mousePositionProvider.Position)
                    .TransformationCompletedCallback(
                        (a) => controller.QueueAction(() => unit.CastSkill<IPositionUnit>("meteor", new PositionUnit2D(mousePositionNow))))
                    .Create();
            controller.AddView( animationView, 1000);

        }
        private bool _checkLearnt() {
            lock (unit.SkillManager) return unit.SkillManager.IsLearnt("meteor");
        }

    }
}
