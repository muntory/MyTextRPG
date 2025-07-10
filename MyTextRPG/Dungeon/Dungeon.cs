using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal abstract class Dungeon
    {
        public int baseDefense;
        public string dungeonName;
        protected int hpLow;
        protected int hpHigh;
        protected int baseGold;

        public bool Enter()
        {
            int randomValue = new Random().Next(0, 10);
            bool success;
            int finalGold;
            double rewardRatio;
            int finalHP;
            float playerDefense = Program.Player.characterStat.Defense + CharacterBaseStatData.BaseDefense;
            float playerAttack = Program.Player.characterStat.Attack + CharacterBaseStatData.BaseAttack;
            int weightHP = (int)playerDefense - baseDefense;

            if (playerDefense < baseDefense)
            {
                if (randomValue < 4)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            else
            {
                success = true;
            }

            finalHP = new Random().Next(hpLow + weightHP, hpHigh + 1 + weightHP);
            if (success)
            {
                Program.Player.DungeonCountToLevelUp--;
                rewardRatio = playerAttack * (new Random().NextDouble() + 1.0);
                finalGold = (int)(baseGold * (rewardRatio * 0.01 + 1.0));

                Program.Player.characterStat.Health -= finalHP;
                Program.Player.Gold += finalGold;
            }
            else
            {
                Program.Player.characterStat.Health -= finalHP / 2;
            }

            return success;
        }
        


    }
}
