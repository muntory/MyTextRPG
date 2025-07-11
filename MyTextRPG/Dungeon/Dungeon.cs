using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal abstract class Dungeon
    {
        public double baseDefense;
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
            double playerDefense = GameManager.Instance.Player.characterStat.FinalDefense;
            double playerAttack = GameManager.Instance.Player.characterStat.FinalAttack;
            int weightHP = (int)(playerDefense - baseDefense);

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

            finalHP = new Random().Next(hpLow - weightHP, hpHigh + 1 - weightHP);

            if (success)
            {
                GameManager.Instance.Player.DungeonCountToLevelUp--;
                rewardRatio = playerAttack * (new Random().NextDouble() + 1.0);
                finalGold = (int)(baseGold * (rewardRatio * 0.01 + 1.0));

                GameManager.Instance.Player.characterStat.Health -= finalHP;
                GameManager.Instance.Player.Gold += finalGold;
            }
            else
            {
                GameManager.Instance.Player.characterStat.Health -= finalHP / 2;
            }

            return success;
        }
        


    }
}
