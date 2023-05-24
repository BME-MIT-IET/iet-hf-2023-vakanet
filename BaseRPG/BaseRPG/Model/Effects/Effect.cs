using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects
{
    public abstract class Effect : GameObject
    {
        private Unit target;
        private double secondsSinceStarted;
        protected double SecondsSinceStarted => secondsSinceStarted;
        public Unit Target => target;

        public virtual void OnAddedToTarget()
        {
        }

        protected Effect(Unit target) : base(target.CurrentWorld)
        {
            this.target = target;
            this.secondsSinceStarted = 0;
        }

        public override void Step(double delta)
        {
            OnStep(delta);
            secondsSinceStarted += delta;
        }

        public abstract void OnStep(double delta);
    }
}
