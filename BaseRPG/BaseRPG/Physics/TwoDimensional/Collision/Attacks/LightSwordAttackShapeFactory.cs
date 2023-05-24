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
    public class LightSwordAttackShapeFactory:IAttackShapeFactory
    {
        private readonly Weapon weapon;

        public LightSwordAttackShapeFactory(Weapon weapon)
        {
            this.weapon = weapon;
        }


        public IShape2D Create(Attack a)
        {
            var shape = new AttackShapeBuilder()
                .PolygonShape(new List<Point2D> {
                    new(0,-50),
                    new(-100,30),
                    new(0,70),
                    new(100,30)
                })
                .Attack(a)
                .OwnerPosition(weapon.Owner.Position)
                .Rotated(true)
                .Create();
            return shape;
        }
    }
}
