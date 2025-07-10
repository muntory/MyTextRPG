using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class EasyDungeon : Dungeon
    {
        public EasyDungeon()
        {
            baseDefense = 5;
            dungeonName = "쉬운 던전";
            hpLow = 20;
            hpHigh = 35;
            baseGold = 1000;
        }

        

    }
}
