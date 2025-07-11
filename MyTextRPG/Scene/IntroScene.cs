﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class IntroScene : Scene
    {
        public enum IntroCommand
        {
            Invalid = -1,
            CreateCharacter,
            ViewStatus,
            OpenInventory,
            OpenStore,
            GoDungeon,
            GoRest,
            GameExit,

        }

        public override void Render()
        {
            base.Render();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("0. 새로운 캐릭터 생성");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 게임종료");
            Console.WriteLine();

            PrintErrorMsg();

        }

        public override int GetInput()
        {
            int ret = base.GetInput();
            IntroCommand cmd = (IntroCommand)ret;

            switch (cmd)
            {
                case IntroCommand.CreateCharacter:
                    GameManager.Instance.CurrentScene = new NewCharacterScene();
                    break;
                case IntroCommand.ViewStatus:
                    GameManager.Instance.CurrentScene = new StatusScene();
                    break;
                case IntroCommand.OpenInventory:
                    GameManager.Instance.CurrentScene = new InventoryScene();
                    break;
                case IntroCommand.OpenStore:
                    GameManager.Instance.CurrentScene = new StoreScene();
                    break;
                case IntroCommand.GoDungeon:
                    GameManager.Instance.CurrentScene = new DungeonScene();
                    break;
                case IntroCommand.GoRest:
                    GameManager.Instance.CurrentScene = new RestScene();
                    break;
                case IntroCommand.GameExit:
                    GameManager.Instance.SaveGameData();
                    Environment.Exit(0);
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
