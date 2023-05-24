using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Skills
{
    public class SkillManager
    {
        public event Action<int> SkillPointsChanged;
        public int SkillPoints { get =>skillPoints; 
            set 
            {
                skillPoints = value;
                SkillPointsChanged?.Invoke(skillPoints);
            } 
        }
        private int skillPoints = 3;
        private Dictionary<string, SkillInfo> possibleSkills;
        private SkillManager(Dictionary<string, SkillInfo> possibleSkills)
        {
            this.possibleSkills = possibleSkills;
        }
        public SkillManager()
        {
            this.possibleSkills = new();
        }
        

        internal Skill GetByName(string name)
        {
            if (possibleSkills.ContainsKey(name)) return possibleSkills[name].Skill;
            return null;
        }

        public bool Cast(string skillName, object skillCastParams)
        {
            if (possibleSkills.ContainsKey(skillName)) {
                if (possibleSkills[skillName].IsLearnt) { 
                    possibleSkills[skillName].Skill.Cast(skillCastParams);
                    return true;
                }
            }
            return false;
            
        }

        internal bool Learn(Skill skill)
        {
            var skillInfo = possibleSkills[skill.Name];
            if(skillInfo.IsLearnt) return true;
            if (skill.LearnCost > SkillPoints) return false;
            skillInfo.IsLearnt = true;
            SkillPoints -= skill.LearnCost;
            return true;
        }
        public bool IsLearnt(string skill) {
            lock (this) {
                return possibleSkills[skill].IsLearnt;
            }
            
        }
        private class SkillInfo {
            public Skill Skill { get; init; }
            public bool IsLearnt { get; set; } = false;
        }
        public class Builder {
            private Dictionary<string,SkillInfo> possibleSkills = new();
            public Builder WithSkill(Skill skill) {
                possibleSkills.Add(skill.Name, new SkillInfo { Skill = skill });
                return this;
            }
            public SkillManager Create() { 
                return new SkillManager(possibleSkills);
            }
        }
    }
}
