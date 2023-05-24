using BaseRPG.Model.Interfaces.Effect;
using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Skills
{
    public class EffectCreatingSkill<PARAM_TYPE> : IGenericSkill<PARAM_TYPE> where PARAM_TYPE : class
    {
        private IEffectFactory<PARAM_TYPE> effectFactory;
        public  EffectCreatingSkill(string name, IEffectFactory<PARAM_TYPE> effectFactory):base(name)
        {
            this.effectFactory = effectFactory;
        }

        protected override void Cast(PARAM_TYPE skillCastParams)
        {
            Effects.Effect effect = effectFactory.CreateEffect(skillCastParams);
            effect.Target.AddEffect(effect);
        }
    }
}
