using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional.Collision.Ray;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    //public class Circle : IShape2D
    //{
    //    private ICollisionDetector owner;
    //    private IMovementManager movementManager;
    //    private Vector2D center;
    //    private double radius;


    //    public Circle(ICollisionDetector owner, IMovementManager movementManager, Vector2D center, double radius)
    //    {
    //        this.owner = owner;
    //        this.movementManager = movementManager;
    //        this.center = center;
    //        this.radius = radius;
    //    }

    //    public Vector2D Middle { get { return center; } }


    //    public Vector2D GlobalPosition => new(movementManager.Position.Values[0], movementManager.Position.Values[1]);

    //    public IMovementManager MovementManager { get => movementManager; set => movementManager = value; }
    //    public ICollisionDetector Owner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //    public IShape2D ShiftedByPos => throw new NotImplementedException();

    //    public Vector2D LastCalculatedMiddle => throw new NotImplementedException();

    //    private double _distanceFrom(Vector2D point) {
    //        return (point - center).Length;
    //    }
    //    public bool CollidesWith(Vector2D point)
    //    {
    //        return (_distanceFrom(point) <= radius);
    //    }

    //    public bool IsColliding(IShape2D r2)
    //    {
    //        return ToPolygon().IsColliding(r2);
    //    }

    //    public void Rotate(double angle)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IShape2D Shifted(Vector2D shift)
    //    {
    //        return Shifted(shift.X, shift.Y);
    //    }

    //    public IShape2D Shifted(params double[] values)
    //    {
    //        return ToPolygon().Shifted(values);
    //        //return new Circle(Owner, movementManager.Copy(), new(center.X + values[0], center.Y + values[1]),radius);
    //    }

    //    public Polygon2D ToPolygon2D()
    //    {
    //        return ToPolygon2D(20);
    //    }
    //    public Polygon2D ToPolygon2D(int numberOfVertices)
    //    {
    //        double step = (Math.PI * 2) / numberOfVertices;
    //        List<Point2D> vertices = new();
    //        for (double angle = 0; angle < (Math.PI * 2); angle += step)
    //        {
    //            vertices.Add(Point2D.FromPolar(radius,Angle.FromRadians(angle)));

    //        }
    //        return new Polygon2D(vertices);
    //    }

    //    public bool IsCollidingCircle(Circle circleSector)
    //    {
    //        return (_distanceFrom(circleSector.center) <= radius + circleSector.radius);
    //    }

    //    public Polygon ToPolygon(int numberOfVertices)
    //    {
    //        return new Polygon(owner, movementManager, ToPolygon2D(numberOfVertices).Vertices);
    //    }

    //    public Polygon ToPolygon()
    //    {
    //        return ToPolygon(20);
    //    }

    //    public bool IsCollidingPoint(Vector2D point)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public RayCollection CastRays(Vector2D movementVector, int v)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Polygon Rotated(double angle)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool IsColliding(Polygon2D polygon2)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
