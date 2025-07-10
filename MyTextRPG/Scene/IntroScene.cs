using System;
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
            ViewStatus = 1,
            OpenInventory,
            OpenStore,
            GoDungeon,
            GoRest,
        }

        public override void Render()
        {
            base.Render();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine();

            PrintErrorMsg();

        }

        public override int GetInput()
        {
            int ret = base.GetInput();
            IntroCommand cmd = (IntroCommand)ret;

            switch (cmd)
            {
                case IntroCommand.ViewStatus:
                    Program.CurrentScene = new StatusScene();
                    break;
                case IntroCommand.OpenInventory:
                    Program.CurrentScene = new InventoryScene();
                    break;
                case IntroCommand.OpenStore:
                    Program.CurrentScene = new StoreScene();
                    break;
                case IntroCommand.GoDungeon:
                    Program.CurrentScene = new DungeonScene();
                    break;
                case IntroCommand.GoRest:
                    Program.CurrentScene = new RestScene();
                    break;
                default:
                    ret = -1;
                    errorMsg = "잘못된 입력입니다.";
                    break;
            }

            return ret;
        }

    }
}
