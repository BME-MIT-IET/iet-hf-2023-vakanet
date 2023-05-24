using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Attack
{
    public class AttackCreatingSkillOnPressInputAction : IInputAction
    {
        private readonly Unit unit;
        private readonly string skillName;
        private readonly IPositionProvider mousePositionProvider;
        private readonly Controller controller;

        public AttackCreatingSkillOnPressInputAction(Unit unit, string skillName, IPositionProvider mousePositionProvider, Controller controller)
        {
            this.unit = unit;
            this.skillName = skillName;
            this.mousePositionProvider = mousePositionProvider;
            this.controller = controller;
        }
        public void OnPressed()
        {
            controller.QueueAction(() => unit.CastSkill<IPositionUnit>(skillName, new PositionUnit2D(mousePositionProvider.Position)));
        }
    }
}
