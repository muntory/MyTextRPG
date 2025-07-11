using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
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

        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                if (level <= 0)
                {
                    level = 1;
                }
            }
        }
        public double BaseAttack = CharacterBaseStat.BaseAttack;
        public double ModifierAttack = 0.0; // 아이템 장착으로 얻는 효과
        public double FinalAttack
        {
            get { return BaseAttack + ModifierAttack; }
        }

        public double BaseDefense = CharacterBaseStat.BaseDefense;
        public double ModifierDefense = 0.0; // 아이템 장착으로 얻는 효과
        public double FinalDefense
        {
            get { return BaseDefense + ModifierDefense; }
        }

        int health = CharacterBaseStat.BaseHealth;
        public int Health 
        {
            get { return health; } 
            set 
            { 
                health = Math.Clamp(value, 0, 100);
            } 
        }

        public CharacterStat()
        {
            Level = 1;

        }
    }
}
