using BaseRPG.Model.Effects;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.WorldView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace BaseRPG.Controller.UnitControl
{
    public class ShapeViewPair
    {
        private IShape2D shape;
        private IDrawable view;

        public IDrawable View { get => view; set => view = value; }
        public IShape2D Shape { get => shape; set => shape = value; }

        public ShapeViewPair(IShape2D shape, IDrawable view)
        {
            Shape = shape;
            View = view;
        }
    }
}
