using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class Scene
    {
        public virtual void Render()
        {
            Program.UserInput = null;
            Console.Clear();

        }

        public virtual int GetInput()
        {

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int ret = int.Parse(Console.ReadLine());
            return ret;

        }
    }
}
