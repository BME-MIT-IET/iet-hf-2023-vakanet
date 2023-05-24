using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Effect
{
    public class InvincibilitySkillOnPressInputAction:IInputAction
    {
        private readonly Unit unit;
        private readonly int skillIndex;

        public InvincibilitySkillOnPressInputAction(Unit unit, int skillIndex)
        {
            this.unit = unit;
            this.skillIndex = skillIndex;
        }
        public void OnPressed()
        {
            TargetedEffectParams targetedEffectParams = new TargetedEffectParams(unit);
            unit.CastSkill("invincibility", targetedEffectParams);
        }

    }
}
