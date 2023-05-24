using BaseRPG.Model.Exceptions;
using BaseRPG.Model.Interfaces.Combat.Attack;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Worlds.InteractionPoints;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Health;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.ItemView;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl
{
    public enum MoveDirection
    {
        Forward, Backward, Left, Right
    }

    public class PlayerControlCreationParams
    {
        private DirectionMovementUnitMapper directionVectorMapper;
        private List<IMovementUnit> movements = new List<IMovementUnit>();
        private IPositionProvider mousePositionProvider;
        private Vector2D mouseOnCharge;
        private readonly Hero controlledUnitAsHero;
        private readonly IDrawableProvider drawableProvider;

    }
    public class PlayerControl:UnitControlBase
    {
        private DirectionMovementUnitMapper directionVectorMapper;
        private List<IMovementUnit> movements = new List<IMovementUnit>();
        private IPositionProvider mousePositionProvider;
        private Vector2D mouseOnCharge;
        private readonly Hero controlledUnitAsHero;
        private readonly IDrawableProvider drawableProvider;
        public IPositionProvider MousePositionProvider { set => mousePositionProvider = value; }
        public Hero ControlledUnitAsHero => controlledUnitAsHero;

        public PlayerControl( Hero controlledUnitAsHero, IDrawableProvider drawableProvider):base(controlledUnitAsHero)
        {
            directionVectorMapper = DirectionMovementUnitMapper.CreateDefault2D();
            this.controlledUnitAsHero = controlledUnitAsHero;
            this.drawableProvider = drawableProvider;
        }

        internal void BuyFromShop(Shop shop, int index)
        {
            controlledUnitAsHero.Buy(shop, index);
        }
        public void OnMove(MoveDirection moveDirection)
        {
            IMovementUnit movement = directionVectorMapper.FromDirection(moveDirection);
            movements.Add(movement);

        }
        public void LightAttack()
        {
            if (ControlledUnit == null) return;
            try
            {
                IAttackFactory attackFactory = ControlledUnit.AttackFactory("light");
                if (attackFactory != null)
                    drawableProvider
                        .GetDrawable<EquippedWeaponView>(controlledUnitAsHero.EquippedWeapon, "equipped")
                        .StartLightAttackAnimation(attackFactory);

            }
            catch (NoSuchAttackBuilderException e)
            {
                //not a problem, ignore
            }
           
        }
        public void HeavyAttackChargeStart()
        {
            mouseOnCharge = mousePositionProvider.Position;
            drawableProvider
                .GetDrawable<EquippedWeaponView>(controlledUnitAsHero.EquippedWeapon, "equipped")
                .StartHeavyAttackChargeAnimation();
        }
        public void HeavyAttackRelease()
        {
            if (ControlledUnit == null) return;
            var itemView = drawableProvider
                        .GetDrawable<EquippedWeaponView>(controlledUnitAsHero.EquippedWeapon, "equipped");
            try
            {
                IAttackFactory attackFactory = ControlledUnit.AttackFactory("heavy");
                if (attackFactory != null)
                {
                    
                    itemView.HeavyAttackStrikeAnimationFactory.MousePositionOnScreen = mouseOnCharge;
                    itemView.StartHeavyAttackStrikeAnimation(attackFactory);
                }
            }
            catch (NoSuchAttackBuilderException e)
            {
                //not a problem, ignore
            }
            catch (AttackNotFullyChargedException ex) {
                itemView.CancelHeavyChargeAnimation();
            }
        }
        public void HeavyAttackChargeHold(double delta) {
            ControlledUnitAsHero.OnChargeHold(delta);
        }

        public override IMovementUnit NextMovement(double delta)
        {
            if (movements.Count == 0) 
                return null;
            IMovementUnit movementUnit = IMovementUnit.Unite(movements);
            movements.Clear();
            return movementUnit.Scaled(200*delta);
        }
    }
}
