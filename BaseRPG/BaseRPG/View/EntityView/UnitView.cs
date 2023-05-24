using BaseRPG.Controller;
using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.Animators;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.EntityView.Health;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI;

namespace BaseRPG.View.EntityView
{
    public class UnitView : IDrawable
    {
        private Unit unit;
        private HealthView healthView;
        private Animator animator;
        private readonly Dictionary<string, ImageSequenceAnimation> animations;
        private ShapeView shapeView;
        private UnitView(Unit unit, HealthView healthView, Animator animator, Dictionary<string, ImageSequenceAnimation> animations, ShapeView shapeView)
        {
            this.unit = unit;
            this.healthView = healthView;
            this.animator = animator;
            this.animations = animations;
            this.shapeView = shapeView;
        }

        public Vector2D ObservedPosition => new(unit.Position.Values[0], unit.Position.Values[1]);

        public bool Exists => unit.Exists;

        public Animator Animator { get => animator; }
        public HealthView HealthView { get => healthView; }

        public void OnRender(DrawingArgs drawingArgs)
        {
            
            Animator.Animate(drawingArgs);
            var size = animator.Size;
            if (shapeView.MouseOver(drawingArgs))
                shapeView.OnRender(drawingArgs);
            drawingArgs.PositionOnScreen -= 
                new Vector2D(
                    size.Item1 / 2,
                    size.Item2 / 1.2);
            HealthView.Render(drawingArgs);
            
        }
        internal void StartAnimation(string v)
        {
            animator.Start(animations[v]);
        }
        public class Builder {

            private Color defaultHealthColor = Color.FromArgb(255,255,255,255);
            private readonly DrawingImage image;
            private Unit unit;
            private readonly ShapeView shapeView;
            private Animator animator;
            private Dictionary<string, ImageSequenceAnimation> animations = new();

            public Builder(DrawingImage image, Unit unit,ShapeView shapeView)
            {
                this.image = image;
                this.unit = unit;
                this.shapeView = shapeView;
            }
                
            private Color _calculateHealthColor(Unit unit) {
                if (unit.OffensiveGroup == AttackabilityService.Group.Enemy)
                    return Color.FromArgb(255, 200, 0, 0);
                if (unit.OffensiveGroup == AttackabilityService.Group.Friendly)
                    return Color.FromArgb(255, 0, 150, 200);
                return defaultHealthColor;
            }
            public Builder Animator(Animator animator) {
                if (this.animator != null) throw new ParameterAlreadyDefined("The animator parameter is already defined");
                this.animator = animator;
                return this;
            }
            public Builder IdleTransformationAnimation(TransformationAnimation2D animation2D) {
                animator = new CustomAnimator(animation2D, animations["idle"]);
                return this;
            }
            public Builder IdleAnimation(ImageSequenceAnimation animation) {
                return Animation("idle", animation);
            }
            public Builder Animation(string name, ImageSequenceAnimation animation) {
                animations.Add(name, animation);
                return this;
            }
            public Builder WithFacingPointAnimation() {
                if (animator != null) throw new ParameterAlreadyDefined("The animator parameter is already defined");
                animator = new CustomAnimator(
                    new FacingPointOnCallbackAnimation(0,
                        PositionObserver.CreateForLastMovement(unit.MovementManager,GameConfigurer.VERY_LARGE_NUMBER)
                        ),
                    animations["idle"]
                );
                return this;
            }
            public UnitView Build() {
                if (animator == null) throw new RequiredParameterMissing("The animator parameter is missing!");
                HealthView healthView = 
                    new(unit.Health,
                    (float)image.Size.Item1,
                    _calculateHealthColor(unit));
                
                return new UnitView(unit, healthView, animator, animations, shapeView); ;
            }
        }

        
    }
}
