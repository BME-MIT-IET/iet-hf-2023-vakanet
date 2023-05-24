using BaseRPG.Model.Combat;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects.Invincibility
{
    public class InvincibilityEffect : Effect
    {
        private readonly double durationSeconds;
        private bool activated = false;

        public override event Action OnCeaseToExist;

        public InvincibilityEffect(Unit target, double durationSeconds) : base(target)
        {
            this.durationSeconds = durationSeconds;
            
        }
        private bool tryActivate()
        {
            return Target.SwitchDamageTakingState(new InvincibleDamageTakingState());
        }
        public override bool Exists
        {
            get
            {
                var result =  SecondsSinceStarted<durationSeconds;
                if (!result)
                {
                    OnCeaseToExist?.Invoke();
                    OnCeaseToExist = null;
                }
                return result;
            }
        }
        public override void OnAddedToTarget()
        {
            base.OnAddedToTarget();
            activated = tryActivate();
            OnCeaseToExist += () => { if (activated) Target.BackToPreviousDamageTakingState(); };
        }


        public override void OnStep(double delta)
        {

        }

        public override void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
