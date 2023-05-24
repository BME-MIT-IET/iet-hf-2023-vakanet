using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Effect
{
    public interface IEffectFactory<PARAM_TYPE> where PARAM_TYPE : class
    {
        event Action<Effects.Effect> EffectCreated;
        Effects.Effect CreateEffect( PARAM_TYPE effectCreationParams);
    }
}
