using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Initialization.SkillConfig;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface ISkillManagerConfigurer
    {
        void Configure(SkillManager.Builder builder,GameConfiguration config);

        public static List<ISkillManagerConfigurer> CreateConfigurers(Hero hero,Controller controller) {
            var skillManagerConfigurers = new List<ISkillManagerConfigurer>();
            skillManagerConfigurers.Add(new DashConfigurer(hero.MovementManager, controller));
            skillManagerConfigurers.Add(new InvincibilityConfigurer(hero.MovementManager, controller));
            skillManagerConfigurers.Add(new MeteorConfigurer(hero.MovementManager, controller));
            skillManagerConfigurers.Add(new StunConfigurer());
            return skillManagerConfigurers;
        }
    }
}
