using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Effects
{
    public class EffectManager:ITickable
    {
        private List<Effect> effects = new();
        public void AddEffect(Effect effect) {
            lock (this)
            {
                effects.Add(effect);
                effect.OnAddedToTarget();
            }
                
        }
        public void OnTick(double delta)
        {
            lock (this) {
                //foreach (Effect effect in effects)
                //{
                //    effect.OnTick(delta);
                //}
                effects.RemoveAll(e => !e.Exists);
            }
        }
    }
}
