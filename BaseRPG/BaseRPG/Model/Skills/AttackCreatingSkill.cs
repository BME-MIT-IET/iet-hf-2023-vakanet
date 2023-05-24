using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.Model.Tickable.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Skills
{
    public class AttackCreatingSkill : IGenericSkill<IPositionUnit>
    {
        private AttackBuilder attackBuilder;

        public AttackCreatingSkill(string name,AttackBuilder attackBuilder, Action<Attack> attackCreationCallback):base(name)
        {
            this.attackBuilder = attackBuilder;
            this.attackBuilder.CreatedEvent += attackCreationCallback;
        }


        protected override void Cast(IPositionUnit position)
        {
            attackBuilder.CreateAttack(position);
        }
    }
}
