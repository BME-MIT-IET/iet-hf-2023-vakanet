using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Attacks
{
    internal class WandAttackShapeFactory : IAttackShapeFactory
    {
        private readonly Weapon weapon;

        public WandAttackShapeFactory(Weapon weapon)
        {
            this.weapon = weapon;
        }

        public IShape2D Create(Attack a)
        {
            var radius = 10;
            var resolution = 10;
            var shape = new AttackShapeBuilder()
                .PolygonShape(Polygon.CircleVertices(new(0, 0), radius, resolution))
                .Attack(a)
                .OwnerPosition(weapon.Owner.Position)
                .Rotated(true)
                .Create();
            return shape;
        }
    }
}
