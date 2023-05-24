using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Skill
{
    public abstract class Skill
    {
        private string name;
        private int learnCost;

        protected Skill(string name, int learnCost = 1)
        {
            this.name = name;
            this.learnCost = learnCost;
        }

        public string Name { get => name;}
        public int LearnCost { get => learnCost; }

        public abstract void Cast(object param);
    }
}
