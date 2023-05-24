using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{

    internal class Rounder
    {
        public static int ToRoundedInt(double value)
        {
            return (int)Math.Round(value, 2);
        }
    }
    
    
    public record struct Stat
    {
        private readonly int value;
        private readonly int additiveBonus;
        private readonly double multiplicativeBonus;
        private Stat(int value, int additiveBonus, int multiplicativeBonus)
        {
            this.value = value;
            this.additiveBonus = additiveBonus;
            this.multiplicativeBonus = multiplicativeBonus;
        }
        public Stat(int value)
        {
            this.value = value;
            this.additiveBonus = 0;
            this.multiplicativeBonus = 1;
        }
        public int Value {
            get {
                return Rounder.ToRoundedInt((value + additiveBonus) * multiplicativeBonus);
            }
        }
        

        public static Stat operator +(Stat stat,int value) {
            Stat newStat = new Stat(
                value :              stat.value,
                additiveBonus :      stat.additiveBonus + value,
                multiplicativeBonus :1
            );
            return newStat;
        }

        public static Stat operator -(Stat stat, int value)
        {
            Stat newStat = new Stat(
                value: stat.value,
                additiveBonus: stat.additiveBonus - value,
                multiplicativeBonus: 1
            );
            return newStat;
        }

        public static Stat operator *(Stat stat, double value)
        {
            Stat newStat = new Stat(
                value: stat.value,
                additiveBonus: 0,
                multiplicativeBonus: Rounder.ToRoundedInt(stat.multiplicativeBonus * value)
            ) ;
            return newStat;
        }

        public static Stat operator /(Stat stat, double value)
        {
            Stat newStat = new Stat(
                value: stat.value,
                additiveBonus: 0,
                multiplicativeBonus: Rounder.ToRoundedInt(stat.multiplicativeBonus / value)
            );
            return newStat;
        }
    }
}
