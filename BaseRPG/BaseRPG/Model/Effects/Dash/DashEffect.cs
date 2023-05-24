using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.Dash
{
    public class DashEffect : Effect
    {
        private IMovementUnit movement;
        private double secondsToComplete;
        public DashEffect(Unit target, IMovementUnit movement, double timeToComplete) : base(target)
        {
            this.movement = movement;
            secondsToComplete = timeToComplete;
        }


        public override bool Exists
        {
            get
            {
                var exists = SecondsSinceStarted < secondsToComplete;
                if (!exists)
                {
                    OnCeaseToExist?.Invoke();
                    OnCeaseToExist = null;
                }
                return exists;
            }
        }

        public override event Action OnCeaseToExist;

        public override void OnStep(double delta)
        {
            var timePassedRelativeToMax = delta / secondsToComplete;
            var newMovement = movement.Scaled(timePassedRelativeToMax);
            Target.QueueMovement(newMovement);
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }

    }
}
