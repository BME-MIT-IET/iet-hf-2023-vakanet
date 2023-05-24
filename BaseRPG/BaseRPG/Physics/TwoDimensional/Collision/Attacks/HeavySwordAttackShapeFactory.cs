using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Attacks
{
    public class HeavySwordAttackShapeFactory : IAttackShapeFactory
    {
        private readonly Weapon weapon;
        private static readonly Angle beginAngle = Angle.FromDegrees(-25);
        private static readonly Angle endAngle = Angle.FromDegrees(220);
        private IReadOnlyList<Point2D> vertices = Polygon.CircleVertices(new(0, -App.IMAGE_SCALE * 10), App.IMAGE_SCALE * 42, beginAngle, endAngle);
        public HeavySwordAttackShapeFactory(Weapon weapon)
        {
            this.weapon = weapon;
        }


        public IShape2D Create(Attack a)
        {
            var shape = new AttackShapeBuilder()
                .PolygonShape(vertices)
                .Attack(a)
                .OwnerPosition(weapon.Owner.Position)
                .Rotated(true)
                .Create();
            return shape;
        }
    }
}
