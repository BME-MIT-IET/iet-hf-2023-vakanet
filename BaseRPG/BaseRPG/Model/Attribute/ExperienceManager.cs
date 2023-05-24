using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public delegate void ExperienceChanged(double currentXp,double xpUntilNextLevel);
    public class ExperienceManager
    {
        public event Action<int> LevelUp;
        public event Action<double,double> ExperienceChanged;
        
        private double experienceUntilNextLevel = 10;
        private int level = 1;
        private double currentExperience = 0;
        public double CurrentExperience {
            get => currentExperience;
            set { 
                currentExperience = value;
                
            }
        }
        public ExperienceManager(){
            this.LevelUp += OnLevelUp;
        }

        public int Level => level;
        public void GainExpirence(double xp) {
            bool hasLeveledUp = false;
            do {
                var xpBefore = experienceUntilNextLevel - CurrentExperience;
                hasLeveledUp = GainExperienceUntilNextLevel(xp);
                xp -= xpBefore;
            } while (hasLeveledUp);
            ExperienceChanged?.Invoke(currentExperience, experienceUntilNextLevel);

        }
        private bool GainExperienceUntilNextLevel(double xp) {
            CurrentExperience += xp;
            if (CurrentExperience >= experienceUntilNextLevel)
            {
                level += 1;
                LevelUp?.Invoke(level);
                return true;
            }
            return false;
        }

        public void OnLevelUp(int newLevel) {
            experienceUntilNextLevel = newLevel*10;
            CurrentExperience = 0;
        }

        public double PercentageToNextLevel => CurrentExperience/experienceUntilNextLevel;
    }
}
