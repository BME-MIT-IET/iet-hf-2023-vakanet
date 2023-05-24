using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl
{
    public abstract class UnitControlBase
    {
        private Unit controlledUnit;

        public UnitControlBase(Unit controlledUnit)
        {
            this.controlledUnit = controlledUnit;
            controlledUnit.OnCeaseToExist += () =>
                this.controlledUnit = null;
        }

        public Unit ControlledUnit { get => controlledUnit; set => controlledUnit = value; }
        public Health ControlledUnitHealth => ControlledUnit.Health;
        public abstract IMovementUnit NextMovement(double delta);
        public void OnTick(double delta)
        {
            if (controlledUnit == null) return;
            IMovementUnit nextMovement = NextMovement(delta);
            if (nextMovement == null) return;
            ControlledUnit.QueueMovement(nextMovement);
        }
    }
    
}
