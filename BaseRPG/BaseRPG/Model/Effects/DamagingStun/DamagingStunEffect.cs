using BaseRPG.Model.Interfaces;
using BaseRPG.Model.State.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.DamagingStun
{
    public class DamagingStunEffect : Effect
    {
        private readonly double durationSeconds;
        private bool activated = false;
        public override event Action OnCeaseToExist;

        public DamagingStunEffect(Unit caster, Unit target, double durationSeconds) : base(target)
        {
            this.durationSeconds = durationSeconds;
        }

        public override bool Exists {
            get {
                var result = SecondsSinceStarted < durationSeconds;
                if (!result) {
                    OnCeaseToExist?.Invoke();
                    OnCeaseToExist = null;
                }
                return result;
            } 
        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }

        public override void OnStep(double delta)
        {
            
        }
        public override void OnAddedToTarget()
        {
            base.OnAddedToTarget();
            activated = Target.SwitchMovementState(new BlockingMovementState());
            OnCeaseToExist += () => { if (activated) Target.BackToPreviousMovementState(); };
        }
    }
}
