using BaseRPG.Model.Interfaces.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Skills
{
    public abstract class SkillNoParams : Skill
    {
        protected SkillNoParams(string name) : base(name)
        {
        }

        public override void Cast(object param)
        {
            Cast();
        }
        public abstract void Cast();
    }
}
