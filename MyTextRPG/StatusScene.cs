using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyTextRPG.IntroScene;

namespace MyTextRPG 
{
    internal class StatusScene : Scene
    {
        public enum StatusCommand
        {
            Invalid,
            Quit,

        }
        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            // 캐릭터 스탯 출력

            Console.WriteLine();

            Console.WriteLine("1. 나가기");
            Console.WriteLine();

            if (Program.UserInput.HasValue && (StatusCommand)Program.UserInput == StatusCommand.Invalid)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine();

            }

        }

        public override int GetInput()
        {
            int ret = base.GetInput();
            StatusCommand cmd = (StatusCommand)ret;

            switch (cmd)
            {
                case StatusCommand.Quit:
                    Program.CurrentScene = new IntroScene();
                    break;
                default:
                    ret = 0;
                    break;
            }

            return ret;

        }
    }
}
