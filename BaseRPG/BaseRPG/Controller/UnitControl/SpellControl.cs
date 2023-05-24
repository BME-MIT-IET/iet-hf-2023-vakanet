using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.Model.Interfaces.Utility;
using BaseRPG.Model.Skills;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl
{
    public class SpellControl
    {
        private SkillManager skillManager;
        private readonly IDrawableProvider drawableProvider;
        private readonly ICanQueueFunc<bool> boolDispatcher;

        public SkillManager SkillManager { get => skillManager;}

        public SpellControl(SkillManager skillManager, IDrawableProvider drawableProvider, ICanQueueFunc<bool> boolDispatcher)
        {
            this.skillManager = skillManager;
            this.drawableProvider = drawableProvider;
            this.boolDispatcher = boolDispatcher;
        }

        public Skill GetSpellByName(string name) {
            return skillManager.GetByName(name);
        }

        internal bool LearnSpell(Skill skill)
        {
            bool? result = null;
            boolDispatcher.QueueWithResult(() => skillManager.Learn(skill), r => result = r);
            var timeOut = 2000;
            Thread.Sleep(10);
            do
            {
                if(result.HasValue) 
                    return result.Value;
                Thread.Sleep(100);
                timeOut -= 100;
            } while (result == null && timeOut > 0);
            if(Debugger.IsAttached)
                return false;
            throw new TimeoutException();

        }
    }
}
