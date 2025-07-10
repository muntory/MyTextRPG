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
            int rewardRatio;
            int finalHP;
            int playerDefense = Program.Player.characterStat.Defense + CharacterBaseStatData.BaseDefense;
            int playerAttack = Program.Player.characterStat.Attack + CharacterBaseStatData.BaseAttack;
            int weightHP = playerDefense - baseDefense;

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
                rewardRatio = new Random().Next(playerAttack, playerAttack * 2 + 1);
                finalGold = (int)(baseGold * rewardRatio * 0.01f);

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
