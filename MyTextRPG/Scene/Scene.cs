using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTextRPG
{
    internal class Scene
    {

        /// <summary>
        /// Render() 함수의 마지막에 출력되는 메시지
        /// </summary>
        protected string msg = null;
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
            if (msg != null)
            {
                Console.WriteLine(msg);
                Console.WriteLine();
                msg = null;
            }
        }
    }
}
