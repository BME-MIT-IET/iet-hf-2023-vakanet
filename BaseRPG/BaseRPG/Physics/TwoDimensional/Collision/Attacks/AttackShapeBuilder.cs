using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision.Attacks
{
    public class AttackShapeBuilder
    {
        private Attack2DBuilderHelper helper;
        private IEnumerable<Point2D> vertices;
        
        public AttackShapeBuilder(Attack attack = null)
        {
            this.helper = new(attack);
        }
        public AttackShapeBuilder Attack(Attack attack)
        {
            helper.Attack = attack;
            return this;
        }
        public AttackShapeBuilder OwnerPosition(IPositionUnit ownerPosition)
        {
            helper.OwnerPosition = ownerPosition;
            return this;
        }

        public AttackShapeBuilder PolygonShape(IEnumerable<Point2D> vertices)
        {
            this.vertices = vertices;
            return this;
        }
        public AttackShapeBuilder Rotated(bool rotated)
        {
            helper.Rotated = rotated;
            return this;
        }
        public IShape2D Create()
        {
            var initialRotation = helper.calculateInitialRotaion();
            var shape = new Polygon(helper.Attack, helper.MovementManager, vertices);
            shape.Rotate(initialRotation - Math.PI / 2);
            return shape;
        }
    }
}
