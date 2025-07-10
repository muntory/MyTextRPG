using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class NormalDungeon : Dungeon
    {
        public NormalDungeon()
        {
            baseDefense = 11;
            dungeonName = "일반 던전";
            hpLow = 20;
            hpHigh = 35;
            baseGold = 1700;
        }

    }
    
}
