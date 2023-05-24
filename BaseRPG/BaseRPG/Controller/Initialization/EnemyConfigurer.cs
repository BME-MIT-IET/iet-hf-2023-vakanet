using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization
{
    /// <summary>
    /// Connects the UnitView, the Shape and the model part of the enemy, and makes configurations across all 3 layers
    /// </summary>
    public class EnemyConfigurer
    {
        private readonly Enemy enemy;
        private IShape2D fullInRangeDetectorShape;
        public IShape2D FullInRangeDetectorShape => fullInRangeDetectorShape;

        public EnemyConfigurer(Enemy enemy) {
            this.enemy = enemy;
        }
        public EnemyConfigurer WithUnitView(UnitView unitView) {
            enemy.AttackableInRange += (a) =>
                unitView.StartAnimation("attack");
            return this;
        }
        public EnemyConfigurer Range(double range) {

            fullInRangeDetectorShape =
                Polygon.Circle(
                    enemy.InRangeDetector,
                    enemy.MovementManager,
                    new(0, 0), range
                );
            return this;
        }
    }
}
