using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class HardDungeon : Dungeon
    {
        public HardDungeon()
        {
            baseDefense = 17;
            dungeonName = "어려운 던전";
            hpLow = 20;
            hpHigh = 35;
            baseGold = 2500;
        }

        
    }
}
