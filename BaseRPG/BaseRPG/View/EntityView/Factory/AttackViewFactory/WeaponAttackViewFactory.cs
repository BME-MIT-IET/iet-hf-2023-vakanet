using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView.Factory.AttackViewFactory
{
    public class WeaponAttackCreationParams
    {
        private bool rotated;
        private IPositionUnit ownerPosition;
        private Attack attack;

        public bool Rotated { get => rotated; init => rotated = value; }
        public IPositionUnit OwnerPosition { get => ownerPosition; init => ownerPosition = value; }
        public Attack Attack { get => attack; init => attack = value; }
    }
    public abstract class WeaponAttackViewFactory : IAttackViewFactory
    {
        private IImageProvider imageProvider;
        private WeaponAttackCreationParams creationParams;

        protected WeaponAttackViewFactory(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
        }
        public WeaponAttackCreationParams CreationParams { set => creationParams = value; }

        public AttackView Create()
        {
            return Create(creationParams,imageProvider);
        }
        protected abstract AttackView Create(WeaponAttackCreationParams creationParams, IImageProvider imageProvider);
    }
}
