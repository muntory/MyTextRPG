using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyTextRPG.SellScene;

namespace MyTextRPG
{
    internal class DungeonScene : Scene
    {
        public enum DungeonCommand
        { 
            Invalid = -1,
            Quit,

        }

        List<Dungeon> dungeonList;
        bool isClear = false;
        string selectedDungeon;
        int prevGold;
        int prevHP;

        public DungeonScene()
        {
            dungeonList = new List<Dungeon>();
            dungeonList.Add(new EasyDungeon());
            dungeonList.Add(new NormalDungeon());
            dungeonList.Add(new HardDungeon());

        }


        public override void Render()
        {
            base.Render();

            Console.WriteLine("던전입장");
            Console.WriteLine("입장하고 싶은 던전을 선택하여 주세요.");
            Console.WriteLine();

            for (int i = 0; i < dungeonList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dungeonList[i].dungeonName}\t| 방어력 {dungeonList[i].baseDefense} 이상 권장");
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            PrintErrorMsg();

        }

        private void PopUpDungeonResult()
        {
            Console.Clear();
            if (isClear)
            {
                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{selectedDungeon}을 클리어 하였습니다.");
                Console.WriteLine();

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {prevHP} -> {GameManager.Instance.Player.characterStat.Health}");
                Console.WriteLine($"Gold {prevGold} G -> {GameManager.Instance.Player.Gold} G");
                Console.WriteLine();

                Console.WriteLine("0. 나가기");
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("던전 클리어 실패");
                Console.WriteLine($"{selectedDungeon}을 클리어 하지 못하였습니다.");
                Console.WriteLine();

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {prevHP} -> {GameManager.Instance.Player.characterStat.Health}");
                Console.WriteLine();

                Console.WriteLine("0. 나가기");
                Console.WriteLine();
            }

            isClear = false;
                        
            while (base.GetInput() != 0)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);

            }
        }


        public override int GetInput()
        {
            int ret = base.GetInput();

            if (ret > 0)
            {
                if (ret <= dungeonList.Count)
                {
                    prevGold = GameManager.Instance.Player.Gold;
                    prevHP = GameManager.Instance.Player.characterStat.Health;

                    selectedDungeon = dungeonList[ret - 1].dungeonName;

                    isClear = dungeonList[ret - 1].Enter();
                    PopUpDungeonResult();

                    if (GameManager.Instance.Player.characterStat.Health <= 0)
                    {
                        GameManager.Instance.Player.OnDie?.Invoke();
                    }

                    return ret;
                }

            }

            DungeonCommand cmd = (DungeonCommand)ret;

            switch (cmd)
            {
                case DungeonCommand.Quit:
                    GameManager.Instance.CurrentScene = new IntroScene();
                    break;
                default:
                    ret = -1;
                    msg = "잘못된 입력입니다.";
                    break;
            }

            return ret;

        }
    }
}
