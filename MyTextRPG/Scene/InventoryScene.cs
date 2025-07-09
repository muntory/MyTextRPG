using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class InventoryScene : Scene
    {
        public enum InventoryCommand
        {
            Invalid = -1,
            Quit,
            OpenEquipment,

        }
        public override void Render()
        {
            base.Render();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            if (Program.UserInput.HasValue && (InventoryCommand)Program.UserInput == InventoryCommand.Invalid)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine();

            }

        }

        public override int GetInput()
        {
            int ret = base.GetInput();
            InventoryCommand cmd = (InventoryCommand)ret;

            switch (cmd)
            {
                case InventoryCommand.OpenEquipment:
                    Program.CurrentScene = new EquipmentScene();
                    break;
                case InventoryCommand.Quit:
                    Program.CurrentScene = new IntroScene();
                    break;
                default:
                    ret = -1;
                    break;
            }

            return ret;

        }
    }
}
