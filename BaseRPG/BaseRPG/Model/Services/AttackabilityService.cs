using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Services
{
    public class AttackabilityService
    {
        public enum Group {
            Friendly, Enemy, Neutral, Harmless
        }
        private List<Func<IAttacking, IAttackable, bool>> allowingPredicates;
        //TODO ezt majd adatbázisból
        private Dictionary<Group, List<Group>> canAttackMapping;
        public Dictionary<Group, List<Group>> CanAttackMapping { get { return canAttackMapping; } set { canAttackMapping = value; } }
        private AttackabilityService(Dictionary<Group, List<Group>> canAttackMapping, List<Func<IAttacking, IAttackable, bool>> allowingPredicates)
        {
            this.canAttackMapping = canAttackMapping;
            this.allowingPredicates = allowingPredicates;
        }

        public bool CanAttack(IAttacking attacker, IAttackable attacked)
        {
            if (allowingPredicates.Any(p => p.Invoke(attacker, attacked)))
                return true;
            return canAttackMapping[attacker.OffensiveGroup].Contains(attacked.DefensiveGroup);
        }

        public class Builder{
            private List<Func<IAttacking, IAttackable, bool>> allowingPredicates = new();
            public Dictionary<Group, List<Group>> staticDefaultCanAttackMapping = null;
            public Dictionary<Group, List<Group>> DefaultCanAttackMapping
            {
                get
                {
                    if (staticDefaultCanAttackMapping == null)
                    {
                        Dictionary<Group, List<Group>> defaultCanAttackMapping = new Dictionary<Group, List<Group>>();
                        defaultCanAttackMapping.Add(Group.Friendly, new List<Group> { Group.Enemy, Group.Neutral, Group.Harmless });
                        defaultCanAttackMapping.Add(Group.Enemy, new List<Group> { Group.Friendly, Group.Neutral, Group.Harmless });
                        defaultCanAttackMapping.Add(Group.Neutral, new List<Group> { Group.Friendly, Group.Enemy, Group.Harmless });
                        defaultCanAttackMapping.Add(Group.Harmless, new List<Group>());
                        staticDefaultCanAttackMapping = defaultCanAttackMapping;

                    }
                    return staticDefaultCanAttackMapping;
                }
            }
            public Dictionary<Group, List<Group>> ReverseMappingOf(Dictionary<Group, List<Group>> canAttackMapping)
            {
                Dictionary<Group, List<Group>> reversedMapping = new Dictionary<Group, List<Group>>();
                throw new NotImplementedException();
            }
            public AttackabilityService CreateByDefaultMapping()
            {
                return Create(DefaultCanAttackMapping);
            }
            public AttackabilityService CreateByMapping(Dictionary<Group, List<Group>> mapping)
            {
                return Create(mapping);
            }
            public AttackabilityService CreateByReverseMapping(Dictionary<Group, List<Group>> mapping) 
            {
                return Create(ReverseMappingOf(mapping));
            }
            public Builder AllowIf(Func<IAttacking, IAttackable, bool> predicate) { 
                allowingPredicates.Add(predicate);
                return this;
            }
            private AttackabilityService Create(Dictionary<Group, List<Group>> mapping) {
                return new AttackabilityService(mapping,allowingPredicates);
            }
        }
    }
}
