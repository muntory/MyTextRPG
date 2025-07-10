using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyTextRPG.RestScene;

namespace MyTextRPG
{
    internal class NewCharacterScene : Scene
    {
        public NewCharacterScene()
        {
            string characterDataPath = $"{Directory.GetCurrentDirectory()}/Save/Character.json";
            if (File.Exists(characterDataPath))
            {
                File.Delete(characterDataPath);
            }
        }

        public override void Render()
        {
            base.Render();

            Console.WriteLine("새로운 캐릭터 생성");
            Console.WriteLine();

            PrintErrorMsg();
        }

        public override int GetInput()
        {
            Console.WriteLine("캐릭터의 이름을 입력하여 주세요.");
            Console.Write(">> ");
            string name = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("캐릭터의 직업을 입력하여 주세요.");
            Console.Write(">> ");
            string rootClass = Console.ReadLine();

            GameManager.Instance.Player = new Character();
            GameManager.Instance.Player.Name = name;
            GameManager.Instance.Player.RootClass = rootClass;

            GameManager.Instance.CurrentScene = new IntroScene();

            return 0;

        }
    }
}
