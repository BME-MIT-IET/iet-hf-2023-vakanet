using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity;
using System.Collections.Generic;

namespace BaseRPG.Model.Interfaces.Collision
{
    public interface ICollisionDetector:IExisting
    {
        void OnCollision(ICollisionDetector other, double delta);
        virtual void OnCollisionExit(ICollisionDetector gameObject) { }
        bool CanCollide(ICollisionDetector other);
        bool CanBeOver { get => true; }
        void SeletBySkillTargetability(LinkedList<Unit> targetableUnits, LinkedList<ICollisionDetector> targetableOther) { }
        void SeletByInteractionTargetability(LinkedList<GameObject> targetableGameObjects, LinkedList<ICollisionDetector> targetableOther) { }
    }
}
