namespace MyTextRPG
{
    internal class Program
    {
        static Scene currentScene = new IntroScene();
        public static Scene CurrentScene
        {
            get { return currentScene; }
            set { currentScene = value; }
        }

        static Character player;
        public static Character Player
        {
            get { return player; }
        }


        static int? userInput = null;
        public static int? UserInput
        {
            get { return userInput; }
            set { userInput = value; }
        }


        static void Main(string[] args)
        {
            Program program = new Program();

            program.Init();

            while (true)
            {
                program.Render();
                program.Update();
            }

        }

        private void Init()
        {
            player = new Character();
        }

        private void Update()
        {
            userInput = currentScene.GetInput();
            
        }

        private void Render()
        {
            currentScene.Render();
        }
    }
}
