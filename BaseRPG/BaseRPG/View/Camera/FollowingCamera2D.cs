using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BaseRPG.View.Camera
{
    public class FollowingCamera2D:Camera2D
    {
        private IMovementManager observedPosition;
        private LinkedList<Vector2D> lastPositions = new ();

        public Unit FollowedUnit { set {
                FollowedPosition = value.MovementManager;
            } }
        public IMovementManager FollowedPosition {
            set {
                observedPosition = value;
                observedPosition.Moved += s => Update();
            } }
 
        public FollowingCamera2D(Vector2D position, Size size):base(position, size)
        {
            
            for (int i = 0; i < 10; i++)
            {
                lastPositions.AddLast(position);
            }
        }
        public override void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.OnCanvasSizeChanged(sender, e);
        }
        public override void Update() {
            base.Update();
            MiddlePosition = PositionUnit2D.ToVector2D(observedPosition.Position);
            //    lastPositions.First.Value;
            //lastPositions.RemoveFirst();
            //lastPositions.AddLast(PositionUnit2D.ToVector2D(observedPosition.Position));
        }
    }
} 
