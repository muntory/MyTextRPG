using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class StatusScene : Scene
    {
        public enum StatusCommand
        {
            Invalid = -1,
            Quit,

        }
        public override void Render()
        {
            base.Render();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            // 캐릭터 스탯 출력
            Console.WriteLine($"Lv. {Program.Player.characterStat.Level}");
            Console.WriteLine($"{Program.Player.Name} ( {Program.Player.RootClass} )");
            Console.WriteLine($"공격력 : {CharacterBaseStatData.BaseAttack + Program.Player.characterStat.Attack} {(Program.Player.characterStat.Attack > 0 ? $"(+{Program.Player.characterStat.Attack})" : "")}");
            Console.WriteLine($"방어력 : {CharacterBaseStatData.BaseArmor + Program.Player.characterStat.Armor} {(Program.Player.characterStat.Armor > 0 ? $"(+{Program.Player.characterStat.Armor})" : "")}");
            Console.WriteLine($"체 력 : {CharacterBaseStatData.BaseHealth + Program.Player.characterStat.Health}  {(Program.Player.characterStat.Health > 0 ? $"(+{Program.Player.characterStat.Health})" : "")}");
            Console.WriteLine($"Gold : {Program.Player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            PrintErrorMsg();

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
                    errorMsg = "잘못된 입력입니다.";
                    break;
            }

            return ret;

        }
    }
}
