using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Effect
{
    internal class DashSkillOnPressInputAction : IInputAction
    {
        private readonly Unit unit;
        private IPositionProvider mousePositionProvider;
        private readonly int skillIndex;

        public DashSkillOnPressInputAction(Unit unit, IPositionProvider mousePositionProvider, int skillIndex)
        {
            this.unit = unit;
            this.mousePositionProvider = mousePositionProvider;
            this.skillIndex = skillIndex;
        }

        public void OnPressed()
        {
            if (mousePositionProvider != null)
            {
                var point = mousePositionProvider.Position - new Vector2D(unit.Position.Values[0], unit.Position.Values[1]);
                DashEffectCreationParams skillCastParams = new DashEffectCreationParams(new MovementUnit2D(point), unit);
                unit.CastSkill("dash", skillCastParams);
            }
        }


    }
}
