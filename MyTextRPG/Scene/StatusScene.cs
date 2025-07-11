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

            Character player = GameManager.Instance.Player;
            // 캐릭터 스탯 출력
            Console.WriteLine($"Lv. {player.characterStat.Level}");
            Console.WriteLine($"{player.Name} ( {player.RootClass} )");
            Console.WriteLine($"공격력 : {player.characterStat.FinalAttack} {(player.characterStat.ModifierAttack > 0 ? $"(+{player.characterStat.ModifierAttack})" : "")}");
            Console.WriteLine($"방어력 : {player.characterStat.FinalDefense} {(player.characterStat.ModifierDefense > 0 ? $"(+{player.characterStat.ModifierDefense})" : "")}");
            Console.WriteLine($"체 력 : {player.characterStat.Health}");
            Console.WriteLine($"Gold : {GameManager.Instance.Player.Gold} G");
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
                    GameManager.Instance.CurrentScene = new IntroScene();
                    break;
                default:
                    ret = 0;
                    msg = "잘못된 입력입니다.";
                    break;
            }

            return ret;

        }
    }
}
