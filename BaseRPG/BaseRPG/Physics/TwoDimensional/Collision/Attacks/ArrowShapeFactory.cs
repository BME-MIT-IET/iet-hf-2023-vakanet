using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Attacks
{
    internal class ArrowShapeFactory : IAttackShapeFactory
    {
        private readonly Weapon weapon;

        public ArrowShapeFactory(Weapon weapon)
        {
            this.weapon = weapon;
        }

        public IShape2D Create(Attack a)
        {
            var shape = new AttackShapeBuilder()
                .PolygonShape(Polygon.RectangleVertices(new(0, 0), 15, 35))
                .Attack(a)
                .OwnerPosition(weapon.Owner.Position)
                .Rotated(true)
                .Create();
            return shape;
        }
    }
}
