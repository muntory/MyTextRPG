using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class Scene
    {

        protected string errorMsg = null;
        public virtual void Render()
        {
            Console.Clear();

        }

        public virtual int GetInput()
        {

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int ret;
            while (!int.TryParse(Console.ReadLine(), out ret))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(">> ");
            }

            return ret;

        }

        public void PrintErrorMsg()
        {
            if (errorMsg != null)
            {
                Console.WriteLine(errorMsg);
                Console.WriteLine();
                errorMsg = null;
            }
        }
    }
}
