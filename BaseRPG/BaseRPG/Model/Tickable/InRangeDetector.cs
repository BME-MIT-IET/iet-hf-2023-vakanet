using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable
{
    public class InRangeDetector : GameObject, ICollisionDetector
    {
        public event Action<ICollisionDetector> OnInRange;
        public event Action<ICollisionDetector> OnExitedRange;
        public override event Action OnCeaseToExist;

        public List<ICollisionDetector> objectsInRange = new();

        public InRangeDetector(World currentWorld) : base(currentWorld)
        {
        }

        private bool exists = true;
        // Exists for the duration of its owner
        public override bool Exists { get { return exists; }}

        public bool CanBeOver => true;

        public void SetExists(bool value) {
            exists = value;
        }
        public void OnCollision(ICollisionDetector gameObject, double delta)
        {
            if (!objectsInRange.Contains(gameObject))
            {
                gameObject.OnCeaseToExist += () => objectsInRange.Remove(gameObject);
                objectsInRange.Add(gameObject);
            }
            OnInRange?.Invoke(gameObject);
        }

        public override void Step(double delta)
        {
            objectsInRange.RemoveAll(o => !o.Exists);
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
        public bool IsInRange(object obj) {
            return objectsInRange.Contains(obj);
        }

        public void OnCollisionExit(ICollisionDetector gameObject)
        {
            objectsInRange.Remove(gameObject);
            OnExitedRange?.Invoke(gameObject);
        }

        public bool CanCollide(ICollisionDetector other)
        {
            return true;
        }

        //public void CanCollide(ICollisionDetector other)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
