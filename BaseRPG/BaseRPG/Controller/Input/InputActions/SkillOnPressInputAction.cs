using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions
{
    public class SkillOnPressInputAction:IInputAction
    {
        private readonly Unit unit;
        private readonly string skillName;

        public SkillOnPressInputAction(Unit unit, string skillName)
        {
            this.unit = unit;
            this.skillName = skillName;
        }
        public void OnPressed()
        {
            unit.CastSkill(skillName, new object());
        }

    }
}
