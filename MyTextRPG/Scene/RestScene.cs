using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class RestScene : Scene
    {
        public enum RestCommand
        {
            Invalid = -1,
            Quit,
            Rest,

        }
        public static int Fare = 500;
        public static int HealAmount = 50;
        public override void Render()
        {
            base.Render();

            Console.WriteLine("휴식하기");
            Console.WriteLine($"{Fare} G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {Program.Player.Gold} G)");
            Console.WriteLine();

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            PrintErrorMsg();
        }

        public override int GetInput()
        {
            int ret = base.GetInput();
            RestCommand cmd = (RestCommand)ret;

            switch (cmd)
            {
                case RestCommand.Rest:
                    int? result = Program.Player.OnRest?.Invoke();
                    if (result.HasValue)
                    {
                        if (result == 0)
                        {
                            errorMsg = "Gold 가 부족합니다.";
                        }
                        if (result == 1)
                        {
                            errorMsg = $"휴식을 완료했습니다. (현재 체력 : {CharacterBaseStatData.BaseHealth + Program.Player.characterStat.Health})";
                        }
                    }
                    break;
                case RestCommand.Quit:
                    Program.CurrentScene = new IntroScene();
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
