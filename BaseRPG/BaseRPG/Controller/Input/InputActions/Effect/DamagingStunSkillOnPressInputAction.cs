using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Effect
{
    public class DamagingStunSkillOnPressInputAction : IInputAction
    {
        private Unit caster;
        private readonly CollisionNotifier2D collisionNotifier2D;

        public DamagingStunSkillOnPressInputAction(Unit caster, CollisionNotifier2D collisionNotifier2D)
        {
            this.caster = caster;
            this.collisionNotifier2D = collisionNotifier2D;
        }

        public void OnPressed() {
            var shapes = collisionNotifier2D.ShapesCollidingWithTrackedPosition;
            if (shapes.Count == 0) return;
            LinkedList<Unit> targetableUnits = new LinkedList<Unit>();
            LinkedList<ICollisionDetector> targetableOthers = new LinkedList<ICollisionDetector>();
            foreach (var shape in shapes)
            {
                shape.Owner.SeletBySkillTargetability(targetableUnits,targetableOthers);
            }
            if(targetableUnits.First != null)
                caster.CastSkill("stun", new TargetedEffectParams(targetableUnits.First.Value));
        }
    }
}
