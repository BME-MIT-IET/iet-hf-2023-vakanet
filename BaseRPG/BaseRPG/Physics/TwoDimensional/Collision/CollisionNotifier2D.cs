using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public class CollisionNotifier2D : ICollisionNotifier
    {
        private List<IShape2D> collisionObjects = new List<IShape2D>();
        private List<Collision> collisions = new List<Collision>();
        public CollisionNotifier2D() { }
        /// <summary>
        /// Stores all of the shapes that collide with the given position provider's position
        /// </summary>
        private IPositionProvider positionProvider;
        private List<IShape2D> shapesCollidingWithTrackedPosition = new();

        public List<IShape2D> ShapesCollidingWithTrackedPosition
        {
            get =>
                shapesCollidingWithTrackedPosition;
        }
        public List<IShape2D> Shapes => collisionObjects;
        //if any 2 collisionObjects are colliding, invoke the CollisionOccured event
        public void NotifyCollisions(double delta)
        {

            UpdateShapes();
            foreach (IShape2D shape1 in collisionObjects)
            {
                foreach (IShape2D shape2 in collisionObjects)
                {
                    if (shape1 == shape2) continue;
                    var shiftedShape1 = shape1.ShiftedByPos;
                    var shiftedShape2 = shape2.ShiftedByPos;
                    if (shiftedShape1.IsColliding(shiftedShape2))
                    {
                        if (!CollisionExists(shiftedShape1.Owner, shiftedShape2.Owner))
                            collisions.Add(new Collision(shape1, shape2));
                        shape1.OnCollision(shape2, delta);
                    }
                }
            }

            collisions.RemoveAll(c => !c.CheckColliding());
            collisionObjects.RemoveAll(s => !s.Owner.Exists);
        }

        public void AddToObservedShapes(IShape2D shape)
        {
            collisionObjects.Add(shape);
        }

        private List<IShape2D> ShapesCollidingWith(Vector2D point)
        {
            List<IShape2D> result = new();
            foreach (var shape in collisionObjects)
            {
                if (shape.ShiftedByPos.IsCollidingPoint(point)) result.Add(shape);
            }
            return result;
        }

        public List<IShape2D> ShapesCollidingWith(Polygon2D shape) {
            List<IShape2D> result = new();
            foreach (var s in collisionObjects)
            {
                if (s.ShiftedByPos.IsColliding(shape))
                    result.Add(s);
            }
            return result;
        }
        public List<IShape2D> ShapesCollidingWith(IShape2D shape)
        {
            return ShapesCollidingWith(shape.ToPolygon2D());
        }
        private void UpdateShapes()
        {
            if (positionProvider == null)
            {
                shapesCollidingWithTrackedPosition.Clear();
                return;
            }
            shapesCollidingWithTrackedPosition = ShapesCollidingWith(positionProvider.Position);
        }
        public bool CollisionExists(ICollisionDetector g1, ICollisionDetector g2)
        {
            foreach (var col in collisions)
            {
                if (col.Shape1.Owner == g1 && col.Shape2.Owner == g2) return true;
                if (col.Shape1.Owner == g2 && col.Shape2.Owner == g1) return true;
            }
            return false;
        }

        public void KeepTrackOf(IPositionProvider positionProvider)
        {
            this.positionProvider = positionProvider;
        }
        private class Collision
        {
            public Collision(IShape2D shape1, IShape2D shape2)
            {
                Shape1 = shape1;
                Shape2 = shape2;
            }

            public IShape2D Shape1 { get; set; }
            public IShape2D Shape2 { get; set; }
            public bool CheckColliding()
            {
                var shiftedShape1 = Shape1.ShiftedByPos;
                var shiftedShape2 = Shape2.ShiftedByPos;
                var result = shiftedShape1.IsColliding(shiftedShape2);
                if (!Shape1.Owner.Exists) result = false;
                if (!Shape2.Owner.Exists) result = false;
                if (!result)
                {
                    Shape1.Owner.OnCollisionExit(Shape2.Owner);
                    Shape2.Owner.OnCollisionExit(Shape1.Owner);
                }
                return result;
            }
        }
    }
}
