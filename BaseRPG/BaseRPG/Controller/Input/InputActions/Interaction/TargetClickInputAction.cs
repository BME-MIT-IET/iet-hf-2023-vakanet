using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds.InteractionPoints;
using BaseRPG.Physics.TwoDimensional.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Interaction
{
    internal class TargetClickInputAction:IInputAction
    {
        private readonly CollisionNotifier2D collisionNotifier2D;
        private readonly Hero interactionStarter;

        public TargetClickInputAction(CollisionNotifier2D collisionNotifier2D, Hero interactionStarter)
        {
            this.collisionNotifier2D = collisionNotifier2D;
            this.interactionStarter = interactionStarter;
        }

        public void OnPressed() {
            var shapes = collisionNotifier2D.ShapesCollidingWithTrackedPosition;
            LinkedList<GameObject> targetableGameObjects = new LinkedList<GameObject>();
            LinkedList<ICollisionDetector> targetableOthers = new LinkedList<ICollisionDetector>();
            foreach (var shape in shapes)
            {
                shape.Owner.SeletByInteractionTargetability(targetableGameObjects, targetableOthers);
            }
            if (targetableGameObjects.First != null)
                targetableGameObjects.First.Value.InteractWith(interactionStarter);
        }
    }
}
