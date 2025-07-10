using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class CharacterStat
    {
        public enum CharacterStatType
        {
            Level,
            Attack,
            Defense,
            Health,

        }

        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        int health;
        public int Health 
        {
            get { return health; } 
            set 
            { 
                health = Math.Clamp(value, -100, 0);
            } 
        }

        public CharacterStat()
        {
            Level = 1;
            Attack = 0;
            Defense = 0;
            Health = 0;

        }
    }
}
