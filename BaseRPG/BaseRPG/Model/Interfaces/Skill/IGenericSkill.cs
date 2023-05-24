using BaseRPG.Model.Exceptions;
using BaseRPG.Model.Utility;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Skill
{
    public abstract class IGenericSkill<PARAM_TYPE> : Skill where PARAM_TYPE : class 
    {
        protected IGenericSkill(string name) : base(name)
        {
        }

        public override void Cast(object skillCastParams)
        {
            PARAM_TYPE @params = skillCastParams as PARAM_TYPE;
            if (@params == null) 
                throw new InvalidSkillCastParamsException(skillCastParams.GetType(),typeof(PARAM_TYPE));
            Cast(@params);
        }
        protected abstract void Cast(PARAM_TYPE skillCastParams);
    }
}
